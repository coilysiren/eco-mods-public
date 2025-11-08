namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianMetallurgyResearchPaperBasicRecipe : MetallurgyResearchPaperBasicRecipe
    {
        public LibrarianMetallurgyResearchPaperBasicRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Metallurgy Research Paper Basic"),
                recipeType: typeof(LibrarianMetallurgyResearchPaperBasicRecipe)
            );
        }
    }
}
