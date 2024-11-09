namespace Eco.Mods.TechTree
{
    public partial class Wheat : PlantEntity
    {
        public partial class WheatSpecies : PlantSpecies
        {
            partial void ModsPostInitialize()
            {
                this.MaturityAgeDays = MaturityAgeDays / 2;
            }
        }
    }
}
