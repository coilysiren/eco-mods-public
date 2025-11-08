namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianCulinaryResearchPaperModernRecipe : CulinaryResearchPaperModernRecipe
    {
        public LibrarianCulinaryResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Modern"),
                recipeType: typeof(LibrarianCulinaryResearchPaperModernRecipe)
            );
        }
    }
}
