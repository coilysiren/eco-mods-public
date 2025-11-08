namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianAdvancedMasonrySkillBookRecipe : AdvancedMasonrySkillBookRecipe
    {
        public LibrarianAdvancedMasonrySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Advanced Masonry Skill Book"),
                recipeType: typeof(LibrarianAdvancedMasonrySkillBookRecipe)
            );
        }
    }
}

