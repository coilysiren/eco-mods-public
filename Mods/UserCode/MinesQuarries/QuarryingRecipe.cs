namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 4)]
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
                items: new List<CraftingElement> { new CraftingElement<SandstoneItem>(200) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(5000, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SandstoneQuarrying),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SandstoneQuarrying));
            CraftingComponent.AddRecipe(tableType: typeof(SandstoneQuarryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class LimestoneQuarrying : RecipeFamily
    {
        public LimestoneQuarrying()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Limestone Quarrying");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<LimestoneItem>(200) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(5000, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(LimestoneQuarrying),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(LimestoneQuarrying));
            CraftingComponent.AddRecipe(tableType: typeof(LimestoneQuarryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class GraniteQuarrying : RecipeFamily
    {
        public GraniteQuarrying()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Granite Quarrying");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<GraniteItem>(200) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(5000, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GraniteQuarrying),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GraniteQuarrying));
            CraftingComponent.AddRecipe(tableType: typeof(GraniteQuarryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 4)]
    public partial class ShaleQuarrying : RecipeFamily
    {
        public ShaleQuarrying()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Shale Quarrying");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(StonePickaxeItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<ShaleItem>(200) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(5000, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ShaleQuarrying),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(ShaleQuarrying));
            CraftingComponent.AddRecipe(tableType: typeof(ShaleQuarryObject), recipe: this);
        }
    }
}
