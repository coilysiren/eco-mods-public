namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianSmeltingSkillBookRecipe : SmeltingSkillBookRecipe
    {
        public LibrarianSmeltingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Smelting Skill Book"),
                recipeType: typeof(LibrarianSmeltingSkillBookRecipe)
            );
        }
    }
}

