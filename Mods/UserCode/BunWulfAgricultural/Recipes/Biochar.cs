namespace BunWulfAgricultural
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

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
                    // 0.5 x 4 = 2 cost
                    new(typeof(CharcoalItem), 4, staticIngredient: true),
                    // 0.1 x 20 = 2 cost
                    new(
                        "Vegetable",
                        20,
                        typeof(FarmingSkill),
                        typeof(FarmingLavishResourcesTalent)
                    ),
                    // ingredient cost = 4
                },
                items: new List<CraftingElement>
                {
                    // 0.5 * 6 = 3
                    new CraftingElement<CharcoalItem>(6),
                    // 0.2 * 4 = 0.8
                    new CraftingElement<OilItem>(2),
                    // products value = 3.8
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(25, typeof(FarmingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiocharRecipe),
                start: 1,
                skillType: typeof(FarmingSkill),
                typeof(FarmingFocusedSpeedTalent),
                typeof(FarmingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Biochar Charcoal Burning"),
                recipeType: typeof(BiocharRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(KilnObject), recipeFamily: this);
        }
    }
}
