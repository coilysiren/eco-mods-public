namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianFertilizersSkillBookRecipe : FertilizersSkillBookRecipe
    {
        public LibrarianFertilizersSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Fertilizers Skill Book"),
                recipeType: typeof(LibrarianFertilizersSkillBookRecipe)
            );
        }
    }
}

