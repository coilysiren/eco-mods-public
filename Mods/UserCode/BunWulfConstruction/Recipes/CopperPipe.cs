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
    [Ecopedia("Blocks", "Pipes", subPageName: "Builder Grade Copper Pipe Item")]
    public partial class ConstructionCopperPipeRecipe : RecipeFamily
    {
        public ConstructionCopperPipeRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "CopperPipe",
                displayName: Localizer.DoStr("Builder Grade Copper Pipe"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(CopperBarItem),
                        1,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<CopperPipeItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(15, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionCopperPipeRecipe),
                start: 0.8f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Builder Grade Copper Pipe"),
                recipeType: typeof(ConstructionCopperPipeRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(AnvilObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
