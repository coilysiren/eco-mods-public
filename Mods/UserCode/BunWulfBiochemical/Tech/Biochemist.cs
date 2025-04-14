namespace BunWulfBioChemical
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Biochemist")]
    [LocDescription(
        "The Biochemist gets recipes to make Biofuel, Plastic, Rubber, Epoxy, and Nylon. The recipes have similar costs to Oil Drilling, but the Biochemist is more sustainable and has a lower impact on the environment. While a biochemist's chemlab performs many of the same functions as an oil refinery, it does them very slowly. Plan to run at least 8 of them."
    )]
    [Ecopedia("Professions", "Scientist", createAsSubPage: true)]
    [RequiresSkill(typeof(ScientistSkill), 0), Tag("Scientist Specialty"), Tier(4)]
    [Tag("Specialty")]
    [Tag("Teachable")]
    public partial class BiochemistSkill : Skill
    {
        public override void OnLevelUp(User user)
        {
            _ = user.Skillset.AddExperience(
                typeof(SelfImprovementSkill),
                20,
                Localizer.DoStr("for leveling up another specialization.")
            );
        }

        public static MultiplicativeStrategy MultiplicativeStrategy =
            new(
                new float[]
                {
                    1,
                    1 - 0.2f,
                    1 - 0.25f,
                    1 - 0.3f,
                    1 - 0.35f,
                    1 - 0.4f,
                    1 - 0.45f,
                    1 - 0.5f,
                }
            );
        public override MultiplicativeStrategy MultiStrategy => MultiplicativeStrategy;

        public static AdditiveStrategy AdditiveStrategy =
            new(new float[] { 0, 0.5f, 0.55f, 0.6f, 0.65f, 0.7f, 0.75f, 0.8f });
        public override AdditiveStrategy AddStrategy => AdditiveStrategy;
        public override int MaxLevel => 7;
        public override int Tier => 4;
    }

    [Serialized]
    [Weight(1000)]
    [LocDisplayName("Biochemist Skill Book")]
    [Ecopedia("Items", "Skill Books", createAsSubPage: true)]
    public partial class BiochemistSkillBook : SkillBook<BiochemistSkill, BiochemistSkillScroll> { }

    [Serialized]
    [Weight(100)]
    [LocDisplayName("Biochemist Skill Scroll")]
    public partial class BiochemistSkillScroll
        : SkillScroll<BiochemistSkill, BiochemistSkillBook> { }

    [RequiresSkill(typeof(FertilizersSkill), 1)]
    [Ecopedia("Professions", "Scientist", subPageName: "Biochemist Skill Book Item")]
    public partial class BiochemistSkillBookRecipe : RecipeFamily
    {
        public BiochemistSkillBookRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Biochemist",
                displayName: Localizer.DoStr("Biochemist Skill Book"),
                ingredients: new List<IngredientElement>
                {
                    new(typeof(CulinaryResearchPaperAdvancedItem), 10, typeof(FertilizersSkill)),
                    new(typeof(AgricultureResearchPaperAdvancedItem), 10, typeof(FertilizersSkill)),
                    new(typeof(EngineeringResearchPaperModernItem), 10, typeof(FertilizersSkill)),
                    new(typeof(AgricultureResearchPaperModernItem), 10, typeof(FertilizersSkill)),
                    new("Basic Research", 30, typeof(FertilizersSkill)),
                    new("Advanced Research", 20, typeof(FertilizersSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<BiochemistSkillBook>() }
            );
            Recipes = new List<Recipe> { recipe };
            LaborInCalories = CreateLaborInCaloriesValue(600, typeof(FertilizersSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiochemistSkillBookRecipe),
                start: 15,
                skillType: typeof(FertilizersSkill)
            );
            Initialize(
                displayText: Localizer.DoStr("Biochemist Skill Book"),
                recipeType: typeof(BiochemistSkillBookRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }
    }
}
