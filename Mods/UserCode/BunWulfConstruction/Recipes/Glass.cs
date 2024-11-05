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
    [Ecopedia("Blocks", "Building Materials", subPageName: "Glass Item")]
    public partial class GlassRecipe : RecipeFamily
    {
        public GlassRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Glass",
                displayName: Localizer.DoStr("Glass"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(SandItem),
                        4,
                        typeof(ConstructionWorkerSkill),
                        typeof(ConstructionWorkerLavishResourcesTalent)
                    ),
                    new(typeof(CrushedLimestoneItem), 1, true),
                },
                items: new List<CraftingElement> { new CraftingElement<GlassItem>() }
            );
            Recipes = new List<Recipe> { recipe };

            LaborInCalories = CreateLaborInCaloriesValue(30, typeof(ConstructionWorkerSkill));

            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GlassRecipe),
                start: 1.2f,
                skillType: typeof(ConstructionWorkerSkill),
                typeof(ConstructionWorkerFocusedSpeedTalent),
                typeof(ConstructionWorkerParallelSpeedTalent)
            );

            ModsPreInitialize();
            Initialize(displayText: Localizer.DoStr("Glass"), recipeType: typeof(GlassRecipe));
            ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(GlassworksObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
