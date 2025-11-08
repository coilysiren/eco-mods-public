namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianMasonrySkillBookRecipe : MasonrySkillBookRecipe
    {
        public LibrarianMasonrySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Masonry Skill Book"),
                recipeType: typeof(LibrarianMasonrySkillBookRecipe)
            );
        }
    }
}

