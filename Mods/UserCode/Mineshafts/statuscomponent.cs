namespace Mineshafts
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    public class MineshaftComponent : WorldObjectComponent
    {
        private readonly Dictionary<string, StatusElement> blockStatusMap = new();
        private readonly Dictionary<string, string> blockTypeMap = new();
        private readonly Dictionary<string, bool> blockFoundMap = new();
        private readonly int radius;
        public override bool Enabled => this.blockFoundMap.Values.All(found => found);

        public MineshaftComponent(Dictionary<string, string> blockTypeMap, int radius)
        {
            this.blockTypeMap = blockTypeMap;
            this.radius = radius;
        }

        public override void Initialize()
        {
            foreach (KeyValuePair<string, string> block in this.blockTypeMap)
            {
                this.blockStatusMap[block.Key] = this
                    .Parent.GetComponent<StatusComponent>(null)
                    .CreateStatusElement();
            }
            this.FindBlocks();
        }

        private void FindBlocks()
        {
            foreach (KeyValuePair<string, StatusElement> blockStatus in this.blockStatusMap)
            {
                StatusElement statusElement = blockStatus.Value;
                if (statusElement != null)
                {
                    bool found = Mineshaft.FindBlock(
                        this.Parent.Position,
                        blockStatus.Key,
                        this.radius
                    );
                    string displayName = this.blockTypeMap[blockStatus.Key];
                    blockStatus.Value.SetStatusMessage(
                        found,
                        Localizer.DoStr(found ? $"{displayName} found" : $"{displayName} not found")
                    );
                    this.blockFoundMap[blockStatus.Key] = found;
                }
            }
        }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class CrudeIronMineshaftComponent : MineshaftComponent
    {
        public CrudeIronMineshaftComponent()
            : base(
                blockTypeMap: new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.IronOreBlock", Item.Get<IronOreItem>().UILink() },
                    { "Eco.Mods.TechTree.SandstoneBlock", Item.Get<SandstoneItem>().UILink() },
                },
                radius: 3
            ) { }
    }
}
