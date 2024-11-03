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
    [RequiresSkill(typeof(BiochemistSkill), 0)]
    public partial class VegetableEthanolRecipe : RecipeFamily
    {
        public VegetableEthanolRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "VegetableEthanol",
                displayName: Localizer.DoStr("Vegetable Ethanol"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        "Vegetable",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<EthanolItem>(1) }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.5f; // Defines how much experience is gained when crafted.
            LaborInCalories = CreateLaborInCaloriesValue(60, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(VegetableEthanolRecipe),
                start: 1,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Vegetable Ethanol"),
                recipeType: typeof(VegetableEthanolRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }
}
