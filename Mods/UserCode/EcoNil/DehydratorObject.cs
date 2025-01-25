namespace EcoNil
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
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
    [RequireComponent(typeof(MoistureComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [RepairRequiresSkill(typeof(SelfImprovementSkill), 5)]
    public partial class DehydratorObject : WorldObject, IRepresentsItem
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override LocString DisplayName => Localizer.DoStr("Dehydrator");
        public virtual Type RepresentedItemType => typeof(DehydratorItem);

        // Balancing Configuration

        public static readonly int radius = 50;
        public static readonly float hydrationAdjustment = -0.05f;
        public static readonly int powerConsumption = 1000;

        static DehydratorObject()
        {
            AddOccupancy<DehydratorObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Power"));
            this.GetComponent<PowerConsumptionComponent>().Initialize(powerConsumption);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<MoistureComponent>().Initialize(radius, hydrationAdjustment);
        }

        public override void Tick()
        {
            base.Tick();
            this.SetAnimatedState("Operating", this.Operating);
        }
    }
}
