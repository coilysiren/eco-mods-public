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
    [LocDisplayName("Frugal Workspace: Construstion")]
    [LocDescription(
        "Lowers the tier requirement of related tables by 0.2. (Only applies to claimed workstations)"
    )]
    public partial class ConstructionFrugalWorkspaceTalentGroup : TalentGroup
    {
        public ConstructionFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionFrugalReqTalent) };
            OwningSkill = typeof(ConstructionSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class ConstructionFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(ConstructionFrugalWorkspaceTalentGroup);

        public ConstructionFrugalReqTalent()
        {
            Value = -0.2f;
        }
    }
}
