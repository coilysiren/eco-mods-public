namespace BunWulfEducational
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
    [LocDisplayName("Librarian")]
    [LocDescription(
        "The librarian is an expert in the learning and study of skills. Basic knowledge of the librarian practice allows a person to do all kinds of basic research. Specializing as a librarian unlocks the ability to become a fountain of knowledge, granting the ability to create every skill book and every research paper. Additionally the librarian can create the ink and paper required for modern research."
    )]
    [Ecopedia("Professions", "Scientist", createAsSubPage: true)]
    [RequiresSkill(typeof(ScientistSkill), 0), Tag("Scientist Specialty"), Tier(1)]
    [Tag("Specialty")]
    [Tag("Teachable")]
    public partial class LibrarianSkill : Skill
    {
        public override void OnLevelUp(User user)
        {
            OnLevelChanged(user);
        }

        private void OnLevelChanged(User user)
        {
            user.Stomach.ChangedMaxCalories();
            user.ChangedCarryWeight();
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
        public override int Tier => 3;
    }

    [Serialized]
    [Weight(1000)]
    [LocDisplayName("Librarian Skill Book")]
    [Ecopedia("Items", "Skill Books", createAsSubPage: true)]
    public partial class LibrarianSkillBook : SkillBook<LibrarianSkill, LibrarianSkillScroll> { }

    [Serialized]
    [Weight(100)]
    [LocDisplayName("Librarian Skill Scroll")]
    public partial class LibrarianSkillScroll : SkillScroll<LibrarianSkill, LibrarianSkillBook> { }

    [RequiresSkill(typeof(SurvivalistSkill), 1)]
    [Ecopedia("Professions", "Scientist", subPageName: "Librarian Skill Book Item")]
    public partial class LibrarianSkillBookRecipe : RecipeFamily
    {
        public LibrarianSkillBookRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Librarian",
                displayName: Localizer.DoStr("Librarian Skill Book"),
                ingredients: new List<IngredientElement>
                {
                    new("Basic Research", 20, typeof(SurvivalistSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<LibrarianSkillBook>() }
            );
            Recipes = new List<Recipe> { recipe };
            LaborInCalories = CreateLaborInCaloriesValue(600, typeof(SurvivalistSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(LibrarianSkillBookRecipe),
                start: 15,
                skillType: typeof(SurvivalistSkill)
            );
            Initialize(
                displayText: Localizer.DoStr("Librarian Skill Book"),
                recipeType: typeof(LibrarianSkillBookRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }
    }
}
