namespace DirectCarbonCapture
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation.Settings;

    [Serialized]
    [Tag("Usable")]
    [RequireComponent(typeof(CarbonCaptureComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [RepairRequiresSkill(typeof(SelfImprovementSkill), 5)]
    public partial class DirectCarbonCaptureObject : WorldObject, IRepresentsItem
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override LocString DisplayName => Localizer.DoStr("Direct Air Capture Pump");
        public virtual Type RepresentedItemType => typeof(DirectCarbonCaptureItem);
        private static readonly string[] fuelTagList = new[] { "Filter" }; //noloc

        // Balancing Configuration

        // The DCC decomes less effective at higher pollution multipliers!
        // At 1x pollution multiplier, 2 DCC can offset 1 combustion generator.
        // At 2x pollution is 3 DCC to 1 combustion generator.
        public static readonly float pollutionTonsPerHour = (float)(
            -1 * Math.Sqrt(EcoDef.Obj.ClimateSettings.PollutionMultiplier)
        );

        // This range should should be roughly 2x the size of the pollution spread radius.
        public static readonly int pollutionClearRadius = 20;

        // The DCC should use significant % of the power a combustion generator produces.
        // Something like 1/10th is probably a good target.
        public static readonly int powerConsumption = 1000;

        // Controls how frequently the DCC burns through carbon filters.
        public static readonly int joulesPerSecond = 25;

        static DirectCarbonCaptureObject()
        {
            AddOccupancy<DirectCarbonCaptureObject>(
                new List<BlockOccupancy>()
                {
                    new(new Vector3i(0, 0, 0)),
                    new(new Vector3i(1, 0, 0)),
                    new(new Vector3i(1, 0, 1)),
                    new(new Vector3i(0, 0, 1)),
                    new(new Vector3i(0, 1, 1)),
                    new(new Vector3i(1, 1, 1)),
                    new(new Vector3i(1, 1, 0)),
                    new(new Vector3i(0, 1, 0)),
                    new(new Vector3i(0, 2, 0)),
                    new(new Vector3i(1, 2, 0)),
                    new(new Vector3i(1, 2, 1)),
                    new(new Vector3i(0, 2, 1)),
                    new(new Vector3i(0, 3, 1)),
                    new(new Vector3i(1, 3, 1)),
                    new(new Vector3i(1, 3, 0)),
                    new(new Vector3i(0, 3, 0)),
                }
            );
        }

        protected override void Initialize()
        {
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(joulesPerSecond);
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Power"));
            this.GetComponent<PowerConsumptionComponent>().Initialize(powerConsumption);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<PowerGridComponent>().DurabilityUsedPerHourOfUse = 1;
            this.GetComponent<HousingComponent>().HomeValue = DirectCarbonCaptureItem.homeValue;
            this.GetComponent<AirPollutionComponent>().Initialize(pollutionTonsPerHour);
            this.GetComponent<CarbonCaptureComponent>()
                .Initialize(pollutionTonsPerHour, pollutionClearRadius);
            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(PistonItem), Quantity = 4 },
                        new() { TypeName = nameof(BoilerItem), Quantity = 1 },
                    }
                );
        }

        public override void Tick()
        {
            base.Tick();
            this.SetAnimatedState("Operating", this.Operating);
        }
    }
}
