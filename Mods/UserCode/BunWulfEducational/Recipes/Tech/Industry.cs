namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianIndustrySkillBookRecipe : IndustrySkillBookRecipe
    {
        public LibrarianIndustrySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Industry Skill Book"),
                recipeType: typeof(LibrarianIndustrySkillBookRecipe)
            );
        }
    }
}

