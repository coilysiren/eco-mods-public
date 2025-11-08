namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianAdvancedCookingSkillBookRecipe : AdvancedCookingSkillBookRecipe
    {
        public LibrarianAdvancedCookingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Advanced Cooking Skill Book"),
                recipeType: typeof(LibrarianAdvancedCookingSkillBookRecipe)
            );
        }
    }
}

