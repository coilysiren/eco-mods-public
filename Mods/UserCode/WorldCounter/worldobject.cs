namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(WorldCounterComponent))]
    [Tag("Usable")]
    public partial class BookkeepingDeskObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(BookkeepingDeskItem);
        public override LocString DisplayName => Localizer.DoStr("Bookkeeping Desk");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        static BookkeepingDeskObject()
        {
            AddOccupancy<BookkeepingDeskObject>(
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
    }

    [Serialized]
    public partial class BookkeepingDeskItem
        : WorldObjectItem<BookkeepingDeskObject>,
            IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object? PersistentData { get; set; }
    }
}
