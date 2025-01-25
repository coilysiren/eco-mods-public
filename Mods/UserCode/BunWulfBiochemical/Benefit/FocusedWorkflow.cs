namespace BunWulfBioChemical
{
    using System;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Focused Workflow: Biochemist")]
    [LocDescription("Doubles the speed of related tables when alone.")]
    public partial class BiochemistFocusedWorkflowTalentGroup : TalentGroup
    {
        public BiochemistFocusedWorkflowTalentGroup()
        {
            Talents = new Type[] { typeof(BiochemistFocusedSpeedTalent) };
            OwningSkill = typeof(BiochemistSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class BiochemistFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistFocusedWorkflowTalentGroup);

        public BiochemistFocusedSpeedTalent()
        {
            Value = 0.5f;
        }
    }
}
