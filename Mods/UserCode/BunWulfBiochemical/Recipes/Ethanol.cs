namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 1)]
    public partial class VegetableEthanolRecipe : RecipeFamily
    {
        public VegetableEthanolRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "VegetableEthanol",
                displayName: Localizer.DoStr("Vegetable Ethanol"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        "Vegetable",
                        10,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement> { new CraftingElement<EthanolItem>(1) }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(60, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(VegetableEthanolRecipe),
                start: 1,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Vegetable Ethanol"),
                recipeType: typeof(VegetableEthanolRecipe)
            );
            CraftingComponent.AddRecipe(
                tableType: typeof(ChemicalLaboratoryObject),
                recipeFamily: this
            );
        }
    }
}
