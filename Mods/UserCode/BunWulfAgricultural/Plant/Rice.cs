
namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;

    public partial class Rice
    {
        public partial class RiceSpecies
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