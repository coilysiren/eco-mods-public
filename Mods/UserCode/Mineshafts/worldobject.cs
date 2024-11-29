namespace Mineshafts
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

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(CrudeIronMineshaftComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [Tag("Usable")]
    public partial class CrudeIronMineshaftObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(CrudeIronMineshaftItem);
        public override LocString DisplayName => Localizer.DoStr("Crude Iron Mineshaft");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        static CrudeIronMineshaftObject()
        {
            AddOccupancy<CrudeIronMineshaftObject>(
                new List<BlockOccupancy>()
                {
                    new(new Vector3i(0, 0, 0)),
                    new(new Vector3i(1, 0, 0)),
                    new(new Vector3i(2, 0, 0)),
                    new(new Vector3i(0, 1, 0)),
                    new(new Vector3i(1, 1, 0)),
                    new(new Vector3i(2, 1, 0)),
                }
            );
        }

        protected override void Initialize()
        {
            PublicStorageComponent storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(16);
        }
    }

    [Serialized]
    public partial class CrudeIronMineshaftItem : WorldObjectItem<CrudeIronMineshaftObject>
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );
    }
}
