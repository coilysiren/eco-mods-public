namespace BunWulfEducational
{
    using System;
    using System.Collections.Generic;
    using Eco.Core.Controller;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Settlements.ClaimStakes;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(LibrarianSkill), 1)]
    public partial class LibrarianCulinaryResearchPaperBasicMeatRecipe : RecipeFamily
    {
        public LibrarianCulinaryResearchPaperBasicMeatRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CulinaryResearchPaperBasicMeat",
                displayName: Localizer.DoStr("Librarian Culinary Research Paper Basic Meat"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(DriedMeatItem), 20, typeof(LibrarianSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<CulinaryResearchPaperBasicItem>(1),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Culinary Research Paper Basic Meat"),
                recipeType: typeof(LibrarianCulinaryResearchPaperBasicMeatRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
