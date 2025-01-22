namespace ShopBoat
{
    using System;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(20000)]
    [LocDisplayName("Shop Boat")]
    [LocDescription("A floating market stall for bringing your goods to the people.")]
    [IconGroup("World Object Minimap")]
    [WaterPlaceable]
    public partial class ShopBoatItem : WorldObjectItem<ShopBoatObject>, IPersistentData
    {
        public float InteractDistance => DefaultInteractDistance.WaterPlacement;

        public bool ShouldHighlight(Type block) => false;

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object PersistentData { get; set; }
    }
}
