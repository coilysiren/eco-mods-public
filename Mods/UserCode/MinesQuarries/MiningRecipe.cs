namespace Mines
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    // [RequiresSkill(typeof(MiningSkill), 6)]
    // public partial class CrudeIronOreMining : RecipeFamily
    // {
    //     public CrudeIronOreMining()
    //     {
    //         Recipe recipe = new();
    //         LocString displayName = Localizer.DoStr("Crude Iron Ore Mining");
    //         recipe.Init(
    //             name: this.GetType().Name,
    //             displayName: displayName,
    //             ingredients: new List<IngredientElement>
    //             {
    //                 new(typeof(AdobeItem), 12, typeof(MiningSkill)),
    //                 new(typeof(HempMooringRopeItem), 6, typeof(MiningSkill)),
    //             },
    //             items: new List<CraftingElement>
    //             {
    //                 new CraftingElement<IronOreItem>(20),
    //                 new CraftingElement<SandstoneItem>(80),
    //             }
    //         );
    //         this.Recipes = new List<Recipe> { recipe };
    //         this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
    //         this.CraftMinutes = CreateCraftTimeValue(
    //             beneficiary: typeof(CrudeIronOreMining),
    //             start: 4,
    //             skillType: typeof(MiningSkill)
    //         );
    //         this.Initialize(displayText: displayName, recipeType: typeof(CrudeIronOreMining));
    //         CraftingComponent.AddRecipe(tableType: typeof(CrudeIronMineObject), recipe: this);
    //     }
    // }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class IronOreMining : RecipeFamily
    {
        public IronOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Iron Ore Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 6, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<IronOreItem>(20),
                    new CraftingElement<SandstoneItem>(80),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(600, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(IronOreMining),
                start: 2,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(IronOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(IronMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class CopperOreMining : RecipeFamily
    {
        public CopperOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Copper Ore Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 6, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CopperOreItem>(20),
                    new CraftingElement<GraniteItem>(80),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1200, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CopperOreMining),
                start: 6,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(CopperOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(CopperMineObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MiningSkill), 6)]
    public partial class GoldOreMining : RecipeFamily
    {
        public GoldOreMining()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Gold Ore Mining");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(HempMooringRopeItem), 6, typeof(MiningSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GoldOreItem>(20),
                    new CraftingElement<GraniteItem>(80),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(1800, typeof(MiningSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(GoldOreMining),
                start: 12,
                skillType: typeof(MiningSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(GoldOreMining));
            CraftingComponent.AddRecipe(tableType: typeof(GoldMineObject), recipe: this);
        }
    }
}
