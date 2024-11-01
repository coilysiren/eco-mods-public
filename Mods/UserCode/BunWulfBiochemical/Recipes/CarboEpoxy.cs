#pragma warning disable IDE0005
using System.Collections.Generic;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]
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
                        typeof(CuttingEdgeCookingSkill),
                        typeof(CuttingEdgeCookingLavishResourcesTalent)
                    ),
                    // priced at 0.15 x 10 = 1.5
                    new(
                        "Fat",
                        10,
                        typeof(CuttingEdgeCookingSkill),
                        typeof(CuttingEdgeCookingLavishResourcesTalent)
                    ),
                    // ingredient cost = 2.5
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<EpoxyItem>(2),
                    // sold @ 50% profit = 3.75 / 2 = 1.9 per epoxy (two significant digits)
                }
            );
            Recipes = new List<Recipe> { recipe };
            // EpoxyRecipe.ExperienceOnCraft
            ExperienceOnCraft = 1;
            // EpoxyRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(CuttingEdgeCookingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarboEpoxyRecipe),
                // EpoxyRecipe.CraftMinutes * 2
                start: 3,
                skillType: typeof(CuttingEdgeCookingSkill),
                typeof(CuttingEdgeCookingFocusedSpeedTalent),
                typeof(CuttingEdgeCookingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Coal Fat Epoxy"),
                recipeType: typeof(CarboEpoxyRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
