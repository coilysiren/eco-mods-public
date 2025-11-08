namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianFarmingSkillBookRecipe : FarmingSkillBookRecipe
    {
        public LibrarianFarmingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Farming Skill Book"),
                recipeType: typeof(LibrarianFarmingSkillBookRecipe)
            );
        }
    }
}
