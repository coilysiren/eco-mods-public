namespace DirectCarbonCapture
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers;
    using Eco.Simulation.WorldLayers.Layers;
    using Eco.World;

    public class CarbonCaptureComponent : WorldObjectComponent, IChunkSubscriber
    {
        private readonly int radius = 10;

        public float UpdateFrequencySec => 60;

        public float MaxQueuedChunkUpdateTime => 300f;

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public override void Initialize() => this.ClearPollution();

        public void ChunksChanged() => this.ClearPollution();

        // Get a list of Vector3s that are within a 3d radius of the parent's position
        public IEnumerable<Vector3i> RelevantChunkPositions()
        {
            bool maximumYAxis = true;
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
                    Vector2i XZ = new(x, z);
                    for (
                        int y = maximumYAxis ? 0 : -this.radius;
                        y <= (maximumYAxis ? World.GetTopBlockY(XZ) : this.radius);
                        y++
                    )
                    {
                        WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3i.Create(
                            wrappedPosition.X + x,
                            maximumYAxis ? y : wrappedPosition.Y + y,
                            wrappedPosition.Z + z
                        );
                        positions.Add(positionToCheck);
                    }
                }
            }
            return (IEnumerable<Vector3i>)positions;
        }

        private void ClearPollution()
        {
            WorldLayer pollution = WorldLayerManager.Obj.GetLayer(LayerNames.GroundPollutionSpread);
            this.RelevantChunkPositions().ForEach(pos => pollution.SetAtWorldPos(pos.XZ, 0f));
            pollution.Modify();
        }
    }
}
