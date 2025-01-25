namespace BunWulfBioChemical
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Frugal Workspace: Biochemist")]
    [LocDescription(
        "Lowers the tier requirement of related tables by 0.2.(Only applies to claimed workstations)"
    )]
    public partial class BiochemistFrugalWorkspaceTalentGroup : TalentGroup
    {
        public BiochemistFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[] { typeof(BiochemistFrugalReqTalent) };
            OwningSkill = typeof(BiochemistSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class BiochemistFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistFrugalWorkspaceTalentGroup);

        public BiochemistFrugalReqTalent()
        {
            Value = -0.2f;
        }
    }
}
