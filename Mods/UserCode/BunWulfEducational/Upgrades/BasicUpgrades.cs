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
    [LocDisplayName("Scholars Basic Upgrade 1")]
    [LocDescription(
        "A thinking Econian's Basic Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Basic Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("BasicUpgrade")]
    public partial class ScholarsBasicUpgradeLvl1Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Basic Upgrade 1");

        // base is 0.9
        public ScholarsBasicUpgradeLvl1Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.85f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Basic Upgrade 2")]
    [LocDescription(
        "A thinking Econian's Basic Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Basic Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("BasicUpgrade")]
    public partial class ScholarsBasicUpgradeLvl2Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Basic Upgrade 2");

        // base is 0.75
        public ScholarsBasicUpgradeLvl2Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.7f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Basic Upgrade 3")]
    [LocDescription(
        "A thinking Econian's Basic Upgrade that increases crafting efficiency, 5% better than a normal upgrade."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Basic Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("BasicUpgrade")]
    public partial class ScholarsBasicUpgradeLvl3Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Basic Upgrade 3");

        // base is 0.6
        public ScholarsBasicUpgradeLvl3Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.55f) { }
    }

    [Serialized]
    [LocDisplayName("Scholars Basic Upgrade 4")]
    [LocDescription(
        "A thinking Econian's Basic Upgrade that increases crafting efficiency, 5% better than a normal upgrade. As powerful as a specialist upgrade, but more flexible."
    )]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Basic Upgrades", createAsSubPage: true)]
    [Tag("Upgrade")]
    [Tag("BasicUpgrade")]
    public partial class ScholarsBasicUpgradeLvl4Item : EfficiencyModule
    {
        public override LocString DisplayNamePlural => Localizer.DoStr("Scholars Basic Upgrade 4");

        // base is 0.55
        public ScholarsBasicUpgradeLvl4Item()
            : base(ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency, 0.5f) { }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsBasicUpgradeLvl1Recipe : RecipeFamily
    {
        public ScholarsBasicUpgradeLvl1Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsBasicUpgradeLvl1",
                displayName: Localizer.DoStr("Scholars Basic Upgrade 1"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BasicUpgradeLvl1Item), 1, true),
                    new IngredientElement("Basic Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsBasicUpgradeLvl1Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsBasicUpgradeLvl1Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Basic Upgrade 1"),
                recipeType: typeof(ScholarsBasicUpgradeLvl1Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsBasicUpgradeLvl2Recipe : RecipeFamily
    {
        public ScholarsBasicUpgradeLvl2Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsBasicUpgradeLvl2",
                displayName: Localizer.DoStr("Scholars Basic Upgrade 2"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BasicUpgradeLvl2Item), 1, true),
                    new IngredientElement(typeof(ScholarsBasicUpgradeLvl1Item), 1, true),
                    new IngredientElement("Basic Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsBasicUpgradeLvl2Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsBasicUpgradeLvl2Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Basic Upgrade 2"),
                recipeType: typeof(ScholarsBasicUpgradeLvl2Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsBasicUpgradeLvl3Recipe : RecipeFamily
    {
        public ScholarsBasicUpgradeLvl3Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsBasicUpgradeLvl3",
                displayName: Localizer.DoStr("Scholars Basic Upgrade 3"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BasicUpgradeLvl3Item), 1, true),
                    new IngredientElement(typeof(ScholarsBasicUpgradeLvl2Item), 1, true),
                    new IngredientElement("Basic Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsBasicUpgradeLvl3Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsBasicUpgradeLvl3Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Basic Upgrade 3"),
                recipeType: typeof(ScholarsBasicUpgradeLvl3Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }
    }

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class ScholarsBasicUpgradeLvl4Recipe : RecipeFamily
    {
        public ScholarsBasicUpgradeLvl4Recipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ScholarsBasicUpgradeLvl4",
                displayName: Localizer.DoStr("Scholars Basic Upgrade 4"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BasicUpgradeLvl4Item), 1, true),
                    new IngredientElement(typeof(ScholarsBasicUpgradeLvl3Item), 1, true),
                    new IngredientElement("Basic Research", 1, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<ScholarsBasicUpgradeLvl4Item>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(60, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ScholarsBasicUpgradeLvl4Recipe),
                start: 4,
                skillType: typeof(LibrarianSkill)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Scholars Basic Upgrade 4"),
                recipeType: typeof(ScholarsBasicUpgradeLvl4Recipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }
    }
}
