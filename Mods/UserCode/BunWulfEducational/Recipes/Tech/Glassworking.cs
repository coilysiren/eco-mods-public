namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianGlassworkingSkillBookRecipe : GlassworkingSkillBookRecipe
    {
        public LibrarianGlassworkingSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Glassworking Skill Book"),
                recipeType: typeof(LibrarianGlassworkingSkillBookRecipe)
            );
        }
    }
}

