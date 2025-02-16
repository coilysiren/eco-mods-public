namespace DirectCarbonCapture
{
    using System.ComponentModel;
    using BunWulfBioChemical;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]
    [Weight(10000)]
    [LocDisplayName("Direct Air Capture Fan")]
    [LocDescription(
        "Direct Air Capture removes carbon dioxide (CO2) directly from the ambient air, sucking it out of the atmosphere. The CO2 is then pumped into nearby porous rock, such as limestone. Do not use in enclosed spaces or near plant life."
    )]
    [IconGroup("World Object Minimap")]
    [AirPollution(-1)]
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
        public static readonly HomeFurnishingValue homeValue = new()
        {
            ObjectName = typeof(DirectCarbonCaptureObject).UILink(),
            Category = HousingConfig.GetRoomCategory("Industrial"),
            TypeForRoomLimit = Localizer.DoStr(""),
        };

        [NewTooltip(CacheAs.SubType, 7)]
        public static LocString PowerConsumptionTooltip() =>
            Localizer.Do(
                $"Consumes: {Text.Info(DirectCarbonCaptureObject.powerConsumption)}w of {new ElectricPower().Name} power."
            );

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
        "Carbon filters are used to remove carbon dioxide (CO2) from the air. They are used in direct air capture systems."
    )]
    [Fuel(1000000)]
    [Tag("Filter")]
    [RepairRequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class CarbonFilterItem : Item
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Carbon Filters");
    }

    [Serialized]
    [Category("Hidden")]
    [Tag("Filter")]
    [LocDisplayName("Filter")]
    [LocDescription(
        "Carbon filters are used to remove carbon dioxide (CO2) from the air. They are used in direct air capture systems."
    )]
    public partial class FilterItem : Item
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Filters");
    }
}
