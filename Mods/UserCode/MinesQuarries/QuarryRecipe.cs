namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 1)]
    public partial class SandstoneQuarry : RecipeFamily
    {
        public SandstoneQuarry()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sandstone Quarry");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<SandstoneQuarryItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SandstoneQuarry),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SandstoneQuarry));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }
}
