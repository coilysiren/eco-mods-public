namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Scholars Modern Upgrade 1")]
    [LocDescription(
        "A thinking Econian's Modern Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Modern Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("ModernUpgrade")]
    public partial class ScholarsModernUpgradeLvl1Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Modern Upgrade 1");

        // base is 0.9
        public ScholarsModernUpgradeLvl1Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.85f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Modern Upgrade 2")]
    [LocDescription(
        "A thinking Econian's Modern Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Modern Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("ModernUpgrade")]
    public partial class ScholarsModernUpgradeLvl2Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Modern Upgrade 2");

        // base is 0.75
        public ScholarsModernUpgradeLvl2Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.7f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Modern Upgrade 3")]
    [LocDescription(
        "A thinking Econian's Modern Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Modern Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("ModernUpgrade")]
    public partial class ScholarsModernUpgradeLvl3Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Modern Upgrade 3");

        // base is 0.6
        public ScholarsModernUpgradeLvl3Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.55f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Modern Upgrade 4")]
    [LocDescription(
        "A thinking Econian's Modern Upgrade that increases crafting efficiency, 5% better than a normal upgrade. As powerful as a specialist upgrade, but more flexible."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Modern Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("ModernUpgrade")]
    public partial class ScholarsModernUpgradeLvl4Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Modern Upgrade 4");

        // base is 0.55
        public ScholarsModernUpgradeLvl4Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.5f) { }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsModernUpgradeLvl1Recipe : RecipeFamily
    {
        public ScholarsModernUpgradeLvl1Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsModernUpgradeLvl1",
                displayName: Localizer.DoStr("Scholars Modern Upgrade 1"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl1Item), 1, true),
                    new IngredientElement("Modern Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsModernUpgradeLvl1Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsModernUpgradeLvl1Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Modern Upgrade 1"),
                recipeType: typeof(ScholarsModernUpgradeLvl1Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsModernUpgradeLvl2Recipe : RecipeFamily
    {
        public ScholarsModernUpgradeLvl2Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsModernUpgradeLvl2",
                displayName: Localizer.DoStr("Scholars Modern Upgrade 2"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl2Item), 1, true),
                    new IngredientElement(typeof(ScholarsModernUpgradeLvl1Item), 1, true),
                    new IngredientElement("Modern Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsModernUpgradeLvl2Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsModernUpgradeLvl2Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Modern Upgrade 2"),
                recipeType: typeof(ScholarsModernUpgradeLvl2Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsModernUpgradeLvl3Recipe : RecipeFamily
    {
        public ScholarsModernUpgradeLvl3Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsModernUpgradeLvl3",
                displayName: Localizer.DoStr("Scholars Modern Upgrade 3"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl3Item), 1, true),
                    new IngredientElement(typeof(ScholarsModernUpgradeLvl2Item), 1, true),
                    new IngredientElement("Modern Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsModernUpgradeLvl3Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsModernUpgradeLvl3Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Modern Upgrade 3"),
                recipeType: typeof(ScholarsModernUpgradeLvl3Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsModernUpgradeLvl4Recipe : RecipeFamily
    {
        public ScholarsModernUpgradeLvl4Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsModernUpgradeLvl4",
                displayName: Localizer.DoStr("Scholars Modern Upgrade 4"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ModernUpgradeLvl4Item), 1, true),
                    new IngredientElement(typeof(ScholarsModernUpgradeLvl3Item), 1, true),
                    new IngredientElement("Modern Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsModernUpgradeLvl4Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsModernUpgradeLvl4Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Modern Upgrade 4"),
                recipeType: typeof(ScholarsModernUpgradeLvl4Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
