namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianCarpentrySkillBookRecipe : CarpentrySkillBookRecipe
    {
        public LibrarianCarpentrySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Carpentry Skill Book"),
                recipeType: typeof(LibrarianCarpentrySkillBookRecipe)
            );
        }
    }
}

