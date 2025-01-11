namespace MinesQuarries
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    public partial class DirtDigging : RecipeFamily
    {
        public DirtDigging()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Dirt Digging");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<DirtItem>(100) }
            );
            this.ExperienceOnCraft = 0;
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(
                5000,
                typeof(SelfImprovementSkill)
            );
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(DirtDigging),
                start: 2,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(DirtDigging));
            CraftingComponent.AddRecipe(tableType: typeof(DirtPitObject), recipe: this);
        }
    }

    public partial class SandDigging : RecipeFamily
    {
        public SandDigging()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Sand Digging");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<SandItem>(100) }
            );
            this.ExperienceOnCraft = 0;
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(
                5000,
                typeof(SelfImprovementSkill)
            );
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SandDigging),
                start: 4,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(SandDigging));
            CraftingComponent.AddRecipe(tableType: typeof(SandPitObject), recipe: this);
        }
    }

    public partial class ClayDigging : RecipeFamily
    {
        public ClayDigging()
        {
            Recipe recipe = new();
            LocString displayName = Localizer.DoStr("Clay Digging");
            recipe.Init(
                name: this.GetType().Name,
                displayName: displayName,
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WoodenShovelItem), 1, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<ClayItem>(100) }
            );
            this.ExperienceOnCraft = 0;
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = this.CreateLaborInCaloriesValue(
                5000,
                typeof(SelfImprovementSkill)
            );
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ClayDigging),
                start: 4,
                skillType: typeof(SelfImprovementSkill)
            );
            this.Initialize(displayText: displayName, recipeType: typeof(ClayDigging));
            CraftingComponent.AddRecipe(tableType: typeof(ClayPitObject), recipe: this);
        }
    }
}
