namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class IronOreMining : RecipeFamily
    {
        public IronOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<IronOreItem>(20),
                    new CraftingElement<SandstoneItem>(60),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreMining),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipe: this);
        }
    }

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
                    new(typeof(DynamiteItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedIronOreItem>(20),
                    new CraftingElement<CrushedSandstoneItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreBlasting),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipe: this);
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
                    new(typeof(MiningChargeItem), 2, typeof(MiningSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CrushedIronOreItem>(20) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreBoring),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class CopperOreMining : RecipeFamily
    {
        public CopperOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CopperOreItem>(20),
                    new CraftingElement<GraniteItem>(60),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreMining),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipe: this);
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
                    new(typeof(DynamiteItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCopperOreItem>(20),
                    new CraftingElement<CrushedGraniteItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreBlasting),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipe: this);
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
                    new(typeof(MiningChargeItem), 2, typeof(MiningSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CrushedCopperOreItem>(20) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreBoring),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class GoldOreMining : RecipeFamily
    {
        public GoldOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 8, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GoldOreItem>(20),
                    new CraftingElement<GraniteItem>(60),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreMining),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipe: this);
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
                    new(typeof(DynamiteItem), 8, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedGoldOreItem>(20),
                    new CraftingElement<CrushedGraniteItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreBlasting),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipe: this);
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
                    new(typeof(MiningChargeItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CrushedGoldOreItem>(20) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreBoring),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreBoring));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class CoalMining : RecipeFamily
    {
        public CoalMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Coal Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CoalItem>(20),
                    new CraftingElement<SlagItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalMining),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalMining));
            CraftingComponent.AddRecipe(tableType: typeof(CoalMineObject), recipe: this);
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
                    new(typeof(DynamiteItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedCoalItem>(20),
                    new CraftingElement<CrushedSlagItem>(20),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalBlasting),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(CoalMineObject), recipe: this);
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
                    new(typeof(MiningChargeItem), 2, typeof(MiningSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CrushedCoalItem>(20) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CoalBoring),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CoalBoring));
            CraftingComponent.AddRecipe(tableType: typeof(CoalMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 5)]
    public partial class SulfurMining : RecipeFamily
    {
        public SulfurMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sulfur Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<SulfurItem>(20),
                    new CraftingElement<SlagItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurMining),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurMining));
            CraftingComponent.AddRecipe(tableType: typeof(SulfurMineObject), recipe: this);
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
                    new(typeof(DynamiteItem), 4, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CrushedSulfurItem>(20),
                    new CraftingElement<CrushedSlagItem>(20),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurBlasting),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurBlasting));
            CraftingComponent.AddRecipe(tableType: typeof(SulfurMineObject), recipe: this);
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
                    new(typeof(MiningChargeItem), 2, typeof(MiningSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<CrushedSulfurItem>(20) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SulfurBoring),
                start: 4,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SulfurBoring));
            CraftingComponent.AddRecipe(tableType: typeof(SulfurMineObject), recipe: this);
        }
    }
}
