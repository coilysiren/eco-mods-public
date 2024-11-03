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
                    new CraftingElement<PlasticItem>(2),
                    // sold @ 50% profit = 3.75 / 2 = 1.9 per plastic (two significant digits)
                }
            );
            Recipes = new List<Recipe> { recipe };
            // PlasticRecipe.ExperienceOnCraft
            ExperienceOnCraft = 1;
            // PlasticRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(45, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BioplasticRecipe),
                // PlasticRecipe.CraftMinutes * 2
                start: 3,
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
