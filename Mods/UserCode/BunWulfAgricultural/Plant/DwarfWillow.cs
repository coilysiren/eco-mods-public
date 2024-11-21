
namespace Eco.Mods.Organisms
{
    using Eco.Mods.Organisms;

    public partial class DwarfWillow
    {
        public partial class DwarfWillowSpecies
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