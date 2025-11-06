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

    [RequiresSkill(typeof(LibrarianSkill), 5)]
    public partial class LibrarianGeologyResearchPaperModernGlassRecipe : RecipeFamily
    {
        public LibrarianGeologyResearchPaperModernGlassRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GeologyResearchPaperModernGlass",
                displayName: Localizer.DoStr("Librarian Geology Research Paper Modern Glass"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GlassItem), 30, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(InkItem), 4, true),
                    new IngredientElement(typeof(PaperItem), 20, true),
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<GeologyResearchPaperModernItem>(1),
                }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 30;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(LibrarianSkill));
            this.CraftMinutes = CreateCraftTimeValue(1);
            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Librarian Geology Research Paper Modern Glass"),
                recipeType: typeof(LibrarianGeologyResearchPaperModernGlassRecipe)
            );
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
