namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    public partial class LibrarianCulinaryResearchPaperAdvancedMeatRecipe : CulinaryResearchPaperAdvancedMeatRecipe
    {
        public LibrarianCulinaryResearchPaperAdvancedMeatRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Advanced Meat"),
                recipeType: typeof(LibrarianCulinaryResearchPaperAdvancedMeatRecipe)
            );
        }
    }
}
