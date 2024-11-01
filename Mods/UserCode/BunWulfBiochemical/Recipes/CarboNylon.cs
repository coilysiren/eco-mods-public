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
    public partial class CarboNylonRecipe : RecipeFamily
    {
        public CarboNylonRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "CarboNylon",
                displayName: Localizer.DoStr("Coal Based Nylon"),
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
                    new CraftingElement<NylonItem>(8),
                    // sold @ 50% profit = 3.75 / 8 = 0.47 per rubber (two significant digits)
                }
            );
            Recipes = new List<Recipe> { recipe };
            // NylonRecipe.ExperienceOnCraft
            ExperienceOnCraft = 1;
            // NylonRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(CuttingEdgeCookingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarboNylonRecipe),
                // NylonRecipe.CraftMinutes * 2
                start: 3,
                skillType: typeof(CuttingEdgeCookingSkill),
                typeof(CuttingEdgeCookingFocusedSpeedTalent),
                typeof(CuttingEdgeCookingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Coal Based Nylon"),
                recipeType: typeof(CarboNylonRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
