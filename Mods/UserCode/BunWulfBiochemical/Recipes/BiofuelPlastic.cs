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
    [RequiresSkill(typeof(BiochemistSkill), 3)]
    public partial class PlasticBiofuel : RecipeFamily
    {
        public PlasticBiofuel()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "PlasticBiofuel",
                displayName: Localizer.DoStr("Plastic Container Biofuel, 50% Ethanol"),
                ingredients: new List<IngredientElement>
                {
                    // priced at 1 x 2 = 2
                    new(typeof(EthanolItem), 2, staticIngredient: true),
                    // priced at 0.2 x 2 = 0.4
                    new(
                        typeof(PlasticItem),
                        2,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    // ingredient cost = 2.4
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<BiodieselItem>(1),
                    // sold @ 50% profit = 3.6 per biodiesel
                }
            );
            Recipes = new List<Recipe> { recipe };
            // BiodieselRecipe.ExperienceOnCraft
            ExperienceOnCraft = 0.5f;
            // BiodieselRecipe.LaborInCalories / 4
            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(PlasticBiofuel),
                // BiodieselRecipe.CraftMinutes * 2
                start: 1.6f,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Plastic Container Biofuel, 50% Ethanol"),
                recipeType: typeof(PlasticBiofuel)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
