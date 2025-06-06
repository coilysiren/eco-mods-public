// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from TechTemplate.tt/>

namespace BunWulfEducational
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Mods.TechTree;

    /// <summary>Auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization.</summary>




    /// <summary>
    /// <para>Server side recipe definition for "CuttingEdgeCooking".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(LibrarianSkill), 6)]
    [Ecopedia("Professions", "Chef", subPageName: "Librarian Cutting Edge Cooking Skill Book Item")]
    public partial class LibrarianCuttingEdgeCookingSkillBookRecipe : RecipeFamily
    {
        public LibrarianCuttingEdgeCookingSkillBookRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CuttingEdgeCooking",  //noloc
                displayName: Localizer.DoStr("Librarian Cutting Edge Cooking Skill Book"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CulinaryResearchPaperAdvancedItem), 20, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(CulinaryResearchPaperModernItem), 20, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 10, typeof(LibrarianSkill)),
                    new IngredientElement(typeof(AgricultureResearchPaperModernItem), 10, typeof(LibrarianSkill)),
                    new IngredientElement("Basic Research", 30, typeof(LibrarianSkill)), //noloc
                    new IngredientElement("Advanced Research", 10, typeof(LibrarianSkill)), //noloc
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<CuttingEdgeCookingSkillBook>()
                });
            this.Recipes = new List<Recipe> { recipe };
            
            // Defines the amount of labor required and the required skill to add labor
            this.ExperienceOnCraft = 250;
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, typeof(LibrarianSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(LibrarianCuttingEdgeCookingSkillBookRecipe), start: 60, skillType: typeof(LibrarianSkill));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Librarian Cutting Edge Cooking Skill Book"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Librarian Cutting Edge Cooking Skill Book"), recipeType: typeof(LibrarianCuttingEdgeCookingSkillBookRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(LaboratoryObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
