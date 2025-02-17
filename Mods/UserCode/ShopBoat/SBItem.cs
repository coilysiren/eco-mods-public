namespace ShopBoat
{
    using System;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

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

        [NewTooltip(CacheAs.SubType, 50)]
        public static LocString UpdateTooltip() =>
            Localizer
                .Do(
                    $"{Localizer.DoStr("Increases")} total shelf life by: {Text.InfoLight(Text.Percent(1))}"
                )
                .Dash();

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object PersistentData { get; set; }
    }
}
