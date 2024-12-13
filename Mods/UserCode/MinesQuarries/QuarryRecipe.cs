namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 4)]
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
                    new("MortaredStone", 20, staticIngredient: true),
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

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class LimestoneQuarry : RecipeFamily
    {
        public LimestoneQuarry()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Limestone Quarry");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                    new("MortaredStone", 20, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<LimestoneQuarryItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(LimestoneQuarry),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(LimestoneQuarry));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class GraniteQuarry : RecipeFamily
    {
        public GraniteQuarry()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Granite Quarry");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                    new("MortaredStone", 20, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<GraniteQuarryItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GraniteQuarry),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GraniteQuarry));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class ShaleQuarry : RecipeFamily
    {
        public ShaleQuarry()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Shale Quarry");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                    new("MortaredStone", 20, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<ShaleQuarryItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ShaleQuarry),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(ShaleQuarry));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }
}
