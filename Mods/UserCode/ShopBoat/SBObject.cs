namespace ShopBoat
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Gameplay.Aliases;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Components.Store;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.GameActions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Property;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Serialization;

    [Serialized]
    [Tag("Usable")]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(BoatComponent))]
    [RequireComponent(typeof(LadderComponent))]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(StoreComponent))]
    public partial class ShopBoatObject : PhysicsWorldObject, INullCurrencyAllowed, ICanOverrideAuth
    {
        public NetPhysicsEntity NetEntity => (NetPhysicsEntity)this.netEntity;
        public override TableTextureMode TableTexture => TableTextureMode.Wood;
        public override float InteractDistance => DefaultInteractDistance.WaterPlacement;
        public override LocString DisplayName => Localizer.DoStr("Shop Boat");
        public virtual Type RepresentedItemType => typeof(ShopBoatItem);
        public override bool PlacesBlocks => false;
        private static readonly string[] fuelTagList = new string[] { "Burnable Fuel" };

        static ShopBoatObject()
        {
            AddOccupancy<ShopBoatObject>(
                new List<BlockOccupancy>()
                {
                    new BlockOccupancy(new Vector3i(-1, 0, -3)),
                    new BlockOccupancy(new Vector3i(-1, 0, -2)),
                    new BlockOccupancy(new Vector3i(-1, 0, -1)),
                    new BlockOccupancy(new Vector3i(-1, 0, 0)),
                    new BlockOccupancy(new Vector3i(-1, 0, 1)),
                    new BlockOccupancy(new Vector3i(-1, 0, 2)),
                    new BlockOccupancy(new Vector3i(-1, 0, 3)),
                    new BlockOccupancy(new Vector3i(-1, 0, 4)),
                    new BlockOccupancy(new Vector3i(-1, 1, -3)),
                    new BlockOccupancy(new Vector3i(-1, 1, -2)),
                    new BlockOccupancy(new Vector3i(-1, 1, -1)),
                    new BlockOccupancy(new Vector3i(-1, 1, 0)),
                    new BlockOccupancy(new Vector3i(-1, 1, 1)),
                    new BlockOccupancy(new Vector3i(-1, 1, 2)),
                    new BlockOccupancy(new Vector3i(-1, 1, 3)),
                    new BlockOccupancy(new Vector3i(-1, 1, 4)),
                    new BlockOccupancy(new Vector3i(-1, 2, -3)),
                    new BlockOccupancy(new Vector3i(-1, 2, -2)),
                    new BlockOccupancy(new Vector3i(-1, 2, -1)),
                    new BlockOccupancy(new Vector3i(-1, 2, 0)),
                    new BlockOccupancy(new Vector3i(-1, 2, 1)),
                    new BlockOccupancy(new Vector3i(-1, 2, 2)),
                    new BlockOccupancy(new Vector3i(-1, 2, 3)),
                    new BlockOccupancy(new Vector3i(-1, 2, 4)),
                    new BlockOccupancy(new Vector3i(-1, 3, -3)),
                    new BlockOccupancy(new Vector3i(-1, 3, -2)),
                    new BlockOccupancy(new Vector3i(-1, 3, -1)),
                    new BlockOccupancy(new Vector3i(-1, 3, 0)),
                    new BlockOccupancy(new Vector3i(-1, 3, 1)),
                    new BlockOccupancy(new Vector3i(-1, 3, 2)),
                    new BlockOccupancy(new Vector3i(-1, 3, 3)),
                    new BlockOccupancy(new Vector3i(-1, 3, 4)),
                    new BlockOccupancy(new Vector3i(0, 0, -3)),
                    new BlockOccupancy(new Vector3i(0, 0, -2)),
                    new BlockOccupancy(new Vector3i(0, 0, -1)),
                    new BlockOccupancy(new Vector3i(0, 0, 0)),
                    new BlockOccupancy(new Vector3i(0, 0, 1)),
                    new BlockOccupancy(new Vector3i(0, 0, 2)),
                    new BlockOccupancy(new Vector3i(0, 0, 3)),
                    new BlockOccupancy(new Vector3i(0, 0, 4)),
                    new BlockOccupancy(new Vector3i(0, 1, -3)),
                    new BlockOccupancy(new Vector3i(0, 1, -2)),
                    new BlockOccupancy(new Vector3i(0, 1, -1)),
                    new BlockOccupancy(new Vector3i(0, 1, 0)),
                    new BlockOccupancy(new Vector3i(0, 1, 1)),
                    new BlockOccupancy(new Vector3i(0, 1, 2)),
                    new BlockOccupancy(new Vector3i(0, 1, 3)),
                    new BlockOccupancy(new Vector3i(0, 1, 4)),
                    new BlockOccupancy(new Vector3i(0, 2, -3)),
                    new BlockOccupancy(new Vector3i(0, 2, -2)),
                    new BlockOccupancy(new Vector3i(0, 2, -1)),
                    new BlockOccupancy(new Vector3i(0, 2, 0)),
                    new BlockOccupancy(new Vector3i(0, 2, 1)),
                    new BlockOccupancy(new Vector3i(0, 2, 2)),
                    new BlockOccupancy(new Vector3i(0, 2, 3)),
                    new BlockOccupancy(new Vector3i(0, 2, 4)),
                    new BlockOccupancy(new Vector3i(0, 3, -3)),
                    new BlockOccupancy(new Vector3i(0, 3, -2)),
                    new BlockOccupancy(new Vector3i(0, 3, -1)),
                    new BlockOccupancy(new Vector3i(0, 3, 0)),
                    new BlockOccupancy(new Vector3i(0, 3, 1)),
                    new BlockOccupancy(new Vector3i(0, 3, 2)),
                    new BlockOccupancy(new Vector3i(0, 3, 3)),
                    new BlockOccupancy(new Vector3i(0, 3, 4)),
                    new BlockOccupancy(new Vector3i(1, 0, -3)),
                    new BlockOccupancy(new Vector3i(1, 0, -2)),
                    new BlockOccupancy(new Vector3i(1, 0, -1)),
                    new BlockOccupancy(new Vector3i(1, 0, 0)),
                    new BlockOccupancy(new Vector3i(1, 0, 1)),
                    new BlockOccupancy(new Vector3i(1, 0, 2)),
                    new BlockOccupancy(new Vector3i(1, 0, 3)),
                    new BlockOccupancy(new Vector3i(1, 0, 4)),
                    new BlockOccupancy(new Vector3i(1, 1, -3)),
                    new BlockOccupancy(new Vector3i(1, 1, -2)),
                    new BlockOccupancy(new Vector3i(1, 1, -1)),
                    new BlockOccupancy(new Vector3i(1, 1, 0)),
                    new BlockOccupancy(new Vector3i(1, 1, 1)),
                    new BlockOccupancy(new Vector3i(1, 1, 2)),
                    new BlockOccupancy(new Vector3i(1, 1, 3)),
                    new BlockOccupancy(new Vector3i(1, 1, 4)),
                    new BlockOccupancy(new Vector3i(1, 2, -3)),
                    new BlockOccupancy(new Vector3i(1, 2, -2)),
                    new BlockOccupancy(new Vector3i(1, 2, -1)),
                    new BlockOccupancy(new Vector3i(1, 2, 0)),
                    new BlockOccupancy(new Vector3i(1, 2, 1)),
                    new BlockOccupancy(new Vector3i(1, 2, 2)),
                    new BlockOccupancy(new Vector3i(1, 2, 3)),
                    new BlockOccupancy(new Vector3i(1, 2, 4)),
                    new BlockOccupancy(new Vector3i(1, 3, -3)),
                    new BlockOccupancy(new Vector3i(1, 3, -2)),
                    new BlockOccupancy(new Vector3i(1, 3, -1)),
                    new BlockOccupancy(new Vector3i(1, 3, 0)),
                    new BlockOccupancy(new Vector3i(1, 3, 1)),
                    new BlockOccupancy(new Vector3i(1, 3, 2)),
                    new BlockOccupancy(new Vector3i(1, 3, 3)),
                    new BlockOccupancy(new Vector3i(1, 3, 4)),
                    new BlockOccupancy(new Vector3i(2, 0, -3)),
                    new BlockOccupancy(new Vector3i(2, 0, -2)),
                    new BlockOccupancy(new Vector3i(2, 0, -1)),
                    new BlockOccupancy(new Vector3i(2, 0, 0)),
                    new BlockOccupancy(new Vector3i(2, 0, 1)),
                    new BlockOccupancy(new Vector3i(2, 0, 2)),
                    new BlockOccupancy(new Vector3i(2, 0, 3)),
                    new BlockOccupancy(new Vector3i(2, 0, 4)),
                    new BlockOccupancy(new Vector3i(2, 1, -3)),
                    new BlockOccupancy(new Vector3i(2, 1, -2)),
                    new BlockOccupancy(new Vector3i(2, 1, -1)),
                    new BlockOccupancy(new Vector3i(2, 1, 0)),
                    new BlockOccupancy(new Vector3i(2, 1, 1)),
                    new BlockOccupancy(new Vector3i(2, 1, 2)),
                    new BlockOccupancy(new Vector3i(2, 1, 3)),
                    new BlockOccupancy(new Vector3i(2, 1, 4)),
                    new BlockOccupancy(new Vector3i(2, 2, -3)),
                    new BlockOccupancy(new Vector3i(2, 2, -2)),
                    new BlockOccupancy(new Vector3i(2, 2, -1)),
                    new BlockOccupancy(new Vector3i(2, 2, 0)),
                    new BlockOccupancy(new Vector3i(2, 2, 1)),
                    new BlockOccupancy(new Vector3i(2, 2, 2)),
                    new BlockOccupancy(new Vector3i(2, 2, 3)),
                    new BlockOccupancy(new Vector3i(2, 2, 4)),
                    new BlockOccupancy(new Vector3i(2, 3, -3)),
                    new BlockOccupancy(new Vector3i(2, 3, -2)),
                    new BlockOccupancy(new Vector3i(2, 3, -1)),
                    new BlockOccupancy(new Vector3i(2, 3, 0)),
                    new BlockOccupancy(new Vector3i(2, 3, 1)),
                    new BlockOccupancy(new Vector3i(2, 3, 2)),
                    new BlockOccupancy(new Vector3i(2, 3, 3)),
                    new BlockOccupancy(new Vector3i(2, 3, 4)),
                }
            );
        }

        protected override void Initialize()
        {
            this.GetComponent<FuelSupplyComponent>().Initialize(4, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(100);
            this.GetComponent<LinkComponent>().Initialize(15);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<BoatComponent>().Size = BoatComponent.BoatSize.Large;
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);
            this.GetComponent<VehicleComponent>().Initialize(10, 2, 7, null, true);
            this.GetComponent<VehicleComponent>().FailDriveMsg = Localizer.Do(
                $"You are too hungry to drive {this.DisplayName}!"
            );
            this.GetComponent<PublicStorageComponent>().Initialize(48, 14000000);
            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(WoodenHullPlanksItem), Quantity = 4 },
                        new() { TypeName = nameof(PortableSteamEngineItem), Quantity = 1 },
                        new() { TypeName = nameof(LubricantItem), Quantity = 2 },
                    }
                );
        }

        public LazyResult ShouldOverrideAuth(IAlias alias, IOwned property, GameAction action)
        {
            return action is QueryAction or OpenAction or TradeAction
                ? LazyResult.Succeeded
                : LazyResult.FailedNoMessage;
        }
    }
}
