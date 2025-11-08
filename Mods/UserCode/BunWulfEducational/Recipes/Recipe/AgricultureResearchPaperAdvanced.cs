namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianAgricultureResearchPaperAdvancedRecipe : AgricultureResearchPaperAdvancedRecipe
    {
        public LibrarianAgricultureResearchPaperAdvancedRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Agriculture Research Paper Advanced"),
                recipeType: typeof(LibrarianAgricultureResearchPaperAdvancedRecipe)
            );
        }
    }
}
