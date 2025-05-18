namespace BunWulfAgricultural
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(GatheringSkill), 7)]
    [RequiresModule(typeof(FarmersTableObject))]
    [LocDescription("An advanced recipe for processing lots of flax stems at once.")]
    public partial class FlaxFiberBulk : RecipeFamily
    {
        public FlaxFiberBulk()
        {
            string name = "Flax Fiber Bulk Processing";
            Recipe recipe = new();
            recipe.Init(
                name: name.Replace(" ", string.Empty),
                displayName: Localizer.DoStr(name),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(FlaxStemItem), 40, typeof(GatheringSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<FlaxFiberItem>(20),
                    new CraftingElement<FlaxSeedItem>(10),
                    new CraftingElement<PlantFibersItem>(40),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = this.CreateLaborInCaloriesValue(300, typeof(GatheringSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(FlaxFiberBulk),
                start: 3,
                skillType: typeof(GatheringSkill)
            );
            this.Initialize(displayText: Localizer.DoStr(name), recipeType: typeof(FlaxFiberBulk));
            CraftingComponent.AddRecipe(
                tableType: typeof(FiberScutchingStationObject),
                recipeFamily: this
            );
        }
    }
}
