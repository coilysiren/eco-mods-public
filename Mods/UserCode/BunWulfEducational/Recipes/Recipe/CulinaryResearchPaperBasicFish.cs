namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianCulinaryResearchPaperBasicFishRecipe : CulinaryResearchPaperBasicFishRecipe
    {
        public LibrarianCulinaryResearchPaperBasicFishRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Basic Fish"),
                recipeType: typeof(LibrarianCulinaryResearchPaperBasicFishRecipe)
            );
        }
    }
}
