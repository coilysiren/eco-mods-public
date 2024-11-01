using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Utils;
using Eco.Core.Utils.AtomicAction;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Services;
using Eco.Shared.Utils;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Biochemist")]
    [LocDescription("Biochemist TODO.")]
    [Ecopedia("Professions", "Scientist", createAsSubPage: true)]
    [RequiresSkill(typeof(ScientistSkill), 0), Tag("Scientist Specialty"), Tier(4)]
    [Tag("Specialty")]
    [Tag("Teachable")]
    public partial class BiochemistSkill : Skill
    {
        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(
                typeof(SelfImprovementSkill),
                20,
                Localizer.DoStr("for leveling up another specialization.")
            );
        }

        public static MultiplicativeStrategy MultiplicativeStrategy = new MultiplicativeStrategy(
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

        public static AdditiveStrategy AdditiveStrategy = new AdditiveStrategy(
            new float[] { 0, 0.5f, 0.55f, 0.6f, 0.65f, 0.7f, 0.75f, 0.8f }
        );
        public override AdditiveStrategy AddStrategy => AdditiveStrategy;
        public override int MaxLevel
        {
            get { return 7; }
        }
        public override int Tier
        {
            get { return 4; }
        }
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

    [RequiresSkill(typeof(FarmingSkill), 1)]
    [Ecopedia("Professions", "Scientist", subPageName: "Biochemist Skill Book Item")]
    public partial class BiochemistSkillBookRecipe : RecipeFamily
    {
        public BiochemistSkillBookRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Biochemist",
                displayName: Localizer.DoStr("Biochemist Skill Book"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(
                        typeof(CulinaryResearchPaperAdvancedItem),
                        10,
                        typeof(FarmingSkill)
                    ),
                    new IngredientElement(
                        typeof(AgricultureResearchPaperAdvancedItem),
                        10,
                        typeof(FarmingSkill)
                    ),
                    new IngredientElement(
                        typeof(EngineeringResearchPaperModernItem),
                        10,
                        typeof(FarmingSkill)
                    ),
                    new IngredientElement(
                        typeof(AgricultureResearchPaperModernItem),
                        10,
                        typeof(FarmingSkill)
                    ),
                    new IngredientElement("Basic Research", 30, typeof(FarmingSkill)),
                    new IngredientElement("Advanced Research", 20, typeof(FarmingSkill)),
                },
                items: new List<CraftingElement> { new CraftingElement<BiochemistSkillBook>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(FarmingSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(BiochemistSkillBookRecipe),
                start: 15,
                skillType: typeof(FarmingSkill)
            );
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Biochemist Skill Book"),
                recipeType: typeof(BiochemistSkillBookRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipe: this);
        }
    }
}
