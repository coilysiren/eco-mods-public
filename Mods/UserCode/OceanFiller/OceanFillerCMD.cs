namespace OceanFiller
{
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.Messaging.Chat.Commands;
    using Eco.Shared.Localization;

    public class OceanFillerCmd
    {
        [ChatCommand("Turn the deep ocean around yourself into shallow ocean", ChatAuthorizationLevel.Admin)]
        public static void ShallowOcean(User user, int radius = 10)
        {
            user.Msg(Localizer.DoStr($"Turning deep ocean into shallow ocean within radius {radius}..."));
            OceanFillerCore.SetShallowOceanAroundSelf(user, radius);
            user.Msg(Localizer.DoStr("The deep ocean has been turned into shallow ocean."));
        }
    }
}
