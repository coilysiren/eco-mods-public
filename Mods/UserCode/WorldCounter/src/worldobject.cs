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
        private StatusElement? progressElement;
        private StatusElement? countsElement;
        private DateTime LastRun = DateTime.MinValue;
        private readonly Dictionary<WrappedWorldPosition3i, (string, int)> countPositions = new();

        public override void Initialize()
        {
            this.progressElement = this
                .Parent.GetComponent<StatusComponent>(null)
                .CreateStatusElement();
            this.countsElement = this
                .Parent.GetComponent<StatusComponent>(null)
                .CreateStatusElement();
        }

        public override void Tick()
        {
            base.Tick();
            // Swap "Second" for "Minute" to before release
            if (DateTime.Now.Second != this.LastRun.Second)
            {
                this.LastRun = DateTime.Now;
                this.UpdateCounts(this.LastRun.Second);
            }
        }

        private void UpdateCounts(int time)
        {
            if (this.countsElement != null && this.progressElement != null)
            {
                // Generate progress value
                int progress = RoundNumber(100 * time / 60);

                // Generate progress message, assign to element
                string progressMessage = Localizer.DoStr($"Bookkeeping progress: {progress}%");
                this.progressElement.SetStatusMessage(true, Localizer.DoStr(progressMessage));

                // Generate retrieve count positions
                Dictionary<WrappedWorldPosition3i, (string, int)> newCounts = Counter.GetCounts(
                    this.Parent.Position,
                    time
                );
                foreach (KeyValuePair<WrappedWorldPosition3i, (string, int)> kvp in newCounts)
                {
                    this.countPositions[kvp.Key] = kvp.Value;
                }

                // Aggregate counts from the various positions
                SortedDictionary<string, int> countsValues = new();
                foreach (
                    KeyValuePair<WrappedWorldPosition3i, (string, int)> kvp in this.countPositions
                )
                {
                    if (countsValues.ContainsKey(kvp.Value.Item1))
                    {
                        countsValues[kvp.Value.Item1] += kvp.Value.Item2;
                    }
                    else
                    {
                        countsValues[kvp.Value.Item1] = kvp.Value.Item2;
                    }
                }

                // Generate counts message, assign to element
                string countsMessage = "Nearby blocks:\n";
                foreach (KeyValuePair<string, int> kvp in countsValues)
                {
                    countsMessage += $"\t{kvp.Key}: {RoundNumber(kvp.Value)}\n";
                }
                this.countsElement.SetStatusMessage(true, Localizer.DoStr(countsMessage));
            }
        }

        private static int RoundNumber(int number)
        {
            if (number < 100)
            {
                return number;
            }
            int magnitude = (int)Math.Pow(10, (int)Math.Log10(number) - 1);
            return number / magnitude * magnitude;
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
