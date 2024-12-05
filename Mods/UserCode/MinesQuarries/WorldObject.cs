namespace Mines
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
    using Eco.Gameplay.Skills;
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
    [RequireComponent(typeof(PartsComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [Tag("Usable")]
    public partial class MineObject : WorldObject
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
    }

    // [Serialized]
    // [RequireComponent(typeof(CrudeIronMineComponent))]
    // public partial class CrudeIronMineObject : MineObject, IRepresentsItem
    // {
    //     public virtual Type RepresentedItemType => typeof(CrudeIronMineItem);
    //     public override LocString DisplayName => Localizer.DoStr("Crude Iron Mine");

    //     static CrudeIronMineObject()
    //     {
    //         AddOccupancy<CrudeIronMineObject>(
    //             new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
    //         );
    //     }
    // }

    // [Serialized]
    // [AllowPluginModules(
    //     Tags = new[] { "AdvancedUpgrade" },
    //     ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    // )]
    // public partial class CrudeIronMineItem : WorldObjectItem<CrudeIronMineObject>
    // {
    //     protected override OccupancyContext GetOccupancyContext =>
    //         new SideAttachedContext(
    //             0 | DirectionAxisFlags.Down,
    //             WorldObject.GetOccupancyInfo(this.WorldObjectType)
    //         );
    // }

    [Serialized]
    [RequireComponent(typeof(IronMineComponent))]
    public partial class IronMineObject : MineObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(IronMineItem);
        public override LocString DisplayName => Localizer.DoStr("Iron Mine");

        static IronMineObject()
        {
            AddOccupancy<IronMineObject>(new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) });
        }

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile

            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(GearboxItem), Quantity = 4 },
                    }
                );
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    public partial class IronMineItem : WorldObjectItem<IronMineObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(CopperMineComponent))]
    public partial class CopperMineObject : MineObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CopperMineItem);
        public override LocString DisplayName => Localizer.DoStr("Copper Mine");

        static CopperMineObject()
        {
            AddOccupancy<CopperMineObject>(
                new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) }
            );
        }

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile

            PartsComponent parts = this.GetComponent<PartsComponent>();
            parts.Config(
                () => LocString.Empty,
                new PartsComponent.PartInfo[]
                {
                    new() { TypeName = nameof(GearboxItem), Quantity = 4 },
                }
            );
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    public partial class CopperMineItem : WorldObjectItem<CopperMineObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(GoldMineComponent))]
    public partial class GoldMineObject : MineObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(GoldMineItem);
        public override LocString DisplayName => Localizer.DoStr("Gold Mine");

        static GoldMineObject()
        {
            AddOccupancy<GoldMineObject>(new List<BlockOccupancy>() { new(new Vector3i(0, 0, 0)) });
        }

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(25); // same as a stockpile

            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(GearboxItem), Quantity = 4 },
                    }
                );
        }
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    public partial class GoldMineItem : WorldObjectItem<GoldMineObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
