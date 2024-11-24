
namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Mods.Organisms;
    using Eco.Shared.Math;

    public partial class Waterweed
    {
        public partial class WaterweedSpecies
        {
            partial void ModsPostInitialize()
            {
                this.MaxGrowthRate = this.MaxGrowthRate * 10;
                this.SpreadRate = this.SpreadRate * 10;
                this.SeedingArea = this.SeedingArea * 10;
                this.SeedsCount = this.SeedsCount * 10;
                this.GenerationDefinitions.StartBiomes = "";
                this.GenerationDefinitions.CountOfClusters = new Range(
                    this.GenerationDefinitions.CountOfClusters.min * 5,
                    this.GenerationDefinitions.CountOfClusters.max * 5
                );
            }
        }
    }
}