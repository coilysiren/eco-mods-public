namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianGeologyResearchPaperAdvancedRecipe : GeologyResearchPaperAdvancedRecipe
    {
        public LibrarianGeologyResearchPaperAdvancedRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Geology Research Paper Advanced"),
                recipeType: typeof(LibrarianGeologyResearchPaperAdvancedRecipe)
            );
        }
    }
}
