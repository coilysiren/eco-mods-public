namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianAgricultureResearchPaperModernRecipe : AgricultureResearchPaperModernRecipe
    {
        public LibrarianAgricultureResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Agriculture Research Paper Modern"),
                recipeType: typeof(LibrarianAgricultureResearchPaperModernRecipe)
            );
        }
    }
}
