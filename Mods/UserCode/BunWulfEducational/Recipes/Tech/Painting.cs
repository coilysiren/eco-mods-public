namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianPaintingSkillBookRecipe : PaintingSkillBookRecipe
    {
        public LibrarianPaintingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Painting Skill Book"),
                recipeType: typeof(LibrarianPaintingSkillBookRecipe)
            );
        }
    }
}

