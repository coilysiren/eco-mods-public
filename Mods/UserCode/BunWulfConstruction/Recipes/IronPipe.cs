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
    [RequiresSkill(typeof(ConstructionWorkerSkill), 4)]
    [Ecopedia("Blocks", "Pipes", subPageName: "Builder Grade Iron Pipe Item")]
    public partial class ConstructionIronPipeRecipe : RecipeFamily
    {
        public ConstructionIronPipeRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "IronPipe",
                displayName: Localizer.DoStr("Builder Grade Iron Pipe"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(IronBarItem),
                        1,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<IronPipeItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(15, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionIronPipeRecipe),
                start: 0.8f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            Initialize(
                displayText: Localizer.DoStr("Builder Grade Iron Pipe"),
                recipeType: typeof(ConstructionIronPipeRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(AnvilObject), recipe: this);
        }
    }
}
