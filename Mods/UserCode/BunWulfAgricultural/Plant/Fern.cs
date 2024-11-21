using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class Fern : PlantEntity
    {
        public partial class FernSpecies : PlantSpecies
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