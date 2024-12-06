namespace Mines
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
    using Mineshafts;

    public class MineComponent : WorldObjectComponent
    {
        private readonly Dictionary<string, StatusElement> blockStatusMap = new();
        private readonly Dictionary<string, string> blockTypeMap = new();
        private readonly Dictionary<string, bool> validChecks = new();
        private readonly int searchRadius;
        private readonly int minProximity;
        public override bool Enabled => this.validChecks.Values.All(found => found);

        public MineComponent(
            Dictionary<string, string> blockTypeMap,
            int searchRadius,
            int minProximity
        )
        {
            this.blockTypeMap = blockTypeMap;
            this.searchRadius = searchRadius;
            this.minProximity = minProximity;
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
                    bool found = Mine.FindBlock(
                        this.Parent.Position,
                        blockStatus.Key,
                        this.searchRadius
                    );
                    string displayName = this.blockTypeMap[blockStatus.Key];
                    blockStatus.Value.SetStatusMessage(
                        found,
                        Localizer.DoStr(found ? $"{displayName} found" : $"{displayName} not found")
                    );
                    this.validChecks[blockStatus.Key] = found;
                }
            }
        }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class CrudeIronMineComponent : MineComponent
    {
        public CrudeIronMineComponent()
            : base(
                blockTypeMap: new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.IronOreBlock", Item.Get<IronOreItem>().UILink() },
                    { "Eco.Mods.TechTree.SandstoneBlock", Item.Get<SandstoneItem>().UILink() },
                },
                searchRadius: 1, // must be right on top of the ore
                minProximity: 10
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class IronMineComponent : MineComponent
    {
        public IronMineComponent()
            : base(
                blockTypeMap: new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.IronOreBlock", Item.Get<IronOreItem>().UILink() },
                    { "Eco.Mods.TechTree.SandstoneBlock", Item.Get<SandstoneItem>().UILink() },
                },
                searchRadius: 3,
                minProximity: 15 // really only larger than the crude variant because the object is larger
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class CopperMineComponent : MineComponent
    {
        public CopperMineComponent()
            : base(
                blockTypeMap: new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.CopperOreBlock", Item.Get<CopperOreItem>().UILink() },
                    { "Eco.Mods.TechTree.GraniteBlock", Item.Get<GraniteItem>().UILink() },
                },
                searchRadius: 3,
                minProximity: 15
            ) { }
    }

    [Serialized]
    [RequireComponent(typeof(StatusComponent), null)]
    public class GoldMineComponent : MineComponent
    {
        public GoldMineComponent()
            : base(
                blockTypeMap: new Dictionary<string, string>
                {
                    { "Eco.Mods.TechTree.GoldOreBlock", Item.Get<GoldOreItem>().UILink() },
                    { "Eco.Mods.TechTree.GraniteBlock", Item.Get<GraniteItem>().UILink() },
                },
                searchRadius: 3,
                minProximity: 15
            ) { }
    }
}
