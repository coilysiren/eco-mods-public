namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianCuttingEdgeCookingSkillBookRecipe : CuttingEdgeCookingSkillBookRecipe
    {
        public LibrarianCuttingEdgeCookingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Cutting Edge Cooking Skill Book"),
                recipeType: typeof(LibrarianCuttingEdgeCookingSkillBookRecipe)
            );
        }
    }
}

