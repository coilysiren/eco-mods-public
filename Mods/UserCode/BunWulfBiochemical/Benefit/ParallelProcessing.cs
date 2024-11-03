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
    [LocDisplayName("Parallel Processing: Biochemist")]
    [LocDescription(
        "Increases the crafting speed of identical tables when they share a room by 20 percent."
    )]
    public partial class BiochemistParallelProcessingTalentGroup : TalentGroup
    {
        public BiochemistParallelProcessingTalentGroup()
        {
            Talents = new Type[] { typeof(BiochemistParallelSpeedTalent) };
            OwningSkill = typeof(BiochemistSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class BiochemistParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(BiochemistParallelProcessingTalentGroup);

        public BiochemistParallelSpeedTalent()
        {
            Value = 0.8f;
        }
    }
}
