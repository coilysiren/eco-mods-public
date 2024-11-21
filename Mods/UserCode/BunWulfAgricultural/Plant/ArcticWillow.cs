using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class ArcticWillow : PlantEntity
    {
        public partial class ArcticWillowSpecies : PlantSpecies
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