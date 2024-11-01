using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Pipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.World;
using Eco.World.Blocks;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(BiochemistSkill), 7)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", subPageName: "Biochemist Upgrade Item")]
    public partial class BiochemistUpgradeRecipe : RecipeFamily
    {
        public BiochemistUpgradeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BiochemistUpgrade",
                displayName: Localizer.DoStr("Biochemist Upgrade"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl4Item), 1, true),
                },
                items: new List<CraftingElement> { new CraftingElement<BiochemistUpgradeItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4; // Defines how much experience is gained when crafted.
            this.LaborInCalories = CreateLaborInCaloriesValue(9000, typeof(BiochemistSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiochemistUpgradeRecipe),
                start: 18,
                skillType: typeof(BiochemistSkill),
                typeof(BiochemistFocusedSpeedTalent),
                typeof(BiochemistParallelSpeedTalent)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Biochemist Upgrade"),
                recipeType: typeof(BiochemistUpgradeRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
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
