namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianDendrologyResearchPaperModernRecipe : DendrologyResearchPaperModernRecipe
    {
        public LibrarianDendrologyResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Dendrology Research Paper Modern"),
                recipeType: typeof(LibrarianDendrologyResearchPaperModernRecipe)
            );
        }
    }
}
