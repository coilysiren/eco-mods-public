namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 2)]
    public partial class BiorubberRecipe : RecipeFamily
    {
        public BiorubberRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Biorubber",
                displayName: Localizer.DoStr("Tree Rubber"),
                ingredients: new List<IngredientElement>
                {
                    // priced at 0.2 x 40 = 8
                    new(
                        typeof(CeibaLogItem),
                        40,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // priced at 0.2 x 40 = 8
                    new(
                        "Fat",
                        40,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // priced at 1
                    new(typeof(EthanolItem), 1, true),
                    // ingredient cost = 20
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SyntheticRubberItem>(30),
                    // sold @ 50% profit = 30 / 30 = 1    per rubber
                    // sold @ 0%  profit = 20 / 30 = 0.66 per rubber
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            // SyntheticRubberRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiorubberRecipe),
                start: 10,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Tree Rubber"),
                recipeType: typeof(BiorubberRecipe)
            );
            CraftingComponent.AddRecipe(
                tableType: typeof(ChemicalLaboratoryObject),
                recipeFamily: this
            );
        }
    }
}
