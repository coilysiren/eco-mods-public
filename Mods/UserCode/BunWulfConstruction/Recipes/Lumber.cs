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
    [RequiresSkill(typeof(ConstructionWorkerSkill), 3)]
    [Ecopedia("Blocks", "Building Materials", subPageName: "Lumber Item")]
    public partial class ConstructionLumberRecipe : RecipeFamily
    {
        public ConstructionLumberRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Lumber",
                displayName: Localizer.DoStr("Builder Grade Lumber"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(NailItem),
                        2,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        typeof(FlaxseedOilItem),
                        0.5f,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        "WoodBoard",
                        10,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<LumberItem>(2) }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(60, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionLumberRecipe),
                start: 0.32f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Builder Grade Lumber"),
                recipeType: typeof(ConstructionLumberRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SawmillObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
