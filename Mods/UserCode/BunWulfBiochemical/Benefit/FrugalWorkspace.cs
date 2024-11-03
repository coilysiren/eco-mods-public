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
