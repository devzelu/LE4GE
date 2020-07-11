using AltV.Net.Elements.Entities;
using le4ge.Utilites.Custom;

namespace le4ge
{
    public class CustomNotification
    {
        public static void NotifyClient(IPlayer player, int duration, string message)
        {
            player.Emit("notifyClient", duration, $"{message}");
        }

        public static void NotifyClientError(IPlayer player, int duration, string message)
        {
            player.Emit("notifyClientError", duration, $"{message}");
        }

        public static void NotifyClientSuccess(IPlayer player, int duration, string message)
        {
            player.Emit("notifyClientSuccess", duration, $"{message}");
        }

        public static void PermissionFailed(IPlayer player)
        {
            CustomNotification.NotifyClientError(player, 5000, "You don't have a permission to use this command");
            CustomLogger.Error($"{player.Name} tried admin command");
        }
    }
}