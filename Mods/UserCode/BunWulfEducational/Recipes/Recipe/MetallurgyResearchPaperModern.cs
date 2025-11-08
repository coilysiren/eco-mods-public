namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianMetallurgyResearchPaperModernRecipe : MetallurgyResearchPaperModernRecipe
    {
        public LibrarianMetallurgyResearchPaperModernRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Metallurgy Research Paper Modern"),
                recipeType: typeof(LibrarianMetallurgyResearchPaperModernRecipe)
            );
        }
    }
}
