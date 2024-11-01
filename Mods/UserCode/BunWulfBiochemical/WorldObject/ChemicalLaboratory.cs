using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Core.Utils;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Civics.Objects;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Components.Storage;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Occupancy;
using Eco.Gameplay.Pipes;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Settlements;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.World.Blocks;
using static Eco.Gameplay.Components.PartsComponent;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(OccupancyRequirementComponent))]
    [RequireComponent(typeof(LiquidConverterComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(24)]
    [RequireRoomMaterialTier(
        2.8f,
        typeof(CuttingEdgeCookingLavishReqTalent),
        typeof(CuttingEdgeCookingFrugalReqTalent)
    )]
    [Tag("Usable")]
    [Ecopedia("Work Stations", "Researching", subPageName: "Chemical Laboratory Item")]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [RepairRequiresSkill(typeof(SelfImprovementSkill), 5)]
    public partial class ChemicalLaboratoryObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(ChemicalLaboratoryItem);
        public override LocString DisplayName => Localizer.DoStr("Chemical Laboratory");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Crafting"));
            this.GetComponent<LiquidConverterComponent>()
                .Setup(
                    typeof(WaterItem),
                    typeof(SewageItem),
                    BlockOccupancyType.WaterInputPort,
                    BlockOccupancyType.SewageOutputPort,
                    0.3f,
                    0.9f
                );
            this.ModsPostInitialize();
            {
                this.GetComponent<PartsComponent>()
                    .Config(
                        () => LocString.Empty,
                        new PartInfo[]
                        {
                            new() { TypeName = nameof(PaperItem), Quantity = 10 },
                        }
                    );
            }
        }
    }

    [Serialized]
    [LocDisplayName("Chemical Laboratory")]
    [LocDescription("For more advanced research and manufacturing. Science rules!")]
    [IconGroup("World Object Minimap")]
    [Ecopedia("Work Stations", "Researching", createAsSubPage: true)]
    [Weight(2000)] // Defines how heavy Chemical Laboratory is.
    [Tag(nameof(SurfaceTags.CanBeOnRug))]
    [AllowPluginModules(
        Tags = new[] { "ModernUpgrade" },
        ItemTypes = new[] { typeof(CuttingEdgeCookingUpgradeItem) }
    )] //noloc
    public partial class ChemicalLaboratoryItem
        : WorldObjectItem<ChemicalLaboratoryObject>,
            IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(this.WorldObjectType)
            );

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object PersistentData { get; set; }
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]
    [Ecopedia("Work Stations", "Researching", subPageName: "Chemical Laboratory Item")]
    public partial class ChemicalLaboratoryRecipe : RecipeFamily
    {
        public ChemicalLaboratoryRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Chemical Laboratory", //noloc
                displayName: Localizer.DoStr("Chemical Laboratory"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(
                        typeof(IronBarItem),
                        20,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new IngredientElement(
                        typeof(GlassItem),
                        15,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new IngredientElement(
                        typeof(PaperItem),
                        20,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new IngredientElement(
                        "Lumber",
                        8,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ), //noloc
                },
                items: new List<CraftingElement> { new CraftingElement<ChemicalLaboratoryItem>() }
            );
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20; // Defines how much experience is gained when crafted.
            this.LaborInCalories = CreateLaborInCaloriesValue(120, typeof(MechanicsSkill));
            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ChemicalLaboratoryRecipe),
                start: 15,
                skillType: typeof(MechanicsSkill),
                typeof(MechanicsFocusedSpeedTalent),
                typeof(MechanicsParallelSpeedTalent)
            );
            this.Initialize(
                displayText: Localizer.DoStr("Chemical Laboratory"),
                recipeType: typeof(ChemicalLaboratoryRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }
}
