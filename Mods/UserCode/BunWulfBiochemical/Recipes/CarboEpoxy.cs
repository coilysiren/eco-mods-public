namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class CarboEpoxyRecipe : RecipeFamily
    {
        public CarboEpoxyRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "CarboEpoxy",
                displayName: Localizer.DoStr("Coal Fat Epoxy"),
                ingredients: new List<IngredientElement>
                {
                    // priced at 1 x 1 = 1
                    new(
                        "Coal",
                        1,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // priced at 0.2 x 10 = 2
                    new(
                        "Fat",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // priced at 1
                    new(typeof(EthanolItem), 1, true),
                    // ingredient cost = 4
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<EpoxyItem>(6),
                    // sold @ 50% profit = 6 / 6 = 1    per epoxy
                    // sold @ 0%  profit = 4 / 6 = 0.66 per epoxy
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            // EpoxyRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarboEpoxyRecipe),
                start: 20,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Coal Fat Epoxy"),
                recipeType: typeof(CarboEpoxyRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
