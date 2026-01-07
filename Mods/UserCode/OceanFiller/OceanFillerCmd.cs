namespace OceanFiller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.Messaging.Chat.Commands;
    using Eco.Mods.TechTree;
    using Eco.Shared.IoC;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.WorldLayers;

    [ChatCommandHandler]
    public static class OceanFillerCommands
    {
        [ChatCommand(
            "Turn the deep ocean around yourself into shallow ocean",
            ChatAuthorizationLevel.User
        )]
        public static void ShallowOcean(User user, int radius = 10)
        {
            var currentVehicle = ServiceHolder<IWorldObjectManager>
                .Obj.GetObjectsWithin(user.Position, 10f)
                .FirstOrDefault(x =>
                    x.GetComponent<VehicleComponent>()?.Driver?.User.Equals(user) ?? false
                );

            if (currentVehicle == null)
            {
                user.Msg(Localizer.DoStr("You must be driving a vehicle to use this command."));
                return;
            }

            bool isIndustrialBarge = currentVehicle.GetType().Name == nameof(IndustrialBargeObject);

            if (!isIndustrialBarge)
            {
                user.Msg(
                    Localizer.DoStr("You must be driving an Industrial Barge to use this command.")
                );
                return;
            }

            bool isDeepOcean = WorldLayerUtils.IsInDeepOcean(user.Position.XZi());

            if (!isDeepOcean)
            {
                user.Msg(Localizer.DoStr("You are not currently in deep ocean."));
                return;
            }

            user.Msg(
                Localizer.DoStr($"Turning deep ocean into shallow ocean within radius {radius}...")
            );
            OceanFillerCore.SetShallowOceanAroundSelf(user, radius);
            user.Msg(Localizer.DoStr("The deep ocean has been turned into shallow ocean."));
        }
    }
}
