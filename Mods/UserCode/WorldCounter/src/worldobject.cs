namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class WorldCounterComponent : WorldObjectComponent
    {
        private StatusElement? status;
        private DateTime LastRun = DateTime.MinValue;

        public override void Initialize()
        {
            this.status = this.Parent.GetComponent<StatusComponent>(null).CreateStatusElement();
            this.UpdateStatus();
        }

        public override void Tick()
        {
            base.Tick();
            if (DateTime.Now - this.LastRun > TimeSpan.FromMinutes(5))
            {
                this.LastRun = DateTime.Now;
                this.UpdateStatus();
            }
        }

        private void UpdateStatus()
        {
            if (this.status != null)
            {
                Vector3 position = this.Parent.Position;
                SortedDictionary<string, int> counts = Counter.GetCounts(position);
                string message = "Nearby Blocks:\n";
                foreach (KeyValuePair<string, int> kvp in counts)
                {
                    message += $"\t{kvp.Key}: {kvp.Value}\n";
                }
                this.status.SetStatusMessage(true, Localizer.DoStr(message));
            }
        }
    }

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
