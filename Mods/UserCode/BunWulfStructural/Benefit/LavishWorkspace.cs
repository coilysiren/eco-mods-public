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
    [LocDisplayName("Lavish Workspace: Construction")]
    [LocDescription(
        "Increases the tier requirement of tables by 0.2, but reduces the resources needed by 5 percent.(Only applies to claimed workstations)"
    )]
    public partial class ConstructionLavishWorkspaceTalentGroup : TalentGroup
    {
        public ConstructionLavishWorkspaceTalentGroup()
        {
            Talents = new Type[]
            {
                typeof(ConstructionLavishResourcesTalent),
                typeof(ConstructionLavishReqTalent),
            };
            OwningSkill = typeof(ConstructionSkill);
            Level = 6;
        }
    }

    [Serialized]
    public partial class ConstructionLavishResourcesTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(ConstructionLavishWorkspaceTalentGroup);

        public ConstructionLavishResourcesTalent()
        {
            Value = 0.95f;
        }
    }

    [Serialized]
    public partial class ConstructionLavishReqTalent : LavishWorkspaceTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(ConstructionLavishWorkspaceTalentGroup);

        public ConstructionLavishReqTalent()
        {
            Value = 0.2f;
        }
    }
}
