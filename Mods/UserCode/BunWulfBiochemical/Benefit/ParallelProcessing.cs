namespace BunWulfBioChemical
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Parallel Processing: Biochemist")]
    [LocDescription(
        "Increases the crafting speed of identical tables when they share a room by 20 percent."
    )]
    public partial class BiochemistParallelProcessingTalentGroup : TalentGroup
    {
        public BiochemistParallelProcessingTalentGroup()
        {
            Talents = new Type[] { typeof(BiochemistParallelSpeedTalent) };
            OwningSkill = typeof(BiochemistSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class BiochemistParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistParallelProcessingTalentGroup);

        public BiochemistParallelSpeedTalent()
        {
            Value = 0.8f;
        }
    }
}
