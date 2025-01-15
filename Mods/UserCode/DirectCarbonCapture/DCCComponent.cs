namespace DirectCarbonCapture
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers;
    using Eco.Simulation.WorldLayers.Layers;
    using Eco.World;

    [Serialized]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class CarbonCaptureComponent : WorldObjectComponent, IChunkSubscriber
    {
        private readonly int radius = 25;

        public float UpdateFrequencySec => 1;

        public float MaxQueuedChunkUpdateTime => 60;

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public override void Initialize() => this.ClearPollution();

        public override void Tick() => this.ClearPollution();

        public void ChunksChanged() => this.ClearPollution();

        // Get a list of Vector3s that are within a 3d radius of the parent's position
        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            List<WrappedWorldPosition3i> positions = new();
            WrappedWorldPosition3i wrappedPosition = WrappedWorldPosition3i.Create(
                this.Parent.Position3i.X,
                this.Parent.Position3i.Y,
                this.Parent.Position3i.Z
            );

            for (int x = -this.radius; x <= this.radius; x++)
            {
                for (int z = -this.radius; z <= this.radius; z++)
                {
                    WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3i.Create(
                        wrappedPosition.X + x,
                        0,
                        wrappedPosition.Z + z
                    );
                    positions.Add(positionToCheck);
                }
            }

            List<Vector3i> plainPositions = new();
            foreach (WrappedWorldPosition3i position in positions)
            {
                plainPositions.Add(new Vector3i(position.X, position.Y, position.Z));
            }

            return plainPositions.AsEnumerable();
        }

        private void ClearPollution()
        {
            if (this.Enabled)
            {
                WorldLayer pollution = WorldLayerManager.Obj.GetLayer(
                    LayerNames.AirPollutionSpread
                );
                this.RelevantChunkPositions().ForEach(pos => pollution.SetAtWorldPos(pos.XZ, 0f));
                pollution.Modify();
            }
        }
    }
}
