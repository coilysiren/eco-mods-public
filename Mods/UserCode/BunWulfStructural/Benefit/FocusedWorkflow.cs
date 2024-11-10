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
    [LocDisplayName("Focused Workflow: Construstion")]
    [LocDescription("Doubles the speed of related tables when alone.")]
    public partial class ConstructionFocusedWorkflowTalentGroup : TalentGroup
    {
        public ConstructionFocusedWorkflowTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionFocusedSpeedTalent) };
            OwningSkill = typeof(ConstructionSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class ConstructionFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(ConstructionFocusedWorkflowTalentGroup);

        public ConstructionFocusedSpeedTalent()
        {
            Value = 0.5f;
        }
    }
}
