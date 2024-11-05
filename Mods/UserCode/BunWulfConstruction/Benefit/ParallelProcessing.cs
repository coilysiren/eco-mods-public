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
    [LocDisplayName("Parallel Processing: Construction Worker")]
    [LocDescription(
        "Increases the crafting speed of identical tables when they share a room by 20 percent."
    )]
    public partial class ConstructionWorkerParallelProcessingTalentGroup : TalentGroup
    {
        public ConstructionWorkerParallelProcessingTalentGroup()
        {
            Talents = new Type[] { typeof(ConstructionWorkerParallelSpeedTalent) };
            OwningSkill = typeof(ConstructionWorkerSkill);
            Level = 3;
        }
    }

    [Serialized]
    public partial class ConstructionWorkerParallelSpeedTalent : ParallelProcessingTalent
    {
        public override bool Base => false;
        public override Type TalentGroupType =>
            typeof(ConstructionWorkerParallelProcessingTalentGroup);

        public ConstructionWorkerParallelSpeedTalent()
        {
            Value = 0.8f;
        }
    }
}
