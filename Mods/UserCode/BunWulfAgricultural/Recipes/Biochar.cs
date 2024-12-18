#pragma warning disable IDE0005
using System.Collections.Generic;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(FarmingSkill), 6)]
    public partial class BiocharRecipe : RecipeFamily
    {
        public BiocharRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Biochar",
                displayName: Localizer.DoStr("Biochar Charcoal Burning"),
                ingredients: new List<IngredientElement>
                {
                    // 1 x 4 = 4 cost
                    new(typeof(CharcoalItem), 4, staticIngredient: true),
                    // 0.1 x 20 = 2 cost
                    new(
                        "Vegetable",
                        20,
                        typeof(FarmingSkill),
                        typeof(FarmingLavishResourcesTalent)
                    ),
                    // ingredient cost = 6
                },
                items: new List<CraftingElement>
                {
                    // 1 x 5 = 5 value
                    new CraftingElement<CharcoalItem>(5),
                    // 0.3 x 3 = 0.9 value
                    new CraftingElement<OilItem>(3),
                    // total value = 5.9
                    // intentionally losing some value in the process
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(25, typeof(FarmingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiocharRecipe),
                start: 1.2f,
                skillType: typeof(FarmingSkill),
                typeof(FarmingFocusedSpeedTalent),
                typeof(FarmingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Biochar Charcoal Burning"),
                recipeType: typeof(BiocharRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(KilnObject), recipe: this);
        }
    }
}
