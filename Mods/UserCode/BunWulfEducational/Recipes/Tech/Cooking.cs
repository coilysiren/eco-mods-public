namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianCookingSkillBookRecipe : CookingSkillBookRecipe
    {
        public LibrarianCookingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Cooking Skill Book"),
                recipeType: typeof(LibrarianCookingSkillBookRecipe)
            );
        }
    }
}

