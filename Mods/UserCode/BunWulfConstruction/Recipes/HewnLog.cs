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
    [Ecopedia("Blocks", "Building Materials", subPageName: "Hewn Log Item")]
    public partial class ConstructionHewnLogRecipe : RecipeFamily
    {
        public ConstructionHewnLogRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "HewnLog",
                displayName: Localizer.DoStr("Builder Grade Hewn Log"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(DowelItem), 2, typeof(ConstructionWorkerSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<HewnLogItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionHewnLogRecipe),
                start: 0.16f,
                skillType: typeof(ConstructionWorkerSkill)
            );

            Initialize(
                displayText: Localizer.DoStr("Builder Grade Hewn Log"),
                recipeType: typeof(ConstructionHewnLogRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }
    }
}
