namespace Mineshafts
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;

    // using Eco.Shared.Serialization;

    // [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class WorldCounterComponent : WorldObjectComponent
    {
        private StatusElement? foundIronStatus;
        private StatusElement? foundSandstoneStatus;

        public override void Initialize()
        {
            this.foundIronStatus = this
                .Parent.GetComponent<StatusComponent>(null)
                .CreateStatusElement();
            this.foundSandstoneStatus = this
                .Parent.GetComponent<StatusComponent>(null)
                .CreateStatusElement();
            this.FindBlocks();
        }

        private void FindBlocks()
        {
            if (this.foundIronStatus != null)
            {
                bool foundIron = Mineshaft.FindBlock(
                    this.Parent.Position,
                    "Eco.Mods.TechTree.IronOreBlock",
                    2
                );
                this.foundIronStatus.SetStatusMessage(
                    foundIron,
                    Localizer.DoStr(foundIron ? "Iron Ore found" : "Iron Ore not found")
                );
            }
            if (this.foundSandstoneStatus != null)
            {
                bool foundSandstone = Mineshaft.FindBlock(
                    this.Parent.Position,
                    "Eco.Mods.TechTree.SandstoneBlock",
                    2
                );
                this.foundSandstoneStatus.SetStatusMessage(
                    foundSandstone,
                    Localizer.DoStr(foundSandstone ? "Sandstone found" : "Sandstone not found")
                );
            }
        }
    }
}
