#pragma warning disable IDE0005
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
#pragma warning restore IDE0005

using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Occupancy;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using static Eco.Gameplay.Components.PartsComponent; // would like to remove the static import, but not sure how to do it

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
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(ForSaleComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(24)]
    [RequireRoomMaterialTier(
        2.8f,
        typeof(BiochemistLavishReqTalent),
        typeof(BiochemistFrugalReqTalent)
    )]
    [Tag("Usable")]
    [Ecopedia("Work Stations", "Researching", subPageName: "Chemical Laboratory Item")]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    public partial class ChemicalLaboratoryObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(ChemicalLaboratoryItem);
        public override LocString DisplayName => Localizer.DoStr("Chemical Laboratory");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        static ChemicalLaboratoryObject()
        {
            AddOccupancy<ChemicalLaboratoryObject>(
                new List<BlockOccupancy>()
                {
                    new(new Vector3i(0, 0, 0)),
                    new(new Vector3i(1, 0, 0)),
                    new(new Vector3i(2, 0, 0)),
                    new(new Vector3i(0, 1, 0)),
                    new(new Vector3i(1, 1, 0)),
                    new(new Vector3i(2, 1, 0)),
                }
            );
        }

        protected override void Initialize()
        {
            GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartInfo[]
                    {
                        new() { TypeName = nameof(PaperItem), Quantity = 10 },
                    }
                );
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
        ItemTypes = new[] { typeof(BiochemistUpgradeItem) }
    )] //noloc
    public partial class ChemicalLaboratoryItem
        : WorldObjectItem<ChemicalLaboratoryObject>,
            IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext =>
            new SideAttachedContext(
                0 | DirectionAxisFlags.Down,
                WorldObject.GetOccupancyInfo(WorldObjectType)
            );

        [
            Serialized,
            SyncToView,
            NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)
        ]
        public object? PersistentData { get; set; }
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]
    [Ecopedia("Work Stations", "Researching", subPageName: "Chemical Laboratory Item")]
    public partial class ChemicalLaboratoryRecipe : RecipeFamily
    {
        public ChemicalLaboratoryRecipe()
        {
            Recipe recipe = new();
            recipe.Init(
                name: "Chemical Laboratory", //noloc
                displayName: Localizer.DoStr("Chemical Laboratory"),
                ingredients: new List<IngredientElement>
                {
                    new(
                        typeof(IronBarItem),
                        20,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new(
                        typeof(GlassItem),
                        15,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new(
                        typeof(PaperItem),
                        20,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ),
                    new(
                        "Lumber",
                        8,
                        typeof(MechanicsSkill),
                        typeof(MechanicsLavishResourcesTalent)
                    ), //noloc
                },
                items: new List<CraftingElement> { new CraftingElement<ChemicalLaboratoryItem>() }
            );
            Recipes = new List<Recipe> { recipe };
            ExperienceOnCraft = 20; // Defines how much experience is gained when crafted.
            LaborInCalories = CreateLaborInCaloriesValue(120, typeof(MechanicsSkill));
            CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ChemicalLaboratoryRecipe),
                start: 15,
                skillType: typeof(MechanicsSkill),
                typeof(MechanicsFocusedSpeedTalent),
                typeof(MechanicsParallelSpeedTalent)
            );
            Initialize(
                displayText: Localizer.DoStr("Chemical Laboratory"),
                recipeType: typeof(ChemicalLaboratoryRecipe)
            );
            CraftingComponent.AddRecipe(tableType: typeof(MachinistTableObject), recipe: this);
        }
    }
}
