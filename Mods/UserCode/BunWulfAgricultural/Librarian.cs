#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Librarian")]
    [LocDescription("TODO")]
    [Ecopedia("Professions", "Survivalist", createAsSubPage: true)]
    [RequiresSkill(typeof(SurvivalistSkill), 0), Tag("Survivalist Specialty"), Tier(3)]
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
    [Ecopedia("Professions", "Survivalist", subPageName: "Librarian Skill Book Item")]
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
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipe: this);
        }
    }
}