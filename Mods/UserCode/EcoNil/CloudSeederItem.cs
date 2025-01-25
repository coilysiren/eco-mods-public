namespace EcoNil
{
    using Eco.Core.Controller;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]
    [Weight(10000)]
    [LocDisplayName("Cloud Seeder")]
    [LocDescription(
        "Uses silver nitrate particle dispersal to increase rainfall and soil moisture. Place 50 blocks apart for optimal coverage. The machine activates immediately upon placement. Repeated placements within a single area will enhance the affect."
    )]
    [IconGroup("World Object Minimap")]
    public partial class CloudSeederItem : WorldObjectItem<CloudSeederObject>, IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );

        [NewTooltip(CacheAs.SubType, 7)]
        public static LocString PowerConsumptionTooltip() =>
            Localizer.Do(
                $"Consumes: {Text.Info(CloudSeederObject.powerConsumption)}w of {new ElectricPower().Name} power."
            );

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object? PersistentData { get; set; }
    }
}
