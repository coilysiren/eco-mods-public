namespace Mineshafts
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class CrudeIronOreMine : RecipeFamily
    {
        public CrudeIronOreMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Crude Iron Ore Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 5, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<CrudeIronMineshaftItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CrudeIronOreMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CrudeIronOreMine));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class CrudeIronOreMining : RecipeFamily
    {
        public CrudeIronOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Crude Iron Ore Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(AdobeItem), 25, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 25, staticIngredient: true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<IronOreItem>(20),
                    new CraftingElement<SandstoneItem>(80),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CrudeIronOreMining),
                start: 1.66f,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CrudeIronOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(CrudeIronMineshaftObject), recipe: this);
        }
    }
}
