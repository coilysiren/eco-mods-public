#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(ConstructionWorkerSkill), 1)]
    [Ecopedia("Blocks", "Building Materials", subPageName: "Mortared Stone Item")]
    public partial class ConstructionMortaredStoneRecipe : RecipeFamily
    {
        public ConstructionMortaredStoneRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "MortaredStone",
                displayName: Localizer.DoStr("Builder Grade Mortared Stone"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(MortarItem),
                        1,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        "Rock",
                        4,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<MortaredStoneItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(15, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionMortaredStoneRecipe),
                start: 0.16f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Builder Grade Mortared Stone"),
                recipeType: typeof(ConstructionMortaredStoneRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
