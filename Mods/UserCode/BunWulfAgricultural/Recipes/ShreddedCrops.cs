#pragma warning disable IDE0005
using System.Collections.Generic;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class ShreddedCropsRecipe : RecipeFamily
    {
        public ShreddedCropsRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "ShreddedCrops",
                displayName: Localizer.DoStr("Shredded Crops"),
                ingredients: new List<IngredientElement>
                {
                    new("Vegetable", 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                },
                items: new List<CraftingElement> { new CraftingElement<PlantFibersItem>(5) }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 0.1f;
            LaborInCalories = CreateLaborInCaloriesValue(200, typeof(FarmingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ShreddedCropsRecipe),
                start: 0.5f,
                skillType: typeof(FarmingSkill)
            );
            Initialize(
                displayText: Localizer.DoStr("Shredded Crops"),
                recipeType: typeof(ShreddedCropsRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipe: this);
        }
    }
}
