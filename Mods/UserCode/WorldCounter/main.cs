namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
    using Eco.Shared.Math;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.WorldGenerator;
    using Vector3 = System.Numerics.Vector3;

    public class BunWulfEducationalPluginEntrypoint : IWorldGenFeature, IModKitPlugin
    {
        public static void Main() { }

        public void Generate(Random seed, Vector3 voxelSize, WorldSettings settings)
        {
            Log.WriteLine(Localizer.DoStr("WorldCounter initializing"));

            IEnumerable<PersistentChunk> Chunks = World.Chunks;

            foreach (PersistentChunk chunk in Chunks)
            {
                Vector3i chunkPosition = chunk.Position;
                Block block = World.GetBlock(chunkPosition);
                Type type = block.GetType();
                Log.WriteLine(Localizer.DoStr($"block postion: {chunkPosition}"));
                Log.WriteLine(Localizer.DoStr($"block type: {type}"));
            }
        }

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
