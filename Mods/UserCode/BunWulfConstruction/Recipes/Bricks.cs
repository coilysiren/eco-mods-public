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
    [RequiresSkill(typeof(ConstructionWorkerSkill), 2)]
    [Ecopedia("Blocks", "Building Materials", subPageName: "Brick Item")]
    public partial class ConstructionBrickRecipe : RecipeFamily
    {
        public ConstructionBrickRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Brick",
                displayName: Localizer.DoStr("Builder Grade Brick"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(WetBrickItem),
                        1,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        typeof(MortarItem),
                        4,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<BrickItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(15, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ConstructionBrickRecipe),
                start: 0.32f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Builder Grade Brick"),
                recipeType: typeof(ConstructionBrickRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(KilnObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
