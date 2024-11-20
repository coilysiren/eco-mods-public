namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Systems.Messaging.Chat.Commands;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
    using Eco.Shared.Math;
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
            SortedDictionary<Type, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<Type, int> kvp in counts)
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
            SortedDictionary<Type, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<Type, int> kvp in counts)
            {
                chatClient.MsgLoc($"[WorldCounter]: {kvp.Key}: {kvp.Value}");
            }
        }
    }

    public class CountWorld
    {
        public static SortedDictionary<Type, int> GetCounts()
        {
            {
                IEnumerable<PersistentChunk> Chunks = World.Chunks;
                SortedDictionary<Type, int> blockCount = new();
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
                        Type type = block.GetType();
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
