namespace EcoNil
{
    using Eco.Shared.Serialization;
    using Eco.Simulation.Time;
    using Eco.Simulation.WorldLayers;

    [Serialized]
    public class MoistureComponent : SharedComponent
    {
        private int radius;
        private float hydrationAdjustment;
        private float soilMoistureAverage;
        private float rainfallAverage;
        private double lastTick = 0;
        private readonly double timePeriodSeconds = 3600;

        public void Initialize(int radius, float hydrationAdjustment)
        {
            this.radius = radius;
            this.hydrationAdjustment = hydrationAdjustment;
            this.soilMoistureAverage = this.AverageLayerValue(this.radius, LayerNames.SoilMoisture);
            this.rainfallAverage = this.AverageLayerValue(this.radius, LayerNames.Rainfall);
        }

        public override void Tick()
        {
            base.Tick();
            this.Hydrate();
        }

        private void Hydrate()
        {
            // Run every < timePeriod > seconds
            if (this.Enabled && WorldTime.Seconds > this.lastTick + this.timePeriodSeconds)
            {
                // Do business logic
                this.AdjustLayers(
                    this.radius,
                    LayerNames.SoilMoisture,
                    this.hydrationAdjustment,
                    this.soilMoistureAverage
                );
                this.AdjustLayers(
                    this.radius,
                    LayerNames.Rainfall,
                    this.hydrationAdjustment,
                    this.rainfallAverage
                );
                // Record the current time so we don't act too often
                this.lastTick = WorldTime.Seconds;
            }
        }
    }
}
