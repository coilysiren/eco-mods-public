using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Plants;
using Eco.Mods.Organisms;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.SharedTypes;
using Eco.Simulation;
using Eco.Simulation.Types;
using Eco.World.Blocks;

namespace Eco.Mods.Organisms
{
    public partial class Pineapple
    {
        public partial class PineappleSpecies
        {
            partial void ModsPostInitialize()
            {
                this.MaturityAgeDays = this.MaturityAgeDays / 2;
                this.SeedingArea = this.SeedingArea * 5;
            }
        }
    }
}