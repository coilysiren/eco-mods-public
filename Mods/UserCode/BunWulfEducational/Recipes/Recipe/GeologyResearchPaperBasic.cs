namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianGeologyResearchPaperBasicRecipe : GeologyResearchPaperBasicRecipe
    {
        public LibrarianGeologyResearchPaperBasicRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Geology Research Paper Basic"),
                recipeType: typeof(LibrarianGeologyResearchPaperBasicRecipe)
            );
        }
    }
}
