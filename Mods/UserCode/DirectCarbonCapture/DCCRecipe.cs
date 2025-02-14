namespace DirectCarbonCapture
{
    using System.Collections.Generic;
    using BunWulfBioChemical;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class CarbonFilterRecipe : RecipeFamily
    {
        public CarbonFilterRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Carbon Filter",
                displayName: Localizer.DoStr("Carbon Filter"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        "Coal",
                        1,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    new(
                        typeof(PaperItem),
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    new(
                        typeof(PlasticItem),
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<CarbonFilterItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = this.CreateLaborInCaloriesValue(20, typeof(BiochemistSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CarbonFilterRecipe),
                start: 1,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Carbon Filter"),
                recipeType: typeof(CarbonFilterRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(MechanicsSkill), 2)]
    public partial class DirectCarbonCaptureRecipe : RecipeFamily
    {
        public DirectCarbonCaptureRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Direct Air Capture Fan",
                displayName: Localizer.DoStr("Direct Air Capture Fan"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(PumpJackItem), 1, staticIngredient: true),
                    new(typeof(BoilerItem), 1, staticIngredient: true),
                    new(typeof(PistonItem), 4, staticIngredient: true),
                },
                items: new List<CraftingElement> { new CraftingElement<DirectCarbonCaptureItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 25;
            this.LaborInCalories = this.CreateLaborInCaloriesValue(100, typeof(MechanicsSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(DirectCarbonCaptureRecipe),
                start: 20,
                skillType: typeof(MechanicsSkill),
                typeof(MechanicsFocusedSpeedTalent),
                typeof(MechanicsParallelSpeedTalent)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Direct Air Capture Fan"),
                recipeType: typeof(DirectCarbonCaptureRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(AssemblyLineObject), recipe: this);
        }
    }
}
