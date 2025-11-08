namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianBakingSkillBookRecipe : BakingSkillBookRecipe
    {
        public LibrarianBakingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Baking Skill Book"),
                recipeType: typeof(LibrarianBakingSkillBookRecipe)
            );
        }
    }
}

