namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianMillingSkillBookRecipe : MillingSkillBookRecipe
    {
        public LibrarianMillingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Milling Skill Book"),
                recipeType: typeof(LibrarianMillingSkillBookRecipe)
            );
        }
    }
}

