namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianMechanicsSkillBookRecipe : MechanicsSkillBookRecipe
    {
        public LibrarianMechanicsSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Mechanics Skill Book"),
                recipeType: typeof(LibrarianMechanicsSkillBookRecipe)
            );
        }
    }
}

