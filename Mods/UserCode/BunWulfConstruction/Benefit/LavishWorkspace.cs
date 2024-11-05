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
    [LocDisplayName("Lavish Workspace: Construction Worker")]
    [LocDescription(
        "Increases the tier requirement of tables by 0.2, but reduces the resources needed by 5 percent.(Only applies to claimed workstations)"
    )]
    public partial class ConstructionWorkerLavishWorkspaceTalentGroup : TalentGroup
    {
        public ConstructionWorkerLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(ConstructionWorkerLavishResourcesTalent),
                typeof(ConstructionWorkerLavishReqTalent),
            };
            OwningSkill = typeof(ConstructionWorkerSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class ConstructionWorkerLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType =>
            typeof(ConstructionWorkerLavishWorkspaceTalentGroup);

        public ConstructionWorkerLavishResourcesTalent()
        {
            Value = 0.95f;
        }
    }

    [Serialized]
    public partial class ConstructionWorkerLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType =>
            typeof(ConstructionWorkerLavishWorkspaceTalentGroup);

        public ConstructionWorkerLavishReqTalent()
        {
            Value = 0.2f;
        }
    }
}
