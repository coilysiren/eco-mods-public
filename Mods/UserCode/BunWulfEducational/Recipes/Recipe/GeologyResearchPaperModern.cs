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

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    [Ecopedia(
        "Items",
        "Research Papers",
        subPageName: "Librarian Geology Research Paper Modern Item"
    )]
    public partial class LibrarianGeologyResearchPaperModernRecipe : RecipeFamily
    {
        public LibrarianGeologyResearchPaperModernRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GeologyResearchPaperModern",
                displayName: Localizer.DoStr("Librarian Geology Research Paper Modern"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(BrickItem), 30, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(InkItem), 4, true),
                    new IngredientElement(typeof(PaperItem), 20, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GeologyResearchPaperModernItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 30;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Geology Research Paper Modern"),
                recipeType: typeof(LibrarianGeologyResearchPaperModernRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
