namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianBasicEngineeringSkillBookRecipe : BasicEngineeringSkillBookRecipe
    {
        public LibrarianBasicEngineeringSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Basic Engineering Skill Book"),
                recipeType: typeof(LibrarianBasicEngineeringSkillBookRecipe)
            );
        }
    }
}

