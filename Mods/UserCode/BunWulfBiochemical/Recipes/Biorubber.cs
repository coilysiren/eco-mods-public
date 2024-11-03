#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
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
                    // priced at 0.15 x 40 = 6
                    new(
                        "Fat",
                        40,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // ingredient cost = 14
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SyntheticRubberItem>(10),
                    // sold @ 50% profit = 21 / 10 = 1.1 per rubber (two significant digits)
                }
            );
            Recipes = new List<Recipe> { recipe };
            // SyntheticRubberRecipe.ExperienceOnCraft
            ExperienceOnCraft = 1;
            // SyntheticRubberRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiorubberRecipe),
                // SyntheticRubberRecipe.CraftMinutes * 2
                start: 3,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Tree Rubber"),
                recipeType: typeof(BiorubberRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
