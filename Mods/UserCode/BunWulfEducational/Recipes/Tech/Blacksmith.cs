namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianBlacksmithSkillBookRecipe : BlacksmithSkillBookRecipe
    {
        public LibrarianBlacksmithSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Blacksmith Skill Book"),
                recipeType: typeof(LibrarianBlacksmithSkillBookRecipe)
            );
        }
    }
}

