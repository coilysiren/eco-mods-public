namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianAdvancedBakingSkillBookRecipe : AdvancedBakingSkillBookRecipe
    {
        public LibrarianAdvancedBakingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Advanced Baking Skill Book"),
                recipeType: typeof(LibrarianAdvancedBakingSkillBookRecipe)
            );
        }
    }
}

