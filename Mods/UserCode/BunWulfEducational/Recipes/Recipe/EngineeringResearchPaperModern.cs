namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianEngineeringResearchPaperModernRecipe : EngineeringResearchPaperModernRecipe
    {
        public LibrarianEngineeringResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Engineering Research Paper Modern"),
                recipeType: typeof(LibrarianEngineeringResearchPaperModernRecipe)
            );
        }
    }
}
