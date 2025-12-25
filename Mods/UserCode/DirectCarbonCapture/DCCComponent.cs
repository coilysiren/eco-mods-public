namespace DirectCarbonCapture
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Simulation.Time;
    using Eco.Simulation.WorldLayers;
    using Eco.Simulation.WorldLayers.Layers;

    [Serialized]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class CarbonCaptureComponent : WorldObjectComponent
    {
        private int radius;
        private double lastCapture = 0;
        private float pollutionTonsPerHour;

        public float UpdateFrequencySec => 60;

        public float MaxQueuedChunkUpdateTime => 60;

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public void Initialize(float pollutionTonsPerHour, int radius)
        {
            this.pollutionTonsPerHour = pollutionTonsPerHour;
            this.radius = radius;
        }

        public override void Tick()
        {
            base.Tick();
            this.ClearPollution();
        }

        private void ClearPollution()
        {
            if (this.Enabled && WorldTime.Seconds > this.lastCapture + this.UpdateFrequencySec)
            {
                // Clear localized air pollution
                WorldLayer airPollution = WorldLayerManager.Obj.GetLayer(
                    LayerNames.AirPollutionSpread
                );
                this.RelevantPositions()
                    .ForEach(pos => airPollution.SetAtWorldPos(pos.XZ, 0.0001f));
                airPollution.Modify();
                // Lower global CO2 levels
                WorldLayerManager.Obj.ClimateSim.AddAirPollutionTons(
                    this.Parent.Position3i,
                    this.pollutionTonsPerHour * this.UpdateFrequencySec / 3600
                );
                // Record the current time so we don't clear pollution too often
                this.lastCapture = WorldTime.Seconds;
            }
        }

        public IEnumerable<Vector3i> RelevantPositions()
        {
            List<WrappedWorldPosition3i> positions = new();
            WrappedWorldPosition3i wrappedPosition = WrappedWorldPosition3i.Create(
                this.Parent.Position3i.X,
                0,
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
    }
}
