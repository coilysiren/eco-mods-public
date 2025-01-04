namespace MinesQuarries
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    public class PitComponent : WorldObjectComponent
    {
        private readonly string blockType = "";
        private readonly int searchRadius;
        private readonly int minimumBlocks;
        public override bool Enabled => this.Status;
        private bool Status = false;

        public PitComponent(string blockType, int searchRadius, int minimumBlocks)
        {
            this.blockType = blockType;
            this.searchRadius = searchRadius;
            this.minimumBlocks = minimumBlocks;
        }

        public override void Initialize() => this.FindBlocks();

        private void FindBlocks()
        {
            int blockCount = Search.FindBlockCount(
                this.Parent.Position,
                this.searchRadius,
                this.blockType
            );

            StatusElement blockCountStatusElement = this
                .Parent.GetComponent<StatusComponent>(null)
                .CreateStatusElement();

            if (blockCount >= this.minimumBlocks)
            {
                blockCountStatusElement.SetStatusMessage(
                    true,
                    Localizer.DoStr(
                        $"{Search.GetDisplayName(this.blockType)}: {blockCount} found (required: >{this.minimumBlocks})"
                    )
                );
                this.Status = true;
            }
            else
            {
                blockCountStatusElement.SetStatusMessage(
                    false,
                    Localizer.DoStr(
                        $"{Search.GetDisplayName(this.blockType)}: {blockCount} found (required: >{this.minimumBlocks})"
                    )
                );
            }
        }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class DirtPitComponent : PitComponent
    {
        public DirtPitComponent()
            : base(
                blockType: "Eco.World.Blocks.DirtBlock", //
                searchRadius: 5,
                minimumBlocks: 20
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class SandPitComponent : PitComponent
    {
        public SandPitComponent()
            : base(
                blockType: "Eco.Mods.TechTree.SandBlock", //
                searchRadius: 5,
                minimumBlocks: 30
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class ClayPitComponent : PitComponent
    {
        public ClayPitComponent()
            : base(
                blockType: "Eco.Mods.TechTree.ClayBlock", //
                searchRadius: 5,
                minimumBlocks: 40
            ) { }
    }
}
