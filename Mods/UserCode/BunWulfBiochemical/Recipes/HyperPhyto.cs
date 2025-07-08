namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(BiochemistSkill), 2)]
    [RequiresModule(typeof(ChemicalLaboratoryObject))]
    public partial class DryHyperPhyto : RecipeFamily
    {
        public DryHyperPhyto()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "DryHyperPhyto",
                displayName: Localizer.DoStr("Dry Hyperaccumulator Phytoremediation"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(TailingsItem), 100, staticIngredient: true),
                    new(typeof(DirtItem), 100, staticIngredient: true),
                    new(
                        typeof(CompostFertilizerItem),
                        100,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    new(
                        typeof(SunflowerSeedItem),
                        100,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<DirtItem>(100),
                    new CraftingElement<SunflowerItem>(100),
                    new CraftingElement<IronConcentrateItem>(10),
                    new CraftingElement<CopperConcentrateItem>(3),
                    new CraftingElement<GoldConcentrateItem>(1),
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(2000, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(DryHyperPhyto),
                start: 360,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Dry Hyperaccumulator Phytoremediation"),
                recipeType: typeof(DryHyperPhyto)
            );
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(BiochemistSkill), 2)]
    [RequiresModule(typeof(ChemicalLaboratoryObject))]
    public partial class WetHyperPhyto : RecipeFamily
    {
        public WetHyperPhyto()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "WetHyperPhyto",
                displayName: Localizer.DoStr("Wet Hyperaccumulator Phytoremediation"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(WetTailingsItem), 100, staticIngredient: true),
                    new(typeof(DirtItem), 100, staticIngredient: true),
                    new(
                        typeof(CompostFertilizerItem),
                        100,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                    new(
                        typeof(SunflowerSeedItem),
                        100,
                        typeof(BiochemistSkill),
                        typeof(BiochemistLavishResourcesTalent)
                    ),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<DirtItem>(100),
                    new CraftingElement<SunflowerItem>(100),
                    new CraftingElement<IronConcentrateItem>(10),
                    new CraftingElement<CopperConcentrateItem>(3),
                    new CraftingElement<GoldConcentrateItem>(1),
                }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 1;
            LaborInCalories = CreateLaborInCaloriesValue(2000, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(WetHyperPhyto),
                start: 360,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Wet Hyperaccumulator Phytoremediation"),
                recipeType: typeof(WetHyperPhyto)
            );
            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }
    }
}
