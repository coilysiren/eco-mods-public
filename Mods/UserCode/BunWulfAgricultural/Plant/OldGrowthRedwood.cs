namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;
    using Eco.Shared.Math;

    public partial class OldGrowthRedwood
    {
        public partial class OldGrowthRedwoodSpecies
        {
            partial void ModsPostInitialize()
            {
                this.ReleasesCO2TonsPerDay = this.ReleasesCO2TonsPerDay * 10;
            }
        }
    }
}
