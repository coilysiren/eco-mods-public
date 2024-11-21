
namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;

    public partial class Saxifrage
    {
        public partial class SaxifrageSpecies
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
                    
                        new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop = 0.2f },
                    
                        new CapacityConstraint() { CapacityLayerName = "ShrubSpace", ConsumedCapacityPerPop = 0.2f }
                    
                };
            }
        }
    }
}