namespace Mines
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
    [RequireComponent(typeof(AirPollutionComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [Tag("Usable")]
    public partial class MineObject : WorldObject
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Crafting"));

            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(50); // 2x a stockpile

            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(GearboxItem), Quantity = 4 },
                    }
                );

            this.GetComponent<AirPollutionComponent>()
                .Initialize(this.GetComponent<MineComponent>());
        }
    }

    [Serialized]
    [RequireComponent(typeof(IronMineComponent))]
    public partial class IronMineObject : MineObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(IronMineItem);
        public override LocString DisplayName => Localizer.DoStr("Iron Mine");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    [LocDisplayName("Iron Mine")]
    [LocDescription("For the extraction of iron ore.")]
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
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    [LocDisplayName("Copper Mine")]
    [LocDescription("For the extraction of copper ore.")]
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
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    [LocDisplayName("Gold Mine")]
    [LocDescription("For the extraction of gold ore.")]
    public partial class GoldMineItem : WorldObjectItem<GoldMineObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }

    [Serialized]
    [RequireComponent(typeof(CoalMineComponent))]
    public partial class CoalMineObject : MineObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CoalMineItem);
        public override LocString DisplayName => Localizer.DoStr("Coal Mine");
    }

    [Serialized]
    [AllowPluginModules(
        Tags = new[] { "AdvancedUpgrade" },
        ItemTypes = new[] { typeof(MiningAdvancedUpgradeItem) }
    )]
    [LocDisplayName("Coal Mine")]
    [LocDescription("For the extraction of coal.")]
    public partial class CoalMineItem : WorldObjectItem<CoalMineObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
