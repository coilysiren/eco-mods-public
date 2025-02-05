namespace BunWulfAgricultural
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(FarmingSkill), 3)]
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
            LaborInCalories = CreateLaborInCaloriesValue(5, typeof(FarmingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CompostDecompositionRecipe),
                start: 2,
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
