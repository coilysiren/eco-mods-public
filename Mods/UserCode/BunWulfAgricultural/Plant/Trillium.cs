using Eco.Mods.Organisms;

namespace Eco.Mods.Organisms
{
    public partial class Trillium : PlantEntity
    {
        public partial class TrilliumSpecies : PlantSpecies
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