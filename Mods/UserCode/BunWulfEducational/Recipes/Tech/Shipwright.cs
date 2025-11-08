namespace BunWulfEducational
{
    using Eco.Mods.TechTree;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(LibrarianSkill), 2)]
    public partial class LibrarianShipwrightSkillBookRecipe : ShipwrightSkillBookRecipe
    {
        public LibrarianShipwrightSkillBookRecipe()
            : base()
        {
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Shipwright Skill Book"),
                recipeType: typeof(LibrarianShipwrightSkillBookRecipe)
            );
        }
    }
}

