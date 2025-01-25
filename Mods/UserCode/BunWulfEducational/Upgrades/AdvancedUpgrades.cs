namespace BunWulfEducational
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Scholars Advanced Upgrade 1")]
    [LocDescription(
        "A thinking Econian's Advanced Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Advanced Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("AdvancedUpgrade")]
    public partial class ScholarsAdvancedUpgradeLvl1Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural =>
            Localizer.DoStr("Scholars Advanced Upgrade 1");

        // base is 0.9
        public ScholarsAdvancedUpgradeLvl1Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.85f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Advanced Upgrade 2")]
    [LocDescription(
        "A thinking Econian's Advanced Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Advanced Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("AdvancedUpgrade")]
    public partial class ScholarsAdvancedUpgradeLvl2Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural =>
            Localizer.DoStr("Scholars Advanced Upgrade 2");

        // base is 0.75
        public ScholarsAdvancedUpgradeLvl2Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.7f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Advanced Upgrade 3")]
    [LocDescription(
        "A thinking Econian's Advanced Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Advanced Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("AdvancedUpgrade")]
    public partial class ScholarsAdvancedUpgradeLvl3Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural =>
            Localizer.DoStr("Scholars Advanced Upgrade 3");

        // base is 0.6
        public ScholarsAdvancedUpgradeLvl3Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.55f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Advanced Upgrade 4")]
    [LocDescription(
        "A thinking Econian's Advanced Upgrade that increases crafting efficiency, 5% better than a normal upgrade. As powerful as a specialist upgrade, but more flexible."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Advanced Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("AdvancedUpgrade")]
    public partial class ScholarsAdvancedUpgradeLvl4Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural =>
            Localizer.DoStr("Scholars Advanced Upgrade 4");

        // base is 0.55
        public ScholarsAdvancedUpgradeLvl4Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.5f) { }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsAdvancedUpgradeLvl1Recipe : RecipeFamily
    {
        public ScholarsAdvancedUpgradeLvl1Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsAdvancedUpgradeLvl1",
                displayName: Localizer.DoStr("Scholars Advanced Upgrade 1"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdvancedUpgradeLvl1Item), 1, true),
                    new IngredientElement("Advanced Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsAdvancedUpgradeLvl1Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsAdvancedUpgradeLvl1Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Advanced Upgrade 1"),
                recipeType: typeof(ScholarsAdvancedUpgradeLvl1Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsAdvancedUpgradeLvl2Recipe : RecipeFamily
    {
        public ScholarsAdvancedUpgradeLvl2Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsAdvancedUpgradeLvl2",
                displayName: Localizer.DoStr("Scholars Advanced Upgrade 2"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdvancedUpgradeLvl2Item), 1, true),
                    new IngredientElement(typeof(ScholarsAdvancedUpgradeLvl1Item), 1, true),
                    new IngredientElement("Advanced Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsAdvancedUpgradeLvl2Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsAdvancedUpgradeLvl2Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Advanced Upgrade 2"),
                recipeType: typeof(ScholarsAdvancedUpgradeLvl2Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsAdvancedUpgradeLvl3Recipe : RecipeFamily
    {
        public ScholarsAdvancedUpgradeLvl3Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsAdvancedUpgradeLvl3",
                displayName: Localizer.DoStr("Scholars Advanced Upgrade 3"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdvancedUpgradeLvl3Item), 1, true),
                    new IngredientElement(typeof(ScholarsAdvancedUpgradeLvl2Item), 1, true),
                    new IngredientElement("Advanced Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsAdvancedUpgradeLvl3Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsAdvancedUpgradeLvl3Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Advanced Upgrade 3"),
                recipeType: typeof(ScholarsAdvancedUpgradeLvl3Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsAdvancedUpgradeLvl4Recipe : RecipeFamily
    {
        public ScholarsAdvancedUpgradeLvl4Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsAdvancedUpgradeLvl4",
                displayName: Localizer.DoStr("Scholars Advanced Upgrade 4"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdvancedUpgradeLvl4Item), 1, true),
                    new IngredientElement(typeof(ScholarsAdvancedUpgradeLvl3Item), 1, true),
                    new IngredientElement("Advanced Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsAdvancedUpgradeLvl4Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsAdvancedUpgradeLvl4Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Advanced Upgrade 4"),
                recipeType: typeof(ScholarsAdvancedUpgradeLvl4Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
