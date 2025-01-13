namespace DirectCarbonCapture
{
    using System;
    using System.Collections.Generic;
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
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

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
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [RepairRequiresSkill(typeof(SelfImprovementSkill), 5)]
    public partial class DirectCarbonCaptureObject : WorldObject, IRepresentsItem
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override LocString DisplayName => Localizer.DoStr("Direct Carbon Capture Pump Jack");
        public virtual Type RepresentedItemType => typeof(DirectCarbonCaptureItem);

        static DirectCarbonCaptureObject()
        {
            AddOccupancy<DirectCarbonCaptureObject>(
                new List<BlockOccupancy>()
                {
                    new(new Vector3i(0, 0, 0)),
                    new(new Vector3i(0, 0, 1)),
                    new(new Vector3i(0, 1, 0)),
                    new(new Vector3i(0, 1, 1)),
                    new(new Vector3i(0, 2, 0)),
                    new(new Vector3i(0, 2, 1)),
                    new(new Vector3i(0, 3, 0)),
                    new(new Vector3i(0, 3, 1)),
                    new(new Vector3i(1, 0, 0)),
                    new(new Vector3i(1, 0, 1)),
                    new(new Vector3i(1, 1, 0)),
                    new(new Vector3i(1, 1, 1)),
                    new(new Vector3i(1, 2, 0)),
                    new(new Vector3i(1, 2, 1)),
                    new(new Vector3i(1, 3, 0)),
                    new(new Vector3i(1, 3, 1)),
                    new(new Vector3i(2, 0, 0)),
                    new(new Vector3i(2, 0, 1)),
                    new(new Vector3i(2, 1, 0)),
                    new(new Vector3i(2, 1, 1)),
                    new(new Vector3i(2, 2, 0)),
                    new(new Vector3i(2, 2, 1)),
                    new(new Vector3i(2, 3, 0)),
                    new(new Vector3i(2, 3, 1)),
                    new(new Vector3i(3, 0, 0)),
                    new(new Vector3i(3, 0, 1)),
                    new(new Vector3i(3, 1, 0)),
                    new(new Vector3i(3, 1, 1)),
                    new(new Vector3i(3, 2, 0)),
                    new(new Vector3i(3, 2, 1)),
                    new(new Vector3i(3, 3, 0)),
                    new(new Vector3i(3, 3, 1)),
                    new(new Vector3i(4, 0, 0)),
                    new(new Vector3i(4, 0, 1)),
                    new(new Vector3i(4, 1, 0)),
                    new(new Vector3i(4, 1, 1)),
                    new(new Vector3i(4, 2, 0)),
                    new(new Vector3i(4, 2, 1)),
                    new(new Vector3i(4, 3, 0)),
                    new(new Vector3i(4, 3, 1)),
                }
            );
        }

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Power"));
            this.GetComponent<PowerConsumptionComponent>().Initialize(2000);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<HousingComponent>().HomeValue = PumpJackItem.homeValue;
            this.GetComponent<AirPollutionComponent>().Initialize(-1);
            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(CarbonFilterItem), Quantity = 10 },
                        new() { TypeName = nameof(PistonItem), Quantity = 1 },
                        new() { TypeName = nameof(BoilerItem), Quantity = 1 },
                    }
                );
        }
    }
}
