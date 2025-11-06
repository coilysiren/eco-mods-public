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
        subPageName: "Librarian Metallurgy Research Paper Basic Item"
    )]
    public partial class LibrarianMetallurgyResearchPaperBasicRecipe : RecipeFamily
    {
        public LibrarianMetallurgyResearchPaperBasicRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "MetallurgyResearchPaperBasic",
                displayName: Localizer.DoStr("Librarian Metallurgy Research Paper Basic"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Ore", 10, typeof(LibrarianSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<MetallurgyResearchPaperBasicItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 10;
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Metallurgy Research Paper Basic"),
                recipeType: typeof(LibrarianMetallurgyResearchPaperBasicRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
