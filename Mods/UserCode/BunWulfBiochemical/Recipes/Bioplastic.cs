namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
    [LocDescription("This is a slow recipe, plan to run it across multiple (3+) tables.")]
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
                    new CraftingElement<PlasticItem>(6),
                    // sold @ 50% profit = 6 / 6 = 1    per plastic
                    // sold @ 0%  profit = 4 / 6 = 0.66 per plastic
                    // with 0 cost crops = 3 / 6 = 0.5  per plastic
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            // PlasticRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BioplasticRecipe),
                start: 10,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Plant Based Bioplastic"),
                recipeType: typeof(BioplasticRecipe)
            );
            CraftingComponent.AddRecipe(
                tableType: typeof(ChemicalLaboratoryObject),
                recipeFamily: this
            );
        }
    }
}
