namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 1)]
    public partial class SandstoneQuarrying : RecipeFamily
    {
        public SandstoneQuarrying()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sandstone Quarrying");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<SandstoneItem>(1000) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(4000, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SandstoneQuarrying),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SandstoneQuarrying));
            CraftingComponent.AddRecipe(tableType: typeof(SandstoneQuarryObject), recipe: this);
        }
    }
}
