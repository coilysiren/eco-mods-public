namespace WorldCounter
{
    using System;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
    using Eco.World;
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
                Log.WriteLine(Localizer.DoStr($"Chunk Type: {chunk.GetType().Name}"));
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
