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
    public class Constants
    {
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

        public static readonly string LinuxServerDirectory = Directory.GetCurrentDirectory();

        public static readonly string ItemPattern = @".*(class \w+Item) .*";
        public static readonly string BlockPattern = @".*(class \w+Block) .*";
        public static readonly string SkillPattern = @".*(class \w+Skill) .*";
        public static readonly string SkillBookPattern = @".*(class \w+SkillBook) .*";
        public static readonly string SkillScrollPattern = @".*(class \w+SkillScroll) .*";
    }

    public class CLIEntryPoint
    {
        [Verb("BunWulfEducational")]
        public class BunWulfEducationalOpts { }

        [Verb("BunWulfStructural")]
        public class BunWulfStructuralOpts { }

        public static void Main(string[] args)
        {
            _ = Parser
                .Default.ParseArguments<BunWulfEducationalOpts, BunWulfStructuralOpts>(args)
                .WithParsed<BunWulfEducationalOpts>(_ =>
                    BunWulfEducational.Initialize(Constants.WindowsServerDirectory)
                )
                .WithParsed<BunWulfStructuralOpts>(_ => Console.WriteLine("TODO"));
        }
    }

    public class EcoPluginEntrypoint : IModKitPlugin, IModInit
    {
        public static void Initialize() =>
            BunWulfEducational.Initialize(Constants.LinuxServerDirectory);

        public override string ToString() => Localizer.DoStr(this.GetType().Name);

        public string GetStatus() => "Loaded " + this.GetType().Name;

        public string GetCategory() => "BunWulf";

        [ChatCommand(
            "regenerates the BunWulfEducational mod",
            "generate-bunwulf-educational",
            ChatAuthorizationLevel.Admin
        )]
        public static void GenerateBunWulfEducational(User user) =>
            BunWulfEducational.Initialize(Constants.LinuxServerDirectory);
    }

    public static class BunWulfEducational
    {
        public static void Initialize(string directory)
        {
            string coreSourceDirectory = Path.Combine(
                directory,
                "Mods",
                "__core__",
                "AutoGen",
                "Tech"
            );
            string targetDirectory = Path.Combine(
                directory,
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
                string sourceFilePath = Path.Combine(coreSourceDirectory, fileName);
                Console.WriteLine("[BunWulfEducational] reading " + sourceFilePath);
                string fileData = File.ReadAllText(file);

                fileData = RemovePattern(fileData, fileName, Constants.SkillPattern);
                fileData = RemovePattern(fileData, fileName, Constants.SkillBookPattern);
                fileData = RemovePattern(fileData, fileName, Constants.SkillScrollPattern);

                string targetFilePath = Path.Combine(targetDirectory, fileName);
                Console.WriteLine("[BunWulfEducational] writing " + targetFilePath);
                File.WriteAllText(targetFilePath, fileData);
            }
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
            Console.WriteLine($"[BunWulfEducational]\tRemoving \"{className}\" from recipe");

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
                throw new Exception(
                    $"[BunWulfEducational]\t\tCouldn't find {className} in {file}:{key}"
                );
            }
            Console.WriteLine(
                $"[BunWulfEducational]\t\tFound \"{className}\" at line {classStartLine}"
            );

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
            Console.WriteLine(
                $"[BunWulfEducational]\t\tMoved class start line to {classStartLine} due to annotations"
            );

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
                throw new Exception(
                    $"[BunWulfEducational]\t\tCouldn't find end of class in {file}:{key}"
                );
            }
            int classEndLine = recipeData[..classEndIndex].Split('\n').Length;
            Console.WriteLine(
                $"[BunWulfEducational]\t\tClass end bracket found at line {classEndLine}"
            );

            // Step 4: Remove the lines from the start to the end of the class.
            Console.WriteLine(
                $"[BunWulfEducational]\t\tRemoving lines {classStartLine} to {classEndLine}"
            );
            recipeLines.RemoveRange(classStartLine, classEndLine - classStartLine);

            // Step -1: Join the lines back together to get a single string.
            recipeData = string.Join("\n", recipeLines);
            return recipeData;
        }
    }
}
