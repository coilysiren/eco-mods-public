
namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;

    public partial class Wheat
    {
        public partial class WheatSpecies
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