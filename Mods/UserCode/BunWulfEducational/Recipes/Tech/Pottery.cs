namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianPotterySkillBookRecipe : PotterySkillBookRecipe
    {
        public LibrarianPotterySkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Pottery Skill Book"),
                recipeType: typeof(LibrarianPotterySkillBookRecipe)
            );
        }
    }
}

