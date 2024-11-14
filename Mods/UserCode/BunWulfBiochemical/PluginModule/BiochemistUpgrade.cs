#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(BiochemistSkill), 7)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", subPageName: "Biochemist Upgrade Item")]
    public partial class BiochemistUpgradeRecipe : RecipeFamily
    {
        public BiochemistUpgradeRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "BiochemistUpgrade",
                displayName: Localizer.DoStr("Biochemist Upgrade"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl4Item), 1, true),
                },
                items: new List<CraftingElement> { new CraftingElement<BiochemistUpgradeItem>() }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 4; // Defines how much experience is gained when crafted.
            LaborInCalories = CreateLaborInCaloriesValue(9000, typeof(BiochemistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiochemistUpgradeRecipe),
                start: 18,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Biochemist Upgrade"),
                recipeType: typeof(BiochemistUpgradeRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ChemicalLaboratoryObject), recipe: this);
        }
    }

    [Serialized]
    [LocDisplayName("Biochemist Upgrade")]
    [LocDescription(
        "Modern Upgrade that greatly increases efficiency when crafting Biochemist recipes."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", createAsSubPage: true)] //_If_EcopediaPage_
    [Tag("Upgrade")]
    public partial class BiochemistUpgradeItem : EfficiencyModule
    {
        public BiochemistUpgradeItem()
            : base(
                ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
                0.5f + 0.05f,
                typeof(BiochemistSkill),
                0.5f
            ) { }
    }
}
