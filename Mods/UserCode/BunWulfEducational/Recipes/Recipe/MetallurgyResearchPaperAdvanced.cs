namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianMetallurgyResearchPaperAdvancedRecipe : MetallurgyResearchPaperAdvancedRecipe
    {
        public LibrarianMetallurgyResearchPaperAdvancedRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Metallurgy Research Paper Advanced"),
                recipeType: typeof(LibrarianMetallurgyResearchPaperAdvancedRecipe)
            );
        }
    }
}
