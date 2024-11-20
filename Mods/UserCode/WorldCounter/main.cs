namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Utils;
    using Eco.World;

    public class BunWulfEducationalPluginEntrypoint
        : IModKitPlugin,
            IServerPlugin,
            IInitializablePlugin,
            IModInit
    {
        public static void Main() { }

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
