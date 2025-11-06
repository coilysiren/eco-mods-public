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

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    [Ecopedia(
        "Items",
        "Research Papers",
        subPageName: "Librarian Culinary Research Paper Basic Item"
    )]
    public partial class LibrarianCulinaryResearchPaperBasicRecipe : RecipeFamily
    {
        public LibrarianCulinaryResearchPaperBasicRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CulinaryResearchPaperBasic",
                displayName: Localizer.DoStr("Librarian Culinary Research Paper Basic"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("FriedVegetable", 3, typeof(LibrarianSkill)),
                    new IngredientElement("CampfireSalad", 5, typeof(LibrarianSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CulinaryResearchPaperBasicItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Basic"),
                recipeType: typeof(LibrarianCulinaryResearchPaperBasicRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
