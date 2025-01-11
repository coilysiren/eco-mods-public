namespace MinesQuarries
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.SharedTypes;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(StockpileComponent))]
    [RequireComponent(typeof(WorldStockpileComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [Tag("Usable")]
    public partial class PitObject : WorldObject
    {
        public static readonly Vector3i DefaultDim = new(5, 5, 5);
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public override InteractionTargetPriority TargetPriority => InteractionTargetPriority.High;

        public static List<BlockOccupancy> blockOccupancies =
            new()
            {
                new BlockOccupancy(new Vector3i(-2, 0, -2)),
                new BlockOccupancy(new Vector3i(-2, 0, -1)),
                new BlockOccupancy(new Vector3i(-2, 0, 0)),
                new BlockOccupancy(new Vector3i(-2, 0, 1)),
                new BlockOccupancy(new Vector3i(-2, 0, 2)),
                new BlockOccupancy(new Vector3i(-2, 1, -2)),
                new BlockOccupancy(new Vector3i(-2, 1, -1)),
                new BlockOccupancy(new Vector3i(-2, 1, 0)),
                new BlockOccupancy(new Vector3i(-2, 1, 1)),
                new BlockOccupancy(new Vector3i(-2, 1, 2)),
                new BlockOccupancy(new Vector3i(-2, 2, -2)),
                new BlockOccupancy(new Vector3i(-2, 2, -1)),
                new BlockOccupancy(new Vector3i(-2, 2, 0)),
                new BlockOccupancy(new Vector3i(-2, 2, 1)),
                new BlockOccupancy(new Vector3i(-2, 2, 2)),
                new BlockOccupancy(new Vector3i(-2, 3, -2)),
                new BlockOccupancy(new Vector3i(-2, 3, -1)),
                new BlockOccupancy(new Vector3i(-2, 3, 0)),
                new BlockOccupancy(new Vector3i(-2, 3, 1)),
                new BlockOccupancy(new Vector3i(-2, 3, 2)),
                new BlockOccupancy(new Vector3i(-2, 4, -2)),
                new BlockOccupancy(new Vector3i(-2, 4, -1)),
                new BlockOccupancy(new Vector3i(-2, 4, 0)),
                new BlockOccupancy(new Vector3i(-2, 4, 1)),
                new BlockOccupancy(new Vector3i(-2, 4, 2)),
                new BlockOccupancy(new Vector3i(-1, 0, -2)),
                new BlockOccupancy(new Vector3i(-1, 0, -1)),
                new BlockOccupancy(new Vector3i(-1, 0, 0)),
                new BlockOccupancy(new Vector3i(-1, 0, 1)),
                new BlockOccupancy(new Vector3i(-1, 0, 2)),
                new BlockOccupancy(new Vector3i(-1, 1, -2)),
                new BlockOccupancy(new Vector3i(-1, 1, -1)),
                new BlockOccupancy(new Vector3i(-1, 1, 0)),
                new BlockOccupancy(new Vector3i(-1, 1, 1)),
                new BlockOccupancy(new Vector3i(-1, 1, 2)),
                new BlockOccupancy(new Vector3i(-1, 2, -2)),
                new BlockOccupancy(new Vector3i(-1, 2, -1)),
                new BlockOccupancy(new Vector3i(-1, 2, 0)),
                new BlockOccupancy(new Vector3i(-1, 2, 1)),
                new BlockOccupancy(new Vector3i(-1, 2, 2)),
                new BlockOccupancy(new Vector3i(-1, 3, -2)),
                new BlockOccupancy(new Vector3i(-1, 3, -1)),
                new BlockOccupancy(new Vector3i(-1, 3, 0)),
                new BlockOccupancy(new Vector3i(-1, 3, 1)),
                new BlockOccupancy(new Vector3i(-1, 3, 2)),
                new BlockOccupancy(new Vector3i(-1, 4, -2)),
                new BlockOccupancy(new Vector3i(-1, 4, -1)),
                new BlockOccupancy(new Vector3i(-1, 4, 0)),
                new BlockOccupancy(new Vector3i(-1, 4, 1)),
                new BlockOccupancy(new Vector3i(-1, 4, 2)),
                new BlockOccupancy(new Vector3i(0, 0, -2)),
                new BlockOccupancy(new Vector3i(0, 0, -1)),
                new BlockOccupancy(new Vector3i(0, 0, 0)),
                new BlockOccupancy(new Vector3i(0, 0, 1)),
                new BlockOccupancy(new Vector3i(0, 0, 2)),
                new BlockOccupancy(new Vector3i(0, 1, -2)),
                new BlockOccupancy(new Vector3i(0, 1, -1)),
                new BlockOccupancy(new Vector3i(0, 1, 0)),
                new BlockOccupancy(new Vector3i(0, 1, 1)),
                new BlockOccupancy(new Vector3i(0, 1, 2)),
                new BlockOccupancy(new Vector3i(0, 2, -2)),
                new BlockOccupancy(new Vector3i(0, 2, -1)),
                new BlockOccupancy(new Vector3i(0, 2, 0)),
                new BlockOccupancy(new Vector3i(0, 2, 1)),
                new BlockOccupancy(new Vector3i(0, 2, 2)),
                new BlockOccupancy(new Vector3i(0, 3, -2)),
                new BlockOccupancy(new Vector3i(0, 3, -1)),
                new BlockOccupancy(new Vector3i(0, 3, 0)),
                new BlockOccupancy(new Vector3i(0, 3, 1)),
                new BlockOccupancy(new Vector3i(0, 3, 2)),
                new BlockOccupancy(new Vector3i(0, 4, -2)),
                new BlockOccupancy(new Vector3i(0, 4, -1)),
                new BlockOccupancy(new Vector3i(0, 4, 0)),
                new BlockOccupancy(new Vector3i(0, 4, 1)),
                new BlockOccupancy(new Vector3i(0, 4, 2)),
                new BlockOccupancy(new Vector3i(1, 0, -2)),
                new BlockOccupancy(new Vector3i(1, 0, -1)),
                new BlockOccupancy(new Vector3i(1, 0, 0)),
                new BlockOccupancy(new Vector3i(1, 0, 1)),
                new BlockOccupancy(new Vector3i(1, 0, 2)),
                new BlockOccupancy(new Vector3i(1, 1, -2)),
                new BlockOccupancy(new Vector3i(1, 1, -1)),
                new BlockOccupancy(new Vector3i(1, 1, 0)),
                new BlockOccupancy(new Vector3i(1, 1, 1)),
                new BlockOccupancy(new Vector3i(1, 1, 2)),
                new BlockOccupancy(new Vector3i(1, 2, -2)),
                new BlockOccupancy(new Vector3i(1, 2, -1)),
                new BlockOccupancy(new Vector3i(1, 2, 0)),
                new BlockOccupancy(new Vector3i(1, 2, 1)),
                new BlockOccupancy(new Vector3i(1, 2, 2)),
                new BlockOccupancy(new Vector3i(1, 3, -2)),
                new BlockOccupancy(new Vector3i(1, 3, -1)),
                new BlockOccupancy(new Vector3i(1, 3, 0)),
                new BlockOccupancy(new Vector3i(1, 3, 1)),
                new BlockOccupancy(new Vector3i(1, 3, 2)),
                new BlockOccupancy(new Vector3i(1, 4, -2)),
                new BlockOccupancy(new Vector3i(1, 4, -1)),
                new BlockOccupancy(new Vector3i(1, 4, 0)),
                new BlockOccupancy(new Vector3i(1, 4, 1)),
                new BlockOccupancy(new Vector3i(1, 4, 2)),
                new BlockOccupancy(new Vector3i(2, 0, -2)),
                new BlockOccupancy(new Vector3i(2, 0, -1)),
                new BlockOccupancy(new Vector3i(2, 0, 0)),
                new BlockOccupancy(new Vector3i(2, 0, 1)),
                new BlockOccupancy(new Vector3i(2, 0, 2)),
                new BlockOccupancy(new Vector3i(2, 1, -2)),
                new BlockOccupancy(new Vector3i(2, 1, -1)),
                new BlockOccupancy(new Vector3i(2, 1, 0)),
                new BlockOccupancy(new Vector3i(2, 1, 1)),
                new BlockOccupancy(new Vector3i(2, 1, 2)),
                new BlockOccupancy(new Vector3i(2, 2, -2)),
                new BlockOccupancy(new Vector3i(2, 2, -1)),
                new BlockOccupancy(new Vector3i(2, 2, 0)),
                new BlockOccupancy(new Vector3i(2, 2, 1)),
                new BlockOccupancy(new Vector3i(2, 2, 2)),
                new BlockOccupancy(new Vector3i(2, 3, -2)),
                new BlockOccupancy(new Vector3i(2, 3, -1)),
                new BlockOccupancy(new Vector3i(2, 3, 0)),
                new BlockOccupancy(new Vector3i(2, 3, 1)),
                new BlockOccupancy(new Vector3i(2, 3, 2)),
                new BlockOccupancy(new Vector3i(2, 4, -2)),
                new BlockOccupancy(new Vector3i(2, 4, -1)),
                new BlockOccupancy(new Vector3i(2, 4, 0)),
                new BlockOccupancy(new Vector3i(2, 4, 1)),
                new BlockOccupancy(new Vector3i(2, 4, 2)),
            };

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25);
            storage.Storage.AddInvRestriction(this.InventoryRestrictionImpl());
        }

        protected virtual InventoryRestriction? InventoryRestrictionImpl() => null;

        protected override void OnCreatePostInitialize()
        {
            base.OnCreatePostInitialize();
            StockpileComponent.ClearPlacementArea(
                this.Creator,
                this.Position3i,
                DefaultDim,
                this.Rotation
            );
        }
    }

    [Serialized]
    [RequireComponent(typeof(DirtPitComponent))]
    public partial class DirtPitObject : PitObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(DirtPitItem);
        public override LocString DisplayName => Localizer.DoStr("Dirt Pit");

        protected override InventoryRestriction InventoryRestrictionImpl() => new DirtRestriction();

        static DirtPitObject()
        {
            AddOccupancy<DirtPitObject>(blockOccupancies);
        }
    }

    [Serialized]
    [LocDisplayName("Dirt Pit")]
    [LocDescription(
        "For the extraction of Dirt. Must be placed on a Dirt deposit. Doesn't exhaust the deposit."
    )]
    public partial class DirtPitItem : WorldObjectItem<DirtPitObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(SandPitComponent))]
    public partial class SandPitObject : PitObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(SandPitItem);
        public override LocString DisplayName => Localizer.DoStr("Sand Pit");

        protected override InventoryRestriction InventoryRestrictionImpl() => new SandRestriction();

        static SandPitObject()
        {
            AddOccupancy<SandPitObject>(blockOccupancies);
        }
    }

    [Serialized]
    [LocDisplayName("Sand Pit")]
    [LocDescription(
        "For the extraction of Sand. Must be placed on a Sand deposit. Doesn't exhaust the deposit."
    )]
    public partial class SandPitItem : WorldObjectItem<SandPitObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(ClayPitComponent))]
    public partial class ClayPitObject : PitObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(ClayPitItem);
        public override LocString DisplayName => Localizer.DoStr("Clay Pit");

        protected override InventoryRestriction InventoryRestrictionImpl() => new ClayRestriction();

        static ClayPitObject()
        {
            AddOccupancy<ClayPitObject>(blockOccupancies);
        }
    }

    [Serialized]
    [LocDisplayName("Clay Pit")]
    [LocDescription(
        "For the extraction of Clay. Must be placed on a Clay deposit. Doesn't exhaust the deposit."
    )]
    public partial class ClayPitItem : WorldObjectItem<ClayPitObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
