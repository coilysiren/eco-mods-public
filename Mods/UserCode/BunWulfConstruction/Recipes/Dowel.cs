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
    [RequiresSkill(typeof(LoggingSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Dowel Item")]
    public partial class ConstructionDowelRecipe : RecipeFamily
    {
        public ConstructionDowelRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Dowel",
                displayName: Localizer.DoStr("Builder Grade Dowel"),
                ingredients: new List<IngredientElement> { },
                items: new List<CraftingElement> { new CraftingElement<DowelItem>(16) }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(40, typeof(LoggingSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionDowelRecipe),
                start: 0.4f,
                skillType: typeof(LoggingSkill)
            );

            Initialize(
                displayText: Localizer.DoStr("Builder Grade Dowel"),
                recipeType: typeof(ConstructionDowelRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }
    }
}
