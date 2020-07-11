using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using System;
using System.Linq;
using System.Numerics;
using logger = le4ge.Utilites.Custom.CustomLogger;
using System.Collections.Generic;
using le4ge.Utilites.Custom;
using le4ge.Utilites.Custom.Entity;

namespace le4ge
{
    public class CustomFunctions
    {
        private ChatHandler chat = new ChatHandler();

        public static IManager GetManager()
        {
            return new DatabaseManager();
        }

        public IPlayer PlayerParser(string target)
        {
            return Alt.GetAllPlayers()
                          .Where(target => target.Name.Any()).FirstOrDefault();
        }

        public void RevivePlayer(IPlayer player, string target)
        {
            var plr = PlayerParser(target);
            plr.Spawn(plr.Position, 1000);

            var myPlayer = (MyPlayer)player;
            if (myPlayer.isDead == true)
            {
                player.Emit("player:revived");
            }
            logger.Success($"{player.Name} revive {plr.Name}");
        }

        public void KillPlayer(IPlayer player, string target)
        {
            var plr = PlayerParser(target);
            plr.Health = 0;
        }

        public List<CustomCharacter> PlayerInfo(MyPlayer player)
        {
            var manager = GetManager();
            return manager.SelectInfo(player.SocialClubId);
        }

        public void SavePosition(IPlayer player, int characterID)
        {
            var manager = GetManager();
            var position = player.Position.ToString();
            var charsToRemove = new string[] { "Position", "x", ":", "y", "z", "(", ")", " " };
            foreach (var c in charsToRemove)
            {
                position = position.Replace(c, string.Empty);
            }
            manager.SavePosition(characterID, position);
        }

        public void SendPM(IPlayer player, string target, string message)
        {
            if (target == null)
            {
                CustomNotification.NotifyClientError(player, 5000, "Recipient not available");
            }
            else
            {
                var reciver = PlayerParser(target);
                chat.PMSended(player, reciver, message);
                chat.SendPM(player, reciver, message);
            }
        }
    }
}