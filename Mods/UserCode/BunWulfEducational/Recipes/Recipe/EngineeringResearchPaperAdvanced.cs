namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianEngineeringResearchPaperAdvancedRecipe : EngineeringResearchPaperAdvancedRecipe
    {
        public LibrarianEngineeringResearchPaperAdvancedRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Engineering Research Paper Advanced"),
                recipeType: typeof(LibrarianEngineeringResearchPaperAdvancedRecipe)
            );
        }
    }
}
