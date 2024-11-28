namespace Mineshafts
{
    using System;
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
    using Eco.World.Blocks;

    public class MineshaftComponent : WorldObjectComponent
    {
        private readonly Dictionary<string, StatusElement> blockStatusMap = new();
        private readonly Dictionary<string, string> blockTypeMap = new();

        public MineshaftComponent(Dictionary<string, string> blockTypeMap)
        {
            this.blockTypeMap = blockTypeMap;
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
                    bool found = Mineshaft.FindBlock(this.Parent.Position, blockStatus.Key, 2);
                    string itemUILink = this.blockTypeMap[blockStatus.Key];
                    blockStatus.Value.SetStatusMessage(
                        found,
                        Localizer.DoStr(found ? $"{itemUILink} found" : $"{itemUILink} not found")
                    );
                }
            }
        }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class IronMineshaftComponent : MineshaftComponent
    {
        public IronMineshaftComponent()
            : base(
                new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.IronOreBlock", Item.Get<IronOreItem>().UILink() },
                    { "Eco.Mods.TechTree.SandstoneBlock", Item.Get<SandstoneItem>().UILink() },
                }
            ) { }
    }
}
