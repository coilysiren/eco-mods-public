using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class BarrelCactus : PlantEntity
    {
        public partial class BarrelCactusSpecies : PlantSpecies
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