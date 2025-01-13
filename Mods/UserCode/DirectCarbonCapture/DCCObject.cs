namespace DirectCarbonCapture
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Tag("Usable")]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [RepairRequiresSkill(typeof(SelfImprovementSkill), 5)]
    public partial class DirectCarbonCaptureObject : WorldObject, IRepresentsItem
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override LocString DisplayName => Localizer.DoStr("Direct Carbon Capture Pump Jack");
        public virtual Type RepresentedItemType => typeof(DirectCarbonCaptureItem);

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Power"));
            this.GetComponent<PowerConsumptionComponent>().Initialize(500);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<HousingComponent>().HomeValue = PumpJackItem.homeValue;
            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(CarbonFilterItem), Quantity = 1 },
                    }
                );
            this.GetComponent<AirPollutionComponent>().Initialize(-1);
        }
    }
}
