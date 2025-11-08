namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianOilDrillingSkillBookRecipe : OilDrillingSkillBookRecipe
    {
        public LibrarianOilDrillingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Oil Drilling Skill Book"),
                recipeType: typeof(LibrarianOilDrillingSkillBookRecipe)
            );
        }
    }
}

