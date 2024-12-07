namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class BioplasticRecipe : RecipeFamily
    {
        public BioplasticRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Bioplastic",
                displayName: Localizer.DoStr("Plant Based Bioplastic"),
                ingredients: new List<IngredientElement>
                {
                    // priced at 0.1 x 10 = 1
                    new(
                        "Vegetable",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // priced at 0.3 x 10 = 3
                    new(
                        "Fat",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // ingredient cost = 4
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<PlasticItem>(6),
                    // sold @ 50% profit = 6 / 6 = 1 per plastic
                }
            );
            Recipes = new List<Recipe> { recipe };
            // PlasticRecipe.ExperienceOnCraft
            ExperienceOnCraft = 1;
            // PlasticRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BioplasticRecipe),
                // PlasticRecipe.CraftMinutes * 8
                start: 12,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Plant Based Bioplastic"),
                recipeType: typeof(BioplasticRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
