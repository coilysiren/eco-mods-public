#pragma warning disable IDE0005
using System.Collections.Generic;
#pragma warning restore IDE0005

using Eco.Gameplay.Components;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 0)]
    public partial class CornEthanolRecipe : RecipeFamily
    {
        public CornEthanolRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CornEthanol",
                displayName: Localizer.DoStr("Corn Ethanol"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(
                        "Vegetable",
                        10,
                        typeof(CuttingEdgeCookingSkill),
                        typeof(CuttingEdgeCookingLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<EthanolItem>(1) }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 0.5f; // Defines how much experience is gained when crafted.
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(CuttingEdgeCookingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CornEthanolRecipe),
                start: 1,
                skillType: typeof(CuttingEdgeCookingSkill),
                typeof(CuttingEdgeCookingFocusedSpeedTalent),
                typeof(CuttingEdgeCookingParallelSpeedTalent)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Corn Ethanol"),
                recipeType: typeof(CornEthanolRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
