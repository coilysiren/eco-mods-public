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
        private int radius = 0;
        private double lastLocalCapture = 0;
        private double lastGlobalCapture = 0;
        private float pollutionChangePerHour = 0;
        private float targetPollution = 0.01f;

        public float MaxQueuedChunkUpdateTime => 60;

        public double QueuedChunkUpdateTime { get; set; }

        public double LastChunkUpdateTime { get; set; }

        public bool ResetUpdateTimeOnEveryChange { get; }

        public bool IgnorePlantUpdates { get; }

        public void Initialize(float pollutionChangePerHour, int radius)
        {
            this.pollutionChangePerHour = pollutionChangePerHour;
            this.radius = radius;
        }

        public override void Tick()
        {
            base.Tick();
            this.ClearLocalPollution();
            this.ClearGlobalPollution();
        }

        private void ClearLocalPollution()
        {
            if (WorldTime.Seconds <= this.lastLocalCapture + 60)
            {
                return;
            }

            if (!this.Enabled)
            {
                return;
            }

            List<Vector3i> positions = this.RelevantPositions().ToList();
            if (positions.Count == 0) // should never happen
            {
                this.lastLocalCapture = WorldTime.Seconds;
                return;
            }

            WorldLayer airPollution = WorldLayerManager.Obj.GetLayer(LayerNames.AirPollutionSpread);

            bool hasAllPositiveLocalPollution = positions.All(pos =>
                airPollution.GetValue(new LayerPosition(pos.XZ, 1)) > this.targetPollution
            );

            bool hasAnyNegativeLocalPollution = positions.Any(pos =>
                airPollution.GetValue(new LayerPosition(pos.XZ, 1)) < -this.targetPollution
            );

            bool pollutionShouldLower =
                hasAllPositiveLocalPollution && !hasAnyNegativeLocalPollution;

            if (pollutionShouldLower)
            {
                foreach (Vector3i pos in positions)
                {
                    airPollution.SetAtWorldPos(pos.XZ, this.targetPollution);
                }
                airPollution.Modify();
            }

            this.lastLocalCapture = WorldTime.Seconds;
        }

        private void ClearGlobalPollution()
        {
            if (WorldTime.Seconds <= this.lastGlobalCapture + 60)
            {
                return;
            }

            if (!this.Enabled)
            {
                return;
            }

            float pollutionChange = this.pollutionChangePerHour / 60f;
            WorldLayerManager.Obj.ClimateSim.AddAirPollutionTons(
                this.Parent.Position3i,
                pollutionChange // pollutionChange is always a negative number
            );

            this.lastGlobalCapture = WorldTime.Seconds;
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
