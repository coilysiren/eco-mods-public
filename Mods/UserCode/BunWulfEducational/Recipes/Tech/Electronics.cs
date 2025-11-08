namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianElectronicsSkillBookRecipe : ElectronicsSkillBookRecipe
    {
        public LibrarianElectronicsSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Electronics Skill Book"),
                recipeType: typeof(LibrarianElectronicsSkillBookRecipe)
            );
        }
    }
}

