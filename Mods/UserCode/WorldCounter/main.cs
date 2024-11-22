namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Systems.Messaging.Chat.Commands;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.WorldGenerator;
    using Vector3 = System.Numerics.Vector3;

    public class Build
    {
        public static void Main() { }
    }

    public class WorldCounterWorldGen : IWorldGenFeature, IModKitPlugin
    {
        public void Generate(Random seed, Vector3 voxelSize, WorldSettings settings)
        {
            SortedDictionary<string, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<string, int> kvp in counts)
            {
                Log.WriteLine(Localizer.DoStr($"[WorldCounter]: {kvp.Key}: {kvp.Value}"));
            }
        }

        public string GetStatus() => "Loaded WorldCounter";

        public string GetCategory() => "WorldCounter";
    }

    [ChatCommandHandler]
    public class WorldCounterCommands
    {
        [ChatCommand("List of commands for world counting", ChatAuthorizationLevel.Admin)]
        public static void WorldCounter(User user) { }

        [ChatSubCommand(
            "worldcounter",
            "Counts the types of blocks in the world",
            ChatAuthorizationLevel.Admin
        )]
        public static void Count(IChatClient chatClient)
        {
            SortedDictionary<string, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<string, int> kvp in counts)
            {
                // Add spaces between capital letters
                string name = Regex.Replace(kvp.Key, "(\\B[A-Z])", " $1");
                chatClient.MsgLoc($"[WorldCounter]: {kvp.Key}: {name}");
            }
        }
    }

    public class CountWorld
    {
        public static SortedDictionary<string, int> GetCounts()
        {
            {
                IEnumerable<PersistentChunk> Chunks = World.Chunks;
                SortedDictionary<string, int> blockCount = new();
                foreach (PersistentChunk chunk in Chunks)
                {
                    if (chunk is null)
                    {
                        continue;
                    }
                    if (chunk.Blocks is null)
                    {
                        continue;
                    }
                    foreach (Block? block in chunk.Blocks)
                    {
                        if (block is null)
                        {
                            continue;
                        }
                        string type = block.GetType().ToString();
                        if (blockCount.ContainsKey(type))
                        {
                            blockCount[type] += 1;
                        }
                        else
                        {
                            blockCount[type] = 1;
                        }
                    }
                }
                return blockCount;
            }
        }
    }
}
