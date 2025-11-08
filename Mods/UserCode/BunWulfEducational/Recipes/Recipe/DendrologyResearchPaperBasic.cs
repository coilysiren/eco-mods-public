namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianDendrologyResearchPaperBasicRecipe : DendrologyResearchPaperBasicRecipe
    {
        public LibrarianDendrologyResearchPaperBasicRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Dendrology Research Paper Basic"),
                recipeType: typeof(LibrarianDendrologyResearchPaperBasicRecipe)
            );
        }
    }
}
