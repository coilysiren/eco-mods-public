using Eco.Core.Plugins.Interfaces;

#pragma warning disable IDE0022

public class BunWulfAgricultural : IModInit
{
    public static void Main()
    {
        // give dotnet an arbitrary entrypoint so it builds the rest of the project
    }

    public static ModRegistration Register() =>
        new()
        {
            ModName = "BunWulfAgricultural",
            ModDescription =
                "A mod that a suite of recipes for agriculture. The goal of these recipes is to increase the flexibility of the Farming Speciality and allow it to impact the economy in novel ways.",
            ModDisplayName = "BunWulf Agricultural",
        };
}
