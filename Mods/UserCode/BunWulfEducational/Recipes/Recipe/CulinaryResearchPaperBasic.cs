namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianCulinaryResearchPaperBasicRecipe : CulinaryResearchPaperBasicRecipe
    {
        public LibrarianCulinaryResearchPaperBasicRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Basic"),
                recipeType: typeof(LibrarianCulinaryResearchPaperBasicRecipe)
            );
        }
    }
}
