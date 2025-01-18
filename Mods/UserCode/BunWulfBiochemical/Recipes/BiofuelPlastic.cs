namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 3)]
    public partial class PlasticBiofuel : RecipeFamily
    {
        public PlasticBiofuel()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "PlasticBiofuel",
                displayName: Localizer.DoStr("Plastic Container Biofuel, 50% Ethanol"),
                ingredients: new List<IngredientElement>
                {
                    // priced at 1 x 2 = 2
                    new(typeof(EthanolItem), 2, staticIngredient: true),
                    // priced at 0.5 x 2 = 1
                    new(
                        typeof(PlasticItem),
                        2,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // ingredient cost = 3
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BiodieselItem>(2),
                    // sold @ 50% profit   = 4.5 / 2 = 2.25 per biodiesel
                    // sold @ 0%  profit   = 3 / 2   = 1.5 per biodiesel
                    // you can make both the plastic and ethanol from scratch... so pure profit!
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            // BiodieselRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(PlasticBiofuel),
                start: 4,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Plastic Container Biofuel, 50% Ethanol"),
                recipeType: typeof(PlasticBiofuel)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
