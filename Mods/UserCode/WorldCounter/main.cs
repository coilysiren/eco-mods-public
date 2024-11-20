using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Shared.Localization;
using Eco.World;

namespace WorldCounter
{
    public class BunWulfEducationalPluginEntrypoint
        : IModKitPlugin,
            IServerPlugin,
            IInitializablePlugin,
            IModInit
    {
        public void Main() { }

        public void Initialize(TimedTask timer)
        {
            Console.WriteLine("WorldCounter initializing");

            IEnumerable<PersistentChunk> Chunks = World.Chunks;

            foreach (PersistentChunk chunk in Chunks)
            {
                Console.WriteLine($"Chunk Type: {chunk.GetType().Name}");
            }
        }

        public override string ToString() => "WorldCounter";

        public string GetStatus() => "Loaded WorldCounter";

        public string GetCategory() => "WorldCounter";

        // [ChatCommand(
        //     "regenerates the BunWulfEducational mod",
        //     "generate-bunwulf-educational",
        //     ChatAuthorizationLevel.Admin
        // )]
        // public static void GenerateBunWulfEducational(User user) => BunWulfEducational.Initialize();
    }
}
