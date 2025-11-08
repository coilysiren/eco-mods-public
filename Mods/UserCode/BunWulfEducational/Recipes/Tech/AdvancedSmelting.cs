namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianAdvancedSmeltingSkillBookRecipe : AdvancedSmeltingSkillBookRecipe
    {
        public LibrarianAdvancedSmeltingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Advanced Smelting Skill Book"),
                recipeType: typeof(LibrarianAdvancedSmeltingSkillBookRecipe)
            );
        }
    }
}

