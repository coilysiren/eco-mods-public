namespace DirectCarbonCapture
{
    using Eco.Core.Controller;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]
    [Weight(10000)]
    [LocDisplayName("Direct Carbon Capture Pump Jack")]
    [LocDescription(
        "Direct carbon Capture removes carbon dioxide (CO2) directly from the ambient air, essentially sucking it out of the atmosphere, and storing it in geological formations."
    )]
    [IconGroup("World Object Minimap")]
    public partial class DirectCarbonCaptureItem
        : WorldObjectItem<DirectCarbonCaptureObject>,
            IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
        public override HomeFurnishingValue HomeValue => homeValue;
        public static readonly HomeFurnishingValue homeValue =
            new()
            {
                ObjectName = typeof(DirectCarbonCaptureObject).UILink(),
                Category = HousingConfig.GetRoomCategory("Industrial"),
                TypeForRoomLimit = Localizer.DoStr(""),
            };

        [NewTooltip(CacheAs.SubType, 7)]
        public static LocString PowerConsumptionTooltip() =>
            Localizer.Do($"Consumes: {Text.Info(500)}w of {new ElectricPower().Name} power.");

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object? PersistentData { get; set; }
    }

    [Serialized]
    [Weight(1)]
    [LocDisplayName("Carbon Filter")]
    [LocDescription(
        "Carbon filters are used to remove carbon dioxide (CO2) from the air. They are used in direct carbon capture systems."
    )]
    [RepairRequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class CarbonFilterItem : PartItem
    {
        private static SkillModifiedValue skilledRepairCost =
            new(
                2,
                BiochemistSkill.MultiplicativeStrategy,
                typeof(BiochemistSkill),
                typeof(CarbonFilterItem),
                Localizer.DoStr("repair cost"),
                DynamicValueType.Efficiency
            );
        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public float ReduceMaxDurabilityByPercent => 0.05f;
    }
}
