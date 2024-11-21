
namespace Eco.Mods.Organisms
{
    using Eco.Mods.Organisms;

    public partial class Flax
    {
        public partial class FlaxSpecies
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