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

    public class WorldCounterWorldGen : IWorldGenFeature, IModKitPlugin
    {
        public static void Main() { }

        public void Generate(Random seed, Vector3 voxelSize, WorldSettings settings)
        {
            Dictionary<Type, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<Type, int> kvp in counts)
            {
                Log.WriteLine(Localizer.DoStr($"{kvp.Key}: {kvp.Value}"));
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
            Dictionary<Type, int> counts = CountWorld.GetCounts();
            foreach (KeyValuePair<Type, int> kvp in counts)
            {
                chatClient.MsgLoc($"{kvp.Key}: {kvp.Value}");
            }
        }
    }

    public class CountWorld
    {
        public static Dictionary<Type, int> GetCounts()
        {
            {
                Log.WriteLine(Localizer.DoStr("WorldCounter starting"));

                IEnumerable<PersistentChunk> Chunks = World.Chunks;
                Dictionary<Type, int> blockCount = new();

                foreach (PersistentChunk chunk in Chunks)
                {
                    Vector3i chunkPosition = chunk.Position;
                    Block block = World.GetBlock(chunkPosition);
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

                return blockCount;
            }
        }
    }
}
