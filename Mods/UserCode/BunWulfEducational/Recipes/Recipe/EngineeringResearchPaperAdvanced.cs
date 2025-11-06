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
        subPageName: "Librarian Engineering Research Paper Advanced Item"
    )]
    public partial class LibrarianEngineeringResearchPaperAdvancedRecipe : RecipeFamily
    {
        public LibrarianEngineeringResearchPaperAdvancedRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "EngineeringResearchPaperAdvanced",
                displayName: Localizer.DoStr("Librarian Engineering Research Paper Advanced"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WaterwheelItem), 2, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(WindmillItem), 2, typeof(LibrarianSkill)),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<EngineeringResearchPaperAdvancedItem>(),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Engineering Research Paper Advanced"),
                recipeType: typeof(LibrarianEngineeringResearchPaperAdvancedRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
