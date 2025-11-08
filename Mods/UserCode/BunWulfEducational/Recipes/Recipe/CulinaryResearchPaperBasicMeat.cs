namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianCulinaryResearchPaperBasicMeatRecipe : CulinaryResearchPaperBasicMeatRecipe
    {
        public LibrarianCulinaryResearchPaperBasicMeatRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Basic Meat"),
                recipeType: typeof(LibrarianCulinaryResearchPaperBasicMeatRecipe)
            );
        }
    }
}
