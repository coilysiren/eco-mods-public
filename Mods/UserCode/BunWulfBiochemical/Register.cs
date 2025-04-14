namespace BunWulfBioChemical
{
    using Eco.Core.Plugins.Interfaces;

    public class BunWulfBioChemical : IModInit
    {
        public static ModRegistration Register() =>
            new()
            {
                ModName = "BunWulfBioChemical",
                ModDescription =
                    "A mod that adds a Biochemist profession who's role is to go toe to toe with Oil Drilling. The Biochemist gets recipes to make Biofuel, Plastic, Rubber, Epoxy, and Nylon. The recipes have similar costs to Oil Drilling, but the Biochemist is more sustainable and has a lower impact on the environment. While a biochemist's chemlab performs many of the same functions as an oil refinery, it does them very slowly. Plan to run at least 8 of them.",
                ModDisplayName = "BunWulf BioChemical",
            };
    }
}
