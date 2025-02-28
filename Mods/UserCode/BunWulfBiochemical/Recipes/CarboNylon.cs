namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
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
                    new CraftingElement<NylonItem>(18),
                    // sold @ 50% profit = 6 / 18 = 0.33 per nylon
                    // sold @ 0%  profit = 4 / 18 = 0.22 per nylon
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            // NylonRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarboNylonRecipe),
                start: 10,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Coal Based Nylon"),
                recipeType: typeof(CarboNylonRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
