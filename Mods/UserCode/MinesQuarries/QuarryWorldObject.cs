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

        static QuarryObject()
        {
            AddOccupancy<QuarryObject>(new List<BlockOccupancy>() { new(new Vector3i(3, 2, 3)) });
        }

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
    [LocDescription(
        "For the extraction of sandstone. Must be placed on a sandstone deposit. Doesn't exhaust the deposit."
    )]
    public partial class SandstoneQuarryItem : WorldObjectItem<SandstoneQuarryObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(LimestoneQuarryComponent))]
    public partial class LimestoneQuarryObject : QuarryObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(LimestoneQuarryItem);
        public override LocString DisplayName => Localizer.DoStr("Limestone Quarry");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    [LocDisplayName("Limestone Quarry")]
    [LocDescription(
        "For the extraction of limestone. Must be placed on a limestone deposit. Doesn't exhaust the deposit."
    )]
    public partial class LimestoneQuarryItem : WorldObjectItem<LimestoneQuarryObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(GraniteQuarryComponent))]
    public partial class GraniteQuarryObject : QuarryObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(GraniteQuarryItem);
        public override LocString DisplayName => Localizer.DoStr("Granite Quarry");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    [LocDisplayName("Granite Quarry")]
    [LocDescription(
        "For the extraction of granite. Must be placed on a granite deposit. Doesn't exhaust the deposit."
    )]
    public partial class GraniteQuarryItem : WorldObjectItem<GraniteQuarryObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(ShaleQuarryComponent))]
    public partial class ShaleQuarryObject : QuarryObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(ShaleQuarryItem);
        public override LocString DisplayName => Localizer.DoStr("Shale Quarry");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    [LocDisplayName("Shale Quarry")]
    [LocDescription(
        "For the extraction of shale. Must be placed on a shale deposit. Doesn't exhaust the deposit."
    )]
    public partial class ShaleQuarryItem : WorldObjectItem<ShaleQuarryObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
