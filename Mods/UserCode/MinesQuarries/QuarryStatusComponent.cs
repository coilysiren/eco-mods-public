namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    public class QuarryComponent : WorldObjectComponent
    {
        private readonly string blockType = "";
        private readonly int searchRadius;
        private readonly float percentage;
        public override bool Enabled => this.Status;
        private bool Status = false;

        public QuarryComponent(string blockType, int searchRadius, float percentage)
        {
            this.blockType = blockType;
            this.searchRadius = searchRadius;
            this.percentage = percentage;
        }

        public override void Initialize() => this.FindBlocks();

        private void FindBlocks()
        {
            Dictionary<string, float> blocks = Search.FindBlockCounts(
                this.Parent.Position,
                this.searchRadius
            );

            bool foundAnyBlocks = false;
            foreach (KeyValuePair<string, float> block in blocks)
            {
                LocString itemUILink = Search.GetItemUILink(block.Key);
                StatusElement statusElement = this
                    .Parent.GetComponent<StatusComponent>(null)
                    .CreateStatusElement();

                // If this is the block we are looking for, check if it is above the percentage threshold
                if (block.Key == this.blockType)
                {
                    bool foundEnoughBlocks = block.Value > this.percentage;
                    statusElement.SetStatusMessage(
                        foundEnoughBlocks,
                        Localizer.DoStr($"{itemUILink}: {block.Value * 100}% concentration")
                    );
                    this.Status = foundEnoughBlocks;
                    foundAnyBlocks = true;
                }

                // Otherwise, just display that we found the block
                statusElement.SetStatusMessage(
                    true,
                    Localizer.DoStr($"{itemUILink}: {block.Value * 100}% concentration")
                );
            }
            if (!foundAnyBlocks)
            {
                LocString itemUILink = Search.GetItemUILink(this.blockType);
                StatusElement statusElement = this
                    .Parent.GetComponent<StatusComponent>(null)
                    .CreateStatusElement();
                statusElement.SetStatusMessage(false, Localizer.DoStr($"{itemUILink}: not found"));
            }
        }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class SandstoneQuarryComponent : QuarryComponent
    {
        public SandstoneQuarryComponent()
            : base(
                blockType: "Eco.Mods.TechTree.SandstoneBlock",
                searchRadius: 5,
                percentage: 0.333f
            ) { }
    }
}
