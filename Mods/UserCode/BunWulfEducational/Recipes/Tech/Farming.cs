namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianFarmingSkillBookRecipe : FarmingSkillBookRecipe
    {
        public LibrarianFarmingSkillBookRecipe()
            : base()
        {
        }
    }
}
