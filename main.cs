using System;
using System.IO;
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
            Console.WriteLine("[BunWulfEducational] Initializing...");
            Console.WriteLine("[BunWulfEducational] working directory: " + directory);
        }
    }
}
