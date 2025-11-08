namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianGatheringResearchPaperBasicRecipe : GatheringResearchPaperBasicRecipe
    {
        public LibrarianGatheringResearchPaperBasicRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Gathering Research Paper Basic"),
                recipeType: typeof(LibrarianGatheringResearchPaperBasicRecipe)
            );
        }
    }
}
