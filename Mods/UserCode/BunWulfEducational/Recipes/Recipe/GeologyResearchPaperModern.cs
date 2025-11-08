namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianGeologyResearchPaperModernRecipe : GeologyResearchPaperModernRecipe
    {
        public LibrarianGeologyResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Geology Research Paper Modern"),
                recipeType: typeof(LibrarianGeologyResearchPaperModernRecipe)
            );
        }
    }
}
