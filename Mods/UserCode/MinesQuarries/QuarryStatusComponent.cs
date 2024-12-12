namespace MinesQuarries
{
    using System;
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
            Dictionary<string, float> blocks = Search.FindBlockConcentrations(
                this.Parent.Position,
                this.searchRadius
            );

            bool foundAnyBlocks = false;
            foreach (KeyValuePair<string, float> block in blocks)
            {
                LocString itemDisplayName = Search.GetDisplayName(block.Key);

                // If this is the block we are looking for, check if it is above the percentage threshold
                if (block.Key == this.blockType)
                {
                    bool foundEnoughBlocks = block.Value > this.percentage;
                    StatusElement statusElement = this
                        .Parent.GetComponent<StatusComponent>(null)
                        .CreateStatusElement();
                    statusElement.SetStatusMessage(
                        foundEnoughBlocks,
                        Localizer.DoStr(
                            $"{itemDisplayName}: {Math.Round(block.Value * 100)}% concentration (required: >{Math.Round(this.percentage * 100)}%)"
                        )
                    );
                    this.Status = foundEnoughBlocks;
                    foundAnyBlocks = true;
                }
                // Otherwise, just display that we found the block
                else
                {
                    if (block.Value > 0.01)
                    {
                        StatusElement statusElement = this
                            .Parent.GetComponent<StatusComponent>(null)
                            .CreateStatusElement();
                        statusElement.SetStatusMessage(
                            true,
                            Localizer.DoStr(
                                $"{itemDisplayName}: {Math.Round(block.Value * 100)}% concentration"
                            )
                        );
                    }
                }
            }
            if (!foundAnyBlocks)
            {
                LocString itemDisplayName = Search.GetDisplayName(this.blockType);
                StatusElement statusElement = this
                    .Parent.GetComponent<StatusComponent>(null)
                    .CreateStatusElement();
                statusElement.SetStatusMessage(
                    false,
                    Localizer.DoStr(
                        $"{itemDisplayName}: not found (required: >{Math.Round(this.percentage * 100)}%)"
                    )
                );
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
                searchRadius: 3,
                percentage: 0.50f
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class LimestoneQuarryComponent : QuarryComponent
    {
        public LimestoneQuarryComponent()
            : base(
                blockType: "Eco.Mods.TechTree.LimestoneBlock",
                searchRadius: 3,
                percentage: 0.50f
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class GraniteQuarryComponent : QuarryComponent
    {
        public GraniteQuarryComponent()
            : base(
                blockType: "Eco.Mods.TechTree.GraniteBlock", //
                searchRadius: 3,
                percentage: 0.50f
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class ShaleQuarryComponent : QuarryComponent
    {
        public ShaleQuarryComponent()
            : base(
                blockType: "Eco.Mods.TechTree.ShaleBlock", //
                searchRadius: 3,
                percentage: 0.50f
            ) { }
    }
}
