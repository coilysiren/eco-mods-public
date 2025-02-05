namespace BunWulfAgricultural
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(PaperMillingSkill), 1)]
    public partial class RicePaperRecipe : RecipeFamily
    {
        public RicePaperRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "RicePaper",
                displayName: Localizer.DoStr("Rice Paper"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(RiceItem),
                        40,
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
                beneficiary: typeof(RicePaperRecipe),
                start: 0.1f,
                skillType: typeof(PaperMillingSkill),
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Rice Paper"),
                recipeType: typeof(RicePaperRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(SmallPaperMachineObject), recipe: this);
        }
    }
}
