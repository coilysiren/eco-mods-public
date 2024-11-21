using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class Seagrass : PlantEntity
    {
        public partial class SeagrassSpecies : PlantSpecies
        {
            partial void ModsPostInitialize()
            {
                this.SeedingArea = this.SeedingArea * 10;
                this.SeedsCount = this.SeedsCount * 10;
                this.CapacityConstraints = new List<CapacityConstraint>()
                {
                    
                };
            }
        }
    }
}