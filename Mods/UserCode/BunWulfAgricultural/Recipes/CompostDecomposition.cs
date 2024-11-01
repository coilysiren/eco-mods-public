#pragma warning disable IDE0005
using System.Collections.Generic;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class CompostDecompositionRecipe : RecipeFamily
    {
        public CompostDecompositionRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "CompostDecomposition",
                displayName: Localizer.DoStr("Compost Decomposition"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(CompostItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<DirtItem>(1) }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.1f;
            LaborInCalories = CreateLaborInCaloriesValue(200, typeof(FarmingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CompostDecompositionRecipe),
                start: 0.5f,
                skillType: typeof(FarmingSkill)
            );
            Initialize(
                displayText: Localizer.DoStr("Compost Decomposition"),
                recipeType: typeof(CompostDecompositionRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipe: this);
        }
    }
}
