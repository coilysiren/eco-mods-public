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
    [LocDisplayName("Lavish Workspace: Biochemist")]
    [LocDescription(
        "Increases the tier requirement of tables by 0.2, but reduces the resources needed by 5 percent.(Only applies to claimed workstations)"
    )]
    public partial class BiochemistLavishWorkspaceTalentGroup : TalentGroup
    {
        public BiochemistLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(BiochemistLavishResourcesTalent),
                typeof(BiochemistLavishReqTalent),
            };
            OwningSkill = typeof(BiochemistSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class BiochemistLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistLavishWorkspaceTalentGroup);

        public BiochemistLavishResourcesTalent()
        {
            Value = 0.95f;
        }
    }

    [Serialized]
    public partial class BiochemistLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistLavishWorkspaceTalentGroup);

        public BiochemistLavishReqTalent()
        {
            Value = 0.2f;
        }
    }
}
