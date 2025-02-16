namespace BunWulfHardwareCo
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions.Interactors;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.SharedTypes;
    using Eco.World.Blocks;

    [Serialized]
    [LocDisplayName("Sledgehammer")]
    [LocDescription("An iron tool whose only purpose is to smash things.")]
    [Tier(2)]
    [RepairRequiresSkill(typeof(BlacksmithSkill), 0)]
    [Weight(1000)]
    [Category("Tool")]
    [Tag("Tool")]
    [Ecopedia("Items", "Tools", createAsSubPage: true)]
    public partial class Sledgehammer : BuildingToolItem
    {
        private static IDynamicValue caloriesBurn = new MultiDynamicValue(
            MultiDynamicOps.Multiply,
            new TalentModifiedValue(typeof(Sledgehammer), typeof(ToolEfficiencyTalent)),
            CreateCalorieValue(8, typeof(SelfImprovementSkill), typeof(Sledgehammer))
        );
        private static IDynamicValue tier = new ConstantValue(2);
        private static SkillModifiedValue skilledRepairCost =
            new(
                2,
                BlacksmithSkill.MultiplicativeStrategy,
                typeof(BlacksmithSkill),
                typeof(Sledgehammer),
                Localizer.DoStr("repair cost"),
                DynamicValueType.Efficiency
            );

        public override IDynamicValue CaloriesBurn => caloriesBurn;
        public override IDynamicValue Tier => tier;
        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public override float OriginalMaxDurability => 1500f;
        public override int FullRepairAmount => 2;
        public override IEnumerable<RepairingItem> RepairItems
        {
            get
            {
                yield return new() { Item = Get<CoarseStoneItem>(), MaterialMult = 2 };
                yield return new() { Item = Get<SharpeningSteelItem>(), MaterialMult = 1 };
                yield return new() { Item = Get<WhetstoneItem>(), MaterialMult = 2 };
                yield return new() { Item = Get<PolishingPasteItem>(), MaterialMult = 0.5f };
            }
        }

        public override bool IsValidForInteraction(Item item)
        {
            return item is not LogItem
                && item is BlockItem blockItem
                && Block.Is<Constructed>(blockItem.OriginType);
        }

        [Interaction(
            InteractionTrigger.LeftClick,
            tags: "Constructable",
            canHoldToTrigger: TriBool.True,
            animationDriven: false
        )]
        public bool Deconstruct(
            Player player,
            InteractionTriggerInfo triggerInfo,
            InteractionTarget target
        )
        {
            return target.BlockPosition.HasValue
                && this.PickupBlock(player, target.BlockPosition.Value);
        }
    }
}
