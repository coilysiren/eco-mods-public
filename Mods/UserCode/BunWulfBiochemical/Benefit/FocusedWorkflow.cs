#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
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
