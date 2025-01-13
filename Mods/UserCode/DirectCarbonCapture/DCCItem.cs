namespace DirectCarbonCapture
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Gold Mine")]
    [LocDescription(
        "For the extraction of gold ore. Must be placed above a gold deposit. Produces waste rock and pollution. Doesn't exhaust the deposit."
    )]
    public partial class DirectCarbonCaptureItem : WorldObjectItem<DirectCarbonCaptureObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
