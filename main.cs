using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CommandLine;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Localization;

namespace BunWulfMods
{
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

    public class EcoPluginEntrypoint : IModKitPlugin, IModInit
    {
        public static void Initialize() => BunWulfEducational.Initialize();

        public override string ToString() => Localizer.DoStr(this.GetType().Name);

        public string GetStatus() => "Loaded " + this.GetType().Name;

        public string GetCategory() => "BunWulf";

        [ChatCommand(
            "regenerates the BunWulfEducational mod",
            "generate-bunwulf-educational",
            ChatAuthorizationLevel.Admin
        )]
        public static void GenerateBunWulfEducational(User user) => BunWulfEducational.Initialize();
    }

    public static class BunWulfEducational
    {
        // Tier Extraction then Skill Level Replacement
        private static readonly string TierPattern = @".*Tier\((\d)\).*";
        private static readonly string RequiresSkillLevelPattern = @"RequiresSkill.*?(\d)";
        private static readonly string RequiresSkillLevelReplacement =
            @"RequiresSkill(typeof(LibrarianSkill), $1";

        // Recipe Replacement
        private static readonly string RecipePattern = @"(\w+SkillBookRecipe)";
        private static readonly string RecipeReplacement = "Librarian$1";

        // Description Replacement
        public static readonly string DescriptionPattern = @"([\w\s]+? Skill Book)";
        private static readonly string DescriptionReplacement = "Librarian $1";

        // Labor Replacement
        private static readonly string LaborPattern = @"typeof\((?!LibrarianSkill)\w+Skill\)";
        private static readonly string LaborReplacement = "typeof(LibrarianSkill)";

        public static void Initialize(string? sourcebaseDirectory = null)
        {
            sourcebaseDirectory ??= Directory.GetCurrentDirectory();
            string coreSourceDirectory = Path.Combine(
                sourcebaseDirectory,
                "Mods",
                "__core__",
                "AutoGen",
                "Tech"
            );
            string targetDirectory = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Mods",
                "UserCode",
                "BunWulfEducational",
                "Recipes",
                "Tech"
            );

            Console.WriteLine("[BunWulfEducational] Initializing...");
            Console.WriteLine("[BunWulfEducational] core source directory: " + coreSourceDirectory);
            Console.WriteLine("[BunWulfEducational] target directory:      " + targetDirectory);

            // Iterate the core source directory to find the files we want to copy
            foreach (string file in Directory.EnumerateFiles(coreSourceDirectory))
            {
                string fileName = Path.GetFileName(file);
                // string sourceFilePath = Path.Combine(coreSourceDirectory, fileName);
                // Console.WriteLine("[BunWulfEducational] reading " + sourceFilePath);
                string fileData = File.ReadAllText(file);

                fileData = TextProcessing.ExtractThenReplace(
                    fileData,
                    fileName,
                    TierPattern,
                    RequiresSkillLevelPattern,
                    RequiresSkillLevelReplacement
                );

                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillPattern
                );
                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillBookPattern
                );
                fileData = TextProcessing.RemovePattern(
                    fileData,
                    fileName,
                    TextProcessing.SkillScrollPattern
                );

                (fileData, bool found) = TextProcessing.StaticReplacePattern(
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

                fileData = Regex.Replace(fileData, RecipePattern, RecipeReplacement);
                fileData = Regex.Replace(fileData, DescriptionPattern, DescriptionReplacement);

                string targetFilePath = Path.Combine(targetDirectory, fileName);
                Console.WriteLine("[BunWulfEducational] writing " + targetFilePath);
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

        public static string ExtractThenReplace(
            string recipeData,
            string file,
            string extractor,
            string pattern,
            string replacement
        )
        {
            Match extractorMatch = Regex.Match(recipeData, extractor);
            string extractedValue;

            if (extractorMatch.Success)
            {
                extractedValue = extractorMatch.Groups[1].Value;
            }
            else
            {
                // Console.WriteLine(
                //     $"[BunWulfEducational]\tCouldn't find extractor {extractor} in {file}"
                // );
                return recipeData;
            }

            // Inside of recipeData, replace pattern with replacement, using
            // extractedValue as the input value into the replacement.

            recipeData = Regex.Replace(
                recipeData,
                pattern,
                replacement.Replace("$1", extractedValue)
            );

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
            List<string> recipeLines =
                new(recipeData.Split(new[] { '\n' }, StringSplitOptions.None));
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
