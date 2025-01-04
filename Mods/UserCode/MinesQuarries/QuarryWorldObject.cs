namespace MinesQuarries
{
    using System;
    using System.Collections.Generic;
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
    using Eco.Shared.SharedTypes;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [Tag("Usable")]
    public partial class QuarryObject : WorldObject
    {
        public static readonly Vector3i DefaultDim = new(3, 2, 3);
        public override TableTextureMode TableTexture => TableTextureMode.Stone;
        public override InteractionTargetPriority TargetPriority => InteractionTargetPriority.High;

        public static List<BlockOccupancy> blockOccupancies =
            new()
            {
                // first layer, 3x1x3 @ y=0
                // row 1, z=0
                new(new Vector3i(0, 0, 0)),
                new(new Vector3i(1, 0, 0)),
                new(new Vector3i(2, 0, 0)),
                // row 2, z=1
                new(new Vector3i(2, 0, 1)),
                new(new Vector3i(1, 0, 1)),
                new(new Vector3i(0, 0, 1)),
                // row 3, z=2
                new(new Vector3i(0, 0, 2)),
                new(new Vector3i(1, 0, 2)),
                new(new Vector3i(2, 0, 2)),
                // second layer, 3x1x3 @ y=1
                // row 3, z=2
                new(new Vector3i(2, 1, 2)),
                new(new Vector3i(1, 1, 2)),
                new(new Vector3i(0, 1, 2)),
                // row 2, z=1
                new(new Vector3i(0, 1, 1)),
                new(new Vector3i(1, 1, 1)),
                new(new Vector3i(2, 1, 1)),
                // row 1, z=0
                new(new Vector3i(2, 1, 0)),
                new(new Vector3i(1, 1, 0)),
                new(new Vector3i(0, 1, 0)),
            };

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25);
            storage.Storage.AddInvRestriction(new NoBuildingRestriction());
            storage.Storage.AddInvRestriction(new ExcavatableRestriction());
        }
    }

    [Serialized]
    [RequireComponent(typeof(SandstoneQuarryComponent))]
    public partial class SandstoneQuarryObject : QuarryObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SandstoneQuarryItem);
        public override LocString DisplayName => Localizer.DoStr("Sandstone Quarry");

        static SandstoneQuarryObject()
        {
            AddOccupancy<SandstoneQuarryObject>(blockOccupancies);
        }
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

        static LimestoneQuarryObject()
        {
            AddOccupancy<LimestoneQuarryObject>(blockOccupancies);
        }
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

        static GraniteQuarryObject()
        {
            AddOccupancy<GraniteQuarryObject>(blockOccupancies);
        }
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

        static ShaleQuarryObject()
        {
            AddOccupancy<ShaleQuarryObject>(blockOccupancies);
        }
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
