namespace BunWulfAgricultural
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(PaperMillingSkill), 1)]
    public partial class FabricPaperRecipe : RecipeFamily
    {
        public FabricPaperRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "FabricPaper",
                displayName: Localizer.DoStr("Fabric Paper"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        "Fabric",
                        6,
                        typeof(PaperMillingSkill),
                        typeof(PaperMillingLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<PaperItem>() }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(20, typeof(PaperMillingSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(FabricPaperRecipe),
                start: 0.1f,
                skillType: typeof(PaperMillingSkill),
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Fabric Paper"),
                recipeType: typeof(FabricPaperRecipe)
            );
            CraftingComponent.AddRecipe(
                tableType: typeof(SmallPaperMachineObject),
                recipeFamily: this
            );
        }
    }
}
