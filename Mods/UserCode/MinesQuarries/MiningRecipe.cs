namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class IronOreBlasting : RecipeFamily
    {
        public IronOreBlasting()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Blasting");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 10, typeof(MiningSkill)),
                    new(typeof(DynamiteItem), 3, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedIronOreItem>(30),
                    new CraftingElement<CrushedSandstoneItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreBlasting),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class IronOreBoring : RecipeFamily
    {
        public IronOreBoring()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Boring");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(MiningChargeItem), 1, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedIronOreItem>(30),
                    new CraftingElement<CrushedSandstoneItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreBoring),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class CopperOreBlasting : RecipeFamily
    {
        public CopperOreBlasting()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Blasting");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 10, typeof(MiningSkill)),
                    new(typeof(DynamiteItem), 3, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCopperOreItem>(30),
                    new CraftingElement<CrushedGraniteItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreBlasting),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class CopperOreBoring : RecipeFamily
    {
        public CopperOreBoring()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Boring");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(MiningChargeItem), 1, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCopperOreItem>(30),
                    new CraftingElement<CrushedGraniteItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreBoring),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class GoldOreBlasting : RecipeFamily
    {
        public GoldOreBlasting()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Blasting");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 10, typeof(MiningSkill)),
                    new(typeof(DynamiteItem), 3, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedGoldOreItem>(30),
                    new CraftingElement<CrushedGraniteItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreBlasting),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class GoldOreBoring : RecipeFamily
    {
        public GoldOreBoring()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Boring");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(MiningChargeItem), 1, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedGoldOreItem>(30),
                    new CraftingElement<CrushedGraniteItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreBoring),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class CoalBlasting : RecipeFamily
    {
        public CoalBlasting()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Coal Blasting");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 10, typeof(MiningSkill)),
                    new(typeof(DynamiteItem), 3, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCoalItem>(30),
                    new CraftingElement<CrushedShaleItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(600, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalBlasting),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(CoalMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class CoalBoring : RecipeFamily
    {
        public CoalBoring()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Coal Boring");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(MiningChargeItem), 1, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCoalItem>(30),
                    new CraftingElement<CrushedShaleItem>(30),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(600, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalBoring),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalBoring));
            CraftingComponent.AddRecipe(tableType: typeof(CoalMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class SulfurBlasting : RecipeFamily
    {
        public SulfurBlasting()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sulfur Blasting");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(IronPickaxeItem), 1, staticIngredient: true),
                    new(typeof(HempMooringRopeItem), 10, typeof(MiningSkill)),
                    new(typeof(DynamiteItem), 3, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedSulfurItem>(30),
                    new CraftingElement<CrushedSandstoneItem>(15),
                    new CraftingElement<CrushedLimestoneItem>(15),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(600, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurBlasting),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(SulfurMineObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 7)]
    public partial class SulfurBoring : RecipeFamily
    {
        public SulfurBoring()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sulfur Boring");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(MiningChargeItem), 1, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedSulfurItem>(30),
                    new CraftingElement<CrushedSandstoneItem>(15),
                    new CraftingElement<CrushedLimestoneItem>(15),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(600, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurBoring),
                start: 8,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurBoring));
            CraftingComponent.AddRecipe(tableType: typeof(SulfurMineObject), recipeFamily: this);
        }
    }
}
