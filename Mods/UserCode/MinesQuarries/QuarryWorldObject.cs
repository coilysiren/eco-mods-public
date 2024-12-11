namespace MinesQuarries
{
    using System;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [Tag("Usable")]
    public partial class QuarryObject : WorldObject
    {
        public override TableTextureMode TableTexture => TableTextureMode.Stone;

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25);
            storage.Storage.AddInvRestriction(new NoBuildingRestriction());
            storage.Storage.AddInvRestriction(new DiggableExcavatableRestriction());
        }
    }

    [Serialized]
    [RequireComponent(typeof(SandstoneQuarryComponent))]
    public partial class SandstoneQuarryObject : QuarryObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SandstoneQuarryItem);
        public override LocString DisplayName => Localizer.DoStr("Sandstone Quarry");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    [LocDisplayName("Sandstone Quarry")]
    [LocDescription("For the extraction of sandstone.")]
    public partial class SandstoneQuarryItem : WorldObjectItem<SandstoneQuarryObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
