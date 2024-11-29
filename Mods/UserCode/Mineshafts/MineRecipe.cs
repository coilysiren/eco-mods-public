namespace Mineshafts
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

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
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(start: 10);
            this.Initialize(displayText: displayName, recipeType: typeof(CrudeIronOreMine));
            CraftingComponent.AddRecipe(tableType: typeof(ToolBenchObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class IronOreMine : RecipeFamily
    {
        public IronOreMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Ore Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 25, typeof(CarpenterSkill)),
                    new(typeof(HewnLogItem), 25, typeof(CarpenterSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<IronMineshaftItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreMine),
                start: 10,
                skillType: typeof(CarpentrySkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreMine));
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class CopperOreMine : RecipeFamily
    {
        public CopperOreMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Ore Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 25, typeof(CarpenterSkill)),
                    new(typeof(HewnLogItem), 25, typeof(CarpenterSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CopperMineshaftItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreMine),
                start: 10,
                skillType: typeof(CarpentrySkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreMine));
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(CarpentrySkill), 1)]
    public partial class GoldOreMine : RecipeFamily
    {
        public GoldOreMine()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Ore Mine");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 25, typeof(CarpenterSkill)),
                    new(typeof(HewnLogItem), 25, typeof(CarpenterSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<GoldMineshaftItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(100);
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreMine),
                start: 10,
                skillType: typeof(CarpentrySkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreMine));
            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipe: this);
        }
    }
}
