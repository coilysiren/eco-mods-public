namespace BunWulfEducational
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Pipes;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(LibrarianSkill), 3)]
    [Ecopedia(
        "Items",
        "Research Papers",
        subPageName: "Librarian Dendrology Research Paper Advanced Item"
    )]
    public partial class LibrarianDendrologyResearchPaperAdvancedRecipe : RecipeFamily
    {
        public LibrarianDendrologyResearchPaperAdvancedRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "DendrologyResearchPaperAdvanced",
                displayName: Localizer.DoStr("Librarian Dendrology Research Paper Advanced"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("HewnLog", 20, typeof(LibrarianSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<DendrologyResearchPaperAdvancedItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Dendrology Research Paper Advanced"),
                recipeType: typeof(LibrarianDendrologyResearchPaperAdvancedRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
