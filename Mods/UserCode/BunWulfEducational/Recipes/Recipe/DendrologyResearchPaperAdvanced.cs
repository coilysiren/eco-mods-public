namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianDendrologyResearchPaperAdvancedRecipe : DendrologyResearchPaperAdvancedRecipe
    {
        public LibrarianDendrologyResearchPaperAdvancedRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Dendrology Research Paper Advanced"),
                recipeType: typeof(LibrarianDendrologyResearchPaperAdvancedRecipe)
            );
        }
    }
}
