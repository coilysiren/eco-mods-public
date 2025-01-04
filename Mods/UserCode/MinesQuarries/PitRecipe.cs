namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    public partial class DirtPit : RecipeFamily
    {
        public DirtPit()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Dirt Pit");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                    new(typeof(StockpileItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<DirtPitItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(DirtPit),
                start: 1,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(DirtPit));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    public partial class SandPit : RecipeFamily
    {
        public SandPit()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sand Pit");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                    new(typeof(StockpileItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<SandPitItem>() }
            );
            this.ExperienceOnCraft = 0;
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SandPit),
                start: 1,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SandPit));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    public partial class ClayPit : RecipeFamily
    {
        public ClayPit()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Clay Pit");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                    new(typeof(StockpileItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<ClayPitItem>() }
            );
            this.ExperienceOnCraft = 0;
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ClayPit),
                start: 1,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(ClayPit));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }
}
