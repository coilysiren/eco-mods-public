namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianTailoringSkillBookRecipe : TailoringSkillBookRecipe
    {
        public LibrarianTailoringSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Tailoring Skill Book"),
                recipeType: typeof(LibrarianTailoringSkillBookRecipe)
            );
        }
    }
}

