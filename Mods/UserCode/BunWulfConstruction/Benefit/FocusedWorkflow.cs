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
    [LocDisplayName("Focused Workflow: Construstion Worker")]
    [LocDescription("Doubles the speed of related tables when alone.")]
    public partial class ConstructionWorkerFocusedWorkflowTalentGroup : TalentGroup
    {
        public ConstructionWorkerFocusedWorkflowTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionWorkerFocusedSpeedTalent) };
            OwningSkill = typeof(ConstructionWorkerSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class ConstructionWorkerFocusedSpeedTalent : FocusedWorkflowTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType =>
            typeof(ConstructionWorkerFocusedWorkflowTalentGroup);

        public ConstructionWorkerFocusedSpeedTalent()
        {
            Value = 0.5f;
        }
    }
}
