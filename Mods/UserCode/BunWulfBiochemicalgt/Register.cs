using Eco.Core.Plugins.Interfaces;

#pragma warning disable IDE0022

public class BunWulfBioChemical : IModInit
{
    public static ModRegistration Register() =>
        new()
        {
            ModName = "BunWulfBioChemical",
            ModDescription =
                "A mod that adds a Biochemist profession who's role is to go toe to toe with Oil Drilling. The Biochemist gets recipes to make Biofuel, Plastic, Rubber, Epoxy, and Nylon. The recipes have similar costs to Oil Drilling, but the Biochemist is more sustainable and has a lower impact on the environment.",
            ModDisplayName = "BunWulf BioChemical",
        };
}
