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
                    // priced at 0.15 x 10 = 1.5
                    new(
                        "Fat",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
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
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarboNylonRecipe),
                // NylonRecipe.CraftMinutes * 2
                start: 3,
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
