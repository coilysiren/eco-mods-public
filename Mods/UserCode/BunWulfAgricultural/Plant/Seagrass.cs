
namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;

    public partial class Seagrass
    {
        public partial class SeagrassSpecies
        {
            partial void ModsPostInitialize()
            {
                this.MaxGrowthRate = this.MaxGrowthRate * 10;
                this.SpreadRate = this.SpreadRate * 10;
                this.SeedingArea = this.SeedingArea * 10;
                this.SeedsCount = this.SeedsCount * 10;
                this.GenerationDefinitions.StartBiomes = "";
                this.CapacityConstraints = new List<CapacityConstraint>()
                {
                    
                        new CapacityConstraint() { CapacityLayerName = "UnderwaterFertileGround", ConsumedCapacityPerPop = 0.4f }
                    
                };
            }
        }
    }
}