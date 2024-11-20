namespace WorldCounter
{
    using Eco.Core.Plugins.Interfaces;
    using Eco.Core.Utils;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
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
            Log.WriteLine(Localizer.DoStr("WorldCounter initializing"));

            IEnumerable<PersistentChunk> Chunks = World.Chunks;

            foreach (PersistentChunk chunk in Chunks)
            {
                Log.WriteLine(Localizer.DoStr($"Chunk Type: {chunk.GetType().Name}"));
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
