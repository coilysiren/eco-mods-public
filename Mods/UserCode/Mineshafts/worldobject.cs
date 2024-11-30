namespace Mineshafts
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

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [Tag("Usable")]
    public partial class MineshaftObject : WorldObject
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
    }

    [Serialized]
    [RequireComponent(typeof(CrudeIronMineshaftComponent))]
    public partial class CrudeIronMineshaftObject : MineshaftObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CrudeIronMineshaftItem);
        public override LocString DisplayName => Localizer.DoStr("Crude Iron Mineshaft");

        static CrudeIronMineshaftObject()
        {
            AddOccupancy<CrudeIronMineshaftObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    public partial class CrudeIronMineshaftItem : WorldObjectItem<CrudeIronMineshaftObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(IronMineshaftComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class IronMineshaftObject : MineshaftObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(IronMineshaftItem);
        public override LocString DisplayName => Localizer.DoStr("Iron Mineshaft");

        static IronMineshaftObject()
        {
            AddOccupancy<IronMineshaftObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }

        protected override void Initialize()
        {
            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    public partial class IronMineshaftItem : WorldObjectItem<IronMineshaftObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(CopperMineshaftComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class CopperMineshaftObject : MineshaftObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CopperMineshaftItem);
        public override LocString DisplayName => Localizer.DoStr("Copper Mineshaft");

        static CopperMineshaftObject()
        {
            AddOccupancy<CopperMineshaftObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }

        protected override void Initialize()
        {
            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    public partial class CopperMineshaftItem : WorldObjectItem<CopperMineshaftObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(GoldMineshaftComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class GoldMineshaftObject : MineshaftObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(GoldMineshaftItem);
        public override LocString DisplayName => Localizer.DoStr("Gold Mineshaft");

        static GoldMineshaftObject()
        {
            AddOccupancy<GoldMineshaftObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }

        protected override void Initialize()
        {
            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "BasicUpgrade" },
        ItemTypes = new[] { typeof(MiningBasicUpgradeItem) }
    )]
    public partial class GoldMineshaftItem : WorldObjectItem<GoldMineshaftObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
