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
    [Ecopedia("Blocks", "Pipes", subPageName: "Copper Pipe Item")]
    public partial class CopperPipeRecipe : RecipeFamily
    {
        public CopperPipeRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "CopperPipe",
                displayName: Localizer.DoStr("Copper Pipe"),
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
                beneficiary: typeof(CopperPipeRecipe),
                start: 0.8f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Copper Pipe"),
                recipeType: typeof(CopperPipeRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(AnvilObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
