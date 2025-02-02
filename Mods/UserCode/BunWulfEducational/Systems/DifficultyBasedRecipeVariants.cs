// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace BunWulfEducational
{
    using Eco.Core.Plugins.Interfaces;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Players;
    using Eco.Mods.TechTree;

    /// <summary> Registers recipe variants for different difficulty settings. </summary>
    public class DifficultyBasedRecipeVariants : IModInit
    {
        public static void PostInitialize()
        {
            // Normal recipe for lower collaboration settings. Uses defaults found in Tech Tree
			// Endgame Goal world object recipes
            RecipeVariant.RegisterDefault<ComputerLabRecipe>(DifficultySettingsConfig.EndgameRecipesNormal);
            RecipeVariant.RegisterDefault<LaserRecipe>(DifficultySettingsConfig.EndgameRecipesNormal);
            // Techtree skillbook recipes
            RecipeVariant.RegisterDefault<LibrarianAdvancedBakingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianAdvancedCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianAdvancedMasonrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianAdvancedSmeltingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianBakingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianBasicEngineeringSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianBlacksmithSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianButcherySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianCarpentrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianCompositesSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianCuttingEdgeCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianElectronicsSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianFarmingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianFertilizersSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianGlassworkingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianIndustrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianMasonrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianMechanicsSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianMillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianOilDrillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianPaperMillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
			RecipeVariant.RegisterDefault<LibrarianPaintingSkillBookRecipe>(DifficultySettingsConfig.EndgameRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianPotterySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianSmeltingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);
            RecipeVariant.RegisterDefault<LibrarianTailoringSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesNormal);



            // Expensive recipes for higher collaboration settings. All costs are static
            RecipeVariant.Register<ComputerLabRecipe>(DifficultySettingsConfig.EndgameRecipesExpensive, new[]
            {
                new IngredientElement(typeof(AdvancedMasonryUpgradeItem), 1, true),
                new IngredientElement(typeof(CompositesUpgradeItem), 1, true),
                new IngredientElement(typeof(ElectronicsUpgradeItem), 1, true),
                new IngredientElement(typeof(IndustryUpgradeItem), 1, true),
                new IngredientElement(typeof(OilDrillingUpgradeItem), 1, true),
                new IngredientElement(typeof(AdvancedSmeltingUpgradeItem), 1, true),
                new IngredientElement(typeof(AdvancedCircuitItem), 100, true),
                new IngredientElement(typeof(PlasticItem), 100, true),
                new IngredientElement(typeof(ReinforcedConcreteItem), 200, true),
                 new IngredientElement("CompositeLumber", 200, true)

            });
            RecipeVariant.Register<LaserRecipe>(DifficultySettingsConfig.EndgameRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GoldBarItem), 80, true),
                new IngredientElement(typeof(SteelBarItem), 80, true),
                new IngredientElement(typeof(FramedGlassItem), 80, true),
                new IngredientElement(typeof(AdvancedCircuitItem), 40, true),
                new IngredientElement(typeof(ElectricMotorItem), 2, true),
                new IngredientElement(typeof(RadiatorItem), 10, true)
            });
			// Expensive skill book recipes for higher collaboration settings. All costs are static
			RecipeVariant.Register<LibrarianAdvancedBakingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperAdvancedItem), 30, true),
                new IngredientElement(typeof(DendrologyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(GeologyResearchPaperModernItem), 15, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 20, true),
                new IngredientElement("Modern Research", 20, true),
            });
			RecipeVariant.Register<LibrarianAdvancedCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperAdvancedItem), 30, true),
                new IngredientElement(typeof(DendrologyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(GeologyResearchPaperModernItem), 15, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 15, true),
                new IngredientElement("Modern Research", 15, true),
            });
            RecipeVariant.Register<LibrarianAdvancedMasonrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 30, true),
                new IngredientElement(typeof(GeologyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(EngineeringResearchPaperModernItem), 15, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 15, true),
            });
			RecipeVariant.Register<LibrarianAdvancedSmeltingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(MetallurgyResearchPaperBasicItem), 30, true),
                new IngredientElement(typeof(MetallurgyResearchPaperAdvancedItem), 30, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianBakingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperBasicItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperBasicItem), 10, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianBasicEngineeringSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperAdvancedItem), 6, true),
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 6, true),
                new IngredientElement("Basic Research", 10, true),
            });
            RecipeVariant.Register<LibrarianBlacksmithSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
{
                new IngredientElement(typeof(MetallurgyResearchPaperBasicItem), 15, true),
                new IngredientElement(typeof(DendrologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement("Basic Research", 10, true),
            });
            RecipeVariant.Register<LibrarianButcherySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperBasicItem), 6, true),
            });
			RecipeVariant.Register<LibrarianCarpentrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperBasicItem), 6, true),
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 6, true),
            });
			RecipeVariant.Register<LibrarianCompositesSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperAdvancedItem), 30, true),
                new IngredientElement(typeof(DendrologyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(EngineeringResearchPaperModernItem), 15, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 15, true),
            });
			RecipeVariant.Register<LibrarianCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperBasicItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperBasicItem), 10, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianCuttingEdgeCookingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(CulinaryResearchPaperAdvancedItem), 30, true),
                new IngredientElement(typeof(CulinaryResearchPaperModernItem), 30, true),
                new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(AgricultureResearchPaperModernItem), 15, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 15, true),
            });
			RecipeVariant.Register<LibrarianElectronicsSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(MetallurgyResearchPaperAdvancedItem), 15, true),
                new IngredientElement(typeof(EngineeringResearchPaperModernItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 30, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 30, true),
                new IngredientElement("Modern Research", 30, true),
            });
			RecipeVariant.Register<LibrarianFarmingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 4, true),
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 2, true),
            });
			RecipeVariant.Register<LibrarianFertilizersSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(AgricultureResearchPaperAdvancedItem), 5, true),
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 5, true),
                new IngredientElement("Basic Research", 10, true),
            });
			RecipeVariant.Register<LibrarianGlassworkingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 20, true),
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(EngineeringResearchPaperAdvancedItem), 10, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianIndustrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(EngineeringResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(EngineeringResearchPaperModernItem), 20, true),
                new IngredientElement(typeof(MetallurgyResearchPaperModernItem), 20, true),
                new IngredientElement("Basic Research", 30, true),
                new IngredientElement("Advanced Research", 20, true),
                new IngredientElement("Modern Research", 10, true),
            });
			RecipeVariant.Register<LibrarianMasonrySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 6, true),
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 6, true),
            });
			RecipeVariant.Register<LibrarianMechanicsSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(EngineeringResearchPaperAdvancedItem), 15, true),
                new IngredientElement(typeof(MetallurgyResearchPaperAdvancedItem), 15, true),
                new IngredientElement("Basic Research", 30, true),
                new IngredientElement("Advanced Research", 10, true),
            });
			RecipeVariant.Register<LibrarianMillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperBasicItem), 10, true),
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 10, true),
                new IngredientElement(typeof(CulinaryResearchPaperBasicItem), 10, true),
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 5, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianOilDrillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(AgricultureResearchPaperAdvancedItem), 20, true),
                new IngredientElement(typeof(GeologyResearchPaperModernItem), 20, true),
                new IngredientElement(typeof(DendrologyResearchPaperModernItem), 20, true),
                new IngredientElement(typeof(EngineeringResearchPaperModernItem), 20, true),
                new IngredientElement("Basic Research", 45, true),
                new IngredientElement("Advanced Research", 30, true),
            });
			RecipeVariant.Register<LibrarianPaperMillingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 10, true),
                new IngredientElement("Basic Research", 10, true),
            });
			RecipeVariant.Register<LibrarianPaintingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
            new IngredientElement(typeof(EngineeringResearchPaperAdvancedItem), 15, typeof(BasicEngineeringSkill)),
            new IngredientElement(typeof(GatheringResearchPaperBasicItem), 15, typeof(BasicEngineeringSkill)),
            new IngredientElement("Basic Research", 30, typeof(BasicEngineeringSkill)), //noloc
            });
			RecipeVariant.Register<LibrarianPotterySkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GeologyResearchPaperBasicItem), 15, true),
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(EngineeringResearchPaperAdvancedItem), 10, true),
                new IngredientElement("Basic Research", 15, true),
            });
			RecipeVariant.Register<LibrarianSmeltingSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(MetallurgyResearchPaperBasicItem), 15, true),
                new IngredientElement(typeof(DendrologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement(typeof(GeologyResearchPaperAdvancedItem), 10, true),
                new IngredientElement("Basic Research", 10, true),
            });
            RecipeVariant.Register<LibrarianShipwrightSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(DendrologyResearchPaperBasicItem), 10, true),
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 10, true),
                new IngredientElement("Basic Research", 15, true), //noloc
            });
            RecipeVariant.Register<LibrarianTailoringSkillBookRecipe>(DifficultySettingsConfig.SkillbookRecipesExpensive, new[]
            {
                new IngredientElement(typeof(GatheringResearchPaperBasicItem), 10, true),
                new IngredientElement("Basic Research", 10, true),
            });
        }
    }
}
