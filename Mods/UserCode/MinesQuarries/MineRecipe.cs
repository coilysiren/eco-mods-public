namespace Mines
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class IronMine : RecipeFamily
    {
        public IronMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(WoodenElevatorItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<IronMineItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronMine));
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class CopperMine : RecipeFamily
    {
        public CopperMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(WoodenElevatorItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<CopperMineItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperMine));
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class GoldMine : RecipeFamily
    {
        public GoldMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(WoodenElevatorItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<GoldMineItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldMine));
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class CoalMine : RecipeFamily
    {
        public CoalMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Coal Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(WoodenElevatorItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<CoalMineItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalMine));
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class SulfurMine : RecipeFamily
    {
        public SulfurMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sulfur Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(WoodenElevatorItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<SulfurMineItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurMine),
                start: 10,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurMine));
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }
}
