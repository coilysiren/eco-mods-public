using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class Orchid : PlantEntity
    {
        public partial class OrchidSpecies : PlantSpecies
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