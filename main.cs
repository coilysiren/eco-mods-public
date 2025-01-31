namespace BunWulfModsPublic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using CommandLine;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Utils;

    public class CLIEntryPoint
    {
        // This only works on my machine >:D
        public static readonly string WindowsServerDirectory = Path.Combine(
            new string[]
            {
                "C:\\",
                "Program Files (x86)",
                "Steam",
                "steamapps",
                "common",
                "Eco",
                "Eco_Data",
                "Server",
            }
        );

        [Verb("BunWulfEducational")]
        public class BunWulfEducationalOpts { }

        [Verb("BunWulfStructural")]
        public class BunWulfStructuralOpts { }

        public static void Main(string[] args)
        {
            _ = Parser
                .Default.ParseArguments<BunWulfEducationalOpts, BunWulfStructuralOpts>(args)
                .WithParsed<BunWulfEducationalOpts>(_ =>
                    BunWulfEducational.Initialize(WindowsServerDirectory)
                )
                .WithParsed<BunWulfStructuralOpts>(_ => Console.WriteLine("TODO"));
        }
    }

    public class BunWulfEducationalPluginEntrypoint
        : IModKitPlugin,
            IServerPlugin,
            IInitializablePlugin,
            IModInit
    {
        public void Initialize(TimedTask timer) => BunWulfEducational.Initialize();

        public override string ToString() => "BunWulfEducational";

        public string GetStatus() => "Loaded BunWulfEducational";

        public string GetCategory() => "BunWulf";

        public static ModRegistration Register() =>
            new()
            {
                ModName = "BunWulfEducational",
                ModDescription = "TODO",
                ModDisplayName = "BunWulf Educational",
            };
    }

    public static class BunWulfEducational
    {
        // Tier Extraction then Skill Level Replacement
        private static readonly string TierExtractionPattern = @".*Tier\((\d)\).*";
        private static readonly string RequiresSkillLevelPattern = @"RequiresSkill.*?(\d)";
        private static readonly string RequiresSkillLevelReplacement =
            @"RequiresSkill(typeof(LibrarianSkill), $1";

        // Skill Extraction then Skill Replacement
        private static readonly string SkillExtractionPattern = @"(\w+)LavishResourcesTalent";
        private static readonly string SkillReplacement = "Librarian";

        // Recipe Replacement
        private static readonly string SkillBookRecipePattern = @"(\w+SkillBookRecipe)";
        private static readonly string ResearchPaperRecipePattern = @"(\w+Research\w+Recipe)";
        private static readonly string RecipeReplacement = "Librarian$1";

        // Description Replacement
        public static readonly string ResearchPaperDescriptionPattern =
            @"([\w\s]+? Research Paper \w+?)";
        public static readonly string SkillBookDescriptionPattern = @"([\w\s]+? Skill Book)";
        private static readonly string DescriptionReplacement = "Librarian $1";

        // Labor Replacement
        private static readonly string LaborPattern = @"typeof\((?!LibrarianSkill)\w+Skill\)";
        private static readonly string LaborReplacement = "typeof(LibrarianSkill)";

        // Namespace replacement
        private static readonly string NamespacePattern = @"namespace .*";
        private static readonly string NamespaceReplacement = "namespace BunWulfEducational";

        // Using replacement
        private static readonly string FirstGameplayUsingPattern =
            @"using Eco.Gameplay.Items.Recipes;";
        private static readonly string FirstGameplayUsingReplacement =
            "using Eco.Gameplay.Items.Recipes;\n    using Eco.Mods.TechTree;";
        private static readonly string SecondGameplayUsingPattern =
            @"using Gameplay.Systems.TextLinks;";
        private static readonly string SecondGameplayUsingReplacement =
            @"using Eco.Gameplay.Systems.TextLinks;";

        // Experience Replacement
        private static readonly string ExperiencePattern =
            @"this.ExperienceOnCraft = \d+(\.\d+f?|f)?";
        private static readonly string ExperienceReplacement = "this.ExperienceOnCraft = $1";

        public static void Initialize(string? sourcebaseDirectory = null)
        {
            sourcebaseDirectory ??= Directory.GetCurrentDirectory();
            Console.WriteLine(
                "[BunWulfEducational] Initializing with base directory: " + sourcebaseDirectory
            );
            WriteTechDirectory(sourcebaseDirectory);
            WriteItemDirectory(sourcebaseDirectory);
            WriteRecipeDirectory(sourcebaseDirectory);
        }

        private static void WriteTechDirectory(string sourcebaseDirectory)
        {
            string coreTechDirectory = Path.Combine(
                sourcebaseDirectory,
                "Mods",
                "__core__",
                "AutoGen",
                "Tech"
            );
            string targetTechDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Mods",
                "UserCode",
                "BunWulfEducational",
                "Recipes",
                "Tech"
            );

            Console.WriteLine("[BunWulfEducational] core tech directory: " + coreTechDirectory);
            Console.WriteLine("[BunWulfEducational] target tech directory: " + targetTechDirectory);

            foreach (string file in Directory.EnumerateFiles(coreTechDirectory))
            {
                bool found;
                string fileName = Path.GetFileName(file);
                // string sourceFilePath = Path.Combine(coreTechDirectory, fileName);
                // Console.WriteLine("[BunWulfEducational] reading " + sourceFilePath);
                string fileData = File.ReadAllText(file);

                Match tierMatch = Regex.Match(fileData, TierExtractionPattern);
                if (tierMatch.Success)
                {
                    string tier = tierMatch.Groups[1].Value;
                    int experience = 50 * int.Parse(tier);
                    fileData = fileData.Replace(
                        "this.LaborInCalories",
                        "this.ExperienceOnCraft = "
                            + experience
                            + ";\n            this.LaborInCalories"
                    );
                }

                // Set the RequiresSkill level to the tier, and replace with LibrarianSkill
                fileData = TextProcessing.ExtractThenReplaceRegex(
                    fileData,
                    fileName,
                    TierExtractionPattern,
                    extractedValue => (int.Parse(extractedValue) + 1).ToString(),
                    RequiresSkillLevelPattern,
                    RequiresSkillLevelReplacement
                );

                // Remove the skill itself so we don't duplicate
                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillPattern
                );

                // Remove the skill book so we don't duplicate
                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillBookPattern
                );

                // Remove the skill scroll so we don't duplicate
                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillScrollPattern
                );

                // Replace with LibrarianSkill labor
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    LaborPattern,
                    LaborReplacement
                );
                if (!found)
                {
                    // If the pattern is not found, we likely aren't in a recipe file
                    // so we skip the file.
                    // Console.WriteLine("[BunWulfEducational]\tskipping write for " + fileName);
                    continue;
                }

                // Replace the namespace with BunWulfEducational
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    NamespacePattern,
                    NamespaceReplacement
                );
                if (!found)
                {
                    throw new InvalidOperationException(
                        $"Namespace pattern not found in {fileName}"
                    );
                }

                // Fixes using statements
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    FirstGameplayUsingPattern,
                    FirstGameplayUsingReplacement
                );
                if (!found)
                {
                    throw new InvalidOperationException($"Using pattern not found in {fileName}");
                }

                // Updates description
                fileData = Regex.Replace(
                    fileData,
                    SkillBookDescriptionPattern,
                    DescriptionReplacement
                );

                fileData = Regex.Replace(fileData, SkillBookRecipePattern, RecipeReplacement);

                string targetFilePath = Path.Combine(targetTechDirectory, fileName);
                Console.WriteLine("[BunWulfEducational] \twriting " + targetFilePath);
                _ = Directory.CreateDirectory(targetTechDirectory);
                File.WriteAllText(targetFilePath, fileData);
            }
        }

        private static void WriteItemDirectory(string sourcebaseDirectory)
        {
            string coreItemDirectory = Path.Combine(
                sourcebaseDirectory,
                "Mods",
                "__core__",
                "AutoGen",
                "Item"
            );
            string targetItemDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Mods",
                "UserCode",
                "BunWulfEducational",
                "Recipes",
                "Item"
            );
            WriteResearchPapers(coreItemDirectory, targetItemDirectory);
        }

        private static void WriteRecipeDirectory(string sourcebaseDirectory)
        {
            string coreRecipeDirectory = Path.Combine(
                sourcebaseDirectory,
                "Mods",
                "__core__",
                "AutoGen",
                "Recipe"
            );
            string targetRecipeDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Mods",
                "UserCode",
                "BunWulfEducational",
                "Recipes",
                "Recipe"
            );
            WriteResearchPapers(coreRecipeDirectory, targetRecipeDirectory);
        }

        private static void WriteResearchPapers(string coreDirectory, string targetDirectory)
        {
            Console.WriteLine("[BunWulfEducational] core item directory: " + coreDirectory);
            Console.WriteLine("[BunWulfEducational] target item directory: " + targetDirectory);

            foreach (string file in Directory.EnumerateFiles(coreDirectory))
            {
                bool found;
                if (!file.Contains("ResearchPaper"))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file);
                // string sourceFilePath = Path.Combine(coreDirectory, fileName);
                // Console.WriteLine("[BunWulfEducational] reading " + sourceFilePath);

                int techLevel = 0;
                if (file.Contains("Basic"))
                {
                    techLevel = 1;
                }
                else if (file.Contains("Advanced"))
                {
                    techLevel = 3;
                }
                else if (file.Contains("Modern"))
                {
                    techLevel = 5;
                }

                int experience = 0;
                if (file.Contains("Basic"))
                {
                    experience = 2;
                }
                else if (file.Contains("Advanced"))
                {
                    experience = 4;
                }
                else if (file.Contains("Modern"))
                {
                    experience = 16;
                }

                string fileData = File.ReadAllText(file);

                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.ItemPattern
                );
                fileData = Regex.Replace(
                    fileData,
                    RequiresSkillLevelPattern,
                    RequiresSkillLevelReplacement.Replace("$1", techLevel.ToString())
                );
                fileData = Regex.Replace(
                    fileData,
                    ExperiencePattern,
                    ExperienceReplacement.Replace("$1", experience.ToString())
                );
                fileData = Regex.Replace(fileData, ResearchPaperRecipePattern, RecipeReplacement);
                fileData = Regex.Replace(
                    fileData,
                    ResearchPaperDescriptionPattern,
                    DescriptionReplacement
                );
                (fileData, _) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    LaborPattern,
                    LaborReplacement
                );
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    NamespacePattern,
                    NamespaceReplacement
                );
                if (!found)
                {
                    throw new InvalidOperationException(
                        $"Namespace pattern not found in {fileName}"
                    );
                }
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    FirstGameplayUsingPattern,
                    FirstGameplayUsingReplacement
                );
                if (!found)
                {
                    throw new InvalidOperationException($"Using pattern not found in {fileName}");
                }
                (fileData, found) = TextProcessing.StaticReplacePattern(
                    fileData,
                    fileName,
                    SecondGameplayUsingPattern,
                    SecondGameplayUsingReplacement
                );
                if (!found) { }
                // Replace Skill => Librarian, requires an extractor though
                fileData = TextProcessing.ExtractThenReplaceString(
                    fileData,
                    fileName,
                    SkillExtractionPattern,
                    SkillReplacement
                );
                // Remove Lavish Resources, which librarian does not have
                fileData = fileData.Replace(", typeof(LibrarianLavishResourcesTalent)", "");
                // Remove Focused Speed, which librarian does not have
                fileData = fileData.Replace(", typeof(LibrarianFocusedSpeedTalent)", "");
                // Remove Parallel Spped, which librarian does not have
                fileData = fileData.Replace(", typeof(LibrarianParallelSpeedTalent)", "");

                string targetFilePath = Path.Combine(targetDirectory, fileName);
                Console.WriteLine("[BunWulfEducational] \twriting " + targetFilePath);
                _ = Directory.CreateDirectory(targetDirectory);
                File.WriteAllText(targetFilePath, fileData);
            }
        }
    }

    public class TextProcessing
    {
        public static readonly string ItemPattern = @".*(class \w+Item) .*";
        public static readonly string BlockPattern = @".*(class \w+Block) .*";
        public static readonly string SkillPattern = @".*(class \w+Skill) .*";
        public static readonly string SkillBookPattern = @".*(class \w+SkillBook) .*";
        public static readonly string SkillScrollPattern = @".*(class \w+SkillScroll) .*";

        public static (string, bool) StaticReplacePattern(
            string recipeData,
            string file,
            string pattern,
            string replacement
        )
        {
            Match match = Regex.Match(recipeData, pattern);
            if (match.Success)
            {
                // Console.WriteLine(
                //     $"[BunWulfEducational]\tReplacing {match.Value} with {replacement}"
                // );
                recipeData = recipeData.Replace(match.Value, replacement);
            }
            else
            {
                // Console.WriteLine(
                //     $"[BunWulfEducational]\tCouldn't find pattern {pattern} in {file}"
                // );
            }
            return (recipeData, match.Success);
        }

        public static string ExtractThenReplaceRegex(
            string recipeData,
            string file,
            string extractor,
            Func<string, string> extractionProcessor,
            string pattern,
            string replacement
        )
        {
            Match extractorMatch = Regex.Match(recipeData, extractor);
            string extractedValue;

            if (extractorMatch.Success)
            {
                extractedValue = extractionProcessor(extractorMatch.Groups[1].Value);
            }
            else
            {
                return recipeData;
            }

            // Inside of recipeData, replace pattern with replacement, using
            // extractedValue as the input value into the replacement.

            string replacementReplaced = replacement.Replace("$1", extractedValue);
            recipeData = Regex.Replace(recipeData, pattern, replacementReplaced);

            return recipeData;
        }

        public static string ExtractThenReplaceString(
            string recipeData,
            string file,
            string extractor,
            string pattern
        )
        {
            Console.WriteLine($"working on {file}");

            Match extractorMatch = Regex.Match(recipeData, extractor);
            string extractedValue;

            if (extractorMatch.Success)
            {
                extractedValue = extractorMatch.Groups[1].Value;
            }
            else
            {
                return recipeData;
            }

            recipeData = recipeData.Replace(extractedValue, pattern);

            return recipeData;
        }

        public static string RemovePattern(string recipeData, string file, string pattern)
        {
            string[] recipeLines = recipeData.Split(new[] { '\n' }, StringSplitOptions.None);
            foreach (string line in recipeLines)
            {
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    recipeData = RemoveClass(recipeData, file, line.Trim(), match.Groups[1].Value);
                }
            }
            return recipeData;
        }

        public static string RemoveClass(
            string recipeData,
            string file,
            string key,
            string className
        )
        {
            List<string> recipeLines = new(
                recipeData.Split(new[] { '\n' }, StringSplitOptions.None)
            );
            // Console.WriteLine($"[BunWulfEducational]\tRemoving \"{className}\" from recipe");

            // Step 1: Identify the line where the class starts.
            int classStartLine = -1;
            for (int line = 0; line < recipeLines.Count; line++)
            {
                if (recipeLines[line].Contains(className))
                {
                    classStartLine = line;
                    break;
                }
            }
            if (classStartLine == -1)
            {
                // Console.WriteLine(
                //     $"[BunWulfEducational]\t\tCouldn't find {className} in {file}:{key}"
                // );
            }
            // Console.WriteLine(
            //     $"[BunWulfEducational]\t\tFound \"{className}\" at line {classStartLine}"
            // );

            // Step 2: Move class start line upwards if the class has any annotations.
            for (int index = classStartLine; index >= 0; index--)
            {
                string line = recipeLines[index].Trim();
                if (line.StartsWith("["))
                {
                    classStartLine = index;
                }
                else if (recipeLines[index].Contains(className))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            // Console.WriteLine(
            //     $"[BunWulfEducational]\t\tMoved class start line to {classStartLine} due to annotations"
            // );

            // Step 3: Count brackets to find the end of the class.
            int bracketCount = 0;
            int classStartIndex = recipeData.IndexOf(className);
            bool bracketsStarted = false;
            int classEndIndex = 0;
            for (int index = classStartIndex; index < recipeData.Length; index++)
            {
                char ch = recipeData[index];
                if (ch == '{')
                {
                    bracketCount++;
                    bracketsStarted = true;
                }
                else if (ch == '}')
                {
                    bracketCount--;
                }
                else if (bracketsStarted && bracketCount == 0)
                {
                    classEndIndex = index;
                    break;
                }
            }
            if (bracketCount != 0 || classEndIndex == 0)
            {
                // Console.WriteLine(
                //     $"[BunWulfEducational]\t\tCouldn't find end of class in {file}:{key}"
                // );
            }
            int classEndLine = recipeData[..classEndIndex].Split('\n').Length;
            // Console.WriteLine(
            //     $"[BunWulfEducational]\t\tClass end bracket found at line {classEndLine}"
            // );

            // Step 4: Remove the lines from the start to the end of the class.
            // Console.WriteLine(
            //     $"[BunWulfEducational]\t\tRemoving lines {classStartLine} to {classEndLine}"
            // );
            recipeLines.RemoveRange(classStartLine, classEndLine - classStartLine);

            // Step -1: Join the lines back together to get a single string.
            recipeData = string.Join("\n", recipeLines);
            return recipeData;
        }
    }
}
