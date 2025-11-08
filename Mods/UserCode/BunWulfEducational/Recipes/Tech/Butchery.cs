namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianButcherySkillBookRecipe : ButcherySkillBookRecipe
    {
        public LibrarianButcherySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Butchery Skill Book"),
                recipeType: typeof(LibrarianButcherySkillBookRecipe)
            );
        }
    }
}

