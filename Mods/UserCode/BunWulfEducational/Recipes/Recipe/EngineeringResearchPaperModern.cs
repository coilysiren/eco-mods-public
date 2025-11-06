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
        subPageName: "Librarian Engineering Research Paper Modern Item"
    )]
    public partial class LibrarianEngineeringResearchPaperModernRecipe : RecipeFamily
    {
        public LibrarianEngineeringResearchPaperModernRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "EngineeringResearchPaperModern",
                displayName: Localizer.DoStr("Librarian Engineering Research Paper Modern"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronAxleItem), 8, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(IronPlateItem), 8, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(IronGearItem), 20, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(InkItem), 4, true),
                    new IngredientElement(typeof(PaperItem), 20, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<EngineeringResearchPaperModernItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 30;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Engineering Research Paper Modern"),
                recipeType: typeof(LibrarianEngineeringResearchPaperModernRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
