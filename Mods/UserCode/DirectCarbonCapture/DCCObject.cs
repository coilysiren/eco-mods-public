namespace DirectCarbonCapture
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.SharedTypes;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RepairRequiresSkill(typeof(MechanicsSkill), 1)]
    [Tag("Usable")]
    public partial class DirectCarbonCaptureObject : WorldObject
    {
        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override InteractionTargetPriority TargetPriority => InteractionTargetPriority.High;

        protected override void Initialize()
        {
            MinimapComponent minimap = this.GetComponent<MinimapComponent>();
            minimap.SetCategory(Localizer.DoStr("Power"));
            this.GetComponent<PartsComponent>()
                .Config(
                    () => LocString.Empty,
                    new PartsComponent.PartInfo[]
                    {
                        new() { TypeName = nameof(GearboxItem), Quantity = 2 },
                    }
                );

            AirPollutionComponent airPollution = this.GetComponent<AirPollutionComponent>();
            airPollution.Initialize(-1);
        }
    }
}
