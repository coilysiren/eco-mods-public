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
    [Ecopedia("Items", "Products", subPageName: "Wet Brick Item")]
    public partial class WetBrickRecipe : RecipeFamily
    {
        public WetBrickRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "WetBrick",
                displayName: Localizer.DoStr("Wet Brick"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(SandItem),
                        3,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        typeof(ClayItem),
                        12,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(
                        typeof(WoodenMoldItem),
                        4,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<WetBrickItem>(4),
                    new CraftingElement<WoodenMoldItem>(
                        typeof(ConstructionWorkerSkill),
                        2,
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(100, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(WetBrickRecipe),
                start: 0.5f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(
                displayText: Localizer.DoStr("Wet Brick"),
                recipeType: typeof(WetBrickRecipe)
            );
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(PotteryTableObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
