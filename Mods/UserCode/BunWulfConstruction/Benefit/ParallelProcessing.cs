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
    [LocDisplayName("Parallel Processing: Construction")]
    [LocDescription(
        "Increases the crafting speed of identical tables when they share a room by 20 percent."
    )]
    public partial class ConstructionParallelProcessingTalentGroup : TalentGroup
    {
        public ConstructionParallelProcessingTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionParallelSpeedTalent) };
            OwningSkill = typeof(ConstructionSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class ConstructionParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType => typeof(ConstructionParallelProcessingTalentGroup);

        public ConstructionParallelSpeedTalent()
        {
            Value = 0.8f;
        }
    }
}
