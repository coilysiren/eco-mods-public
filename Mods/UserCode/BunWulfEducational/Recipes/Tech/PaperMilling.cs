namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianPaperMillingSkillBookRecipe : PaperMillingSkillBookRecipe
    {
        public LibrarianPaperMillingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Paper Milling Skill Book"),
                recipeType: typeof(LibrarianPaperMillingSkillBookRecipe)
            );
        }
    }
}

