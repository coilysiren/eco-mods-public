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
    [LocDisplayName("Frugal Workspace: Construstion Worker")]
    [LocDescription(
        "Lowers the tier requirement of related tables by 0.2. (Only applies to claimed workstations)"
    )]
    public partial class ConstructionWorkerFrugalWorkspaceTalentGroup : TalentGroup
    {
        public ConstructionWorkerFrugalWorkspaceTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionWorkerFrugalReqTalent) };
            OwningSkill = typeof(ConstructionWorkerSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class ConstructionWorkerFrugalReqTalent : FrugalWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType =>
            typeof(ConstructionWorkerFrugalWorkspaceTalentGroup);

        public ConstructionWorkerFrugalReqTalent()
        {
            Value = -0.2f;
        }
    }
}
