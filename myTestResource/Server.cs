using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using le4ge.Utilites.Custom;
using le4ge.Utilites.Custom.Entity;
using le4ge.Utilites.Custom.Entity.Factory;
using System;
using System.Collections.Generic;
using System.Numerics;
using logger = le4ge.Utilites.Custom.CustomLogger;

namespace le4ge
{
    internal class Server : Resource
    {
        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new MyPlayerFactory();
        }

        public override void OnStart()
        {
            Alt.OnPlayerConnect += OnPlayerConnect;
            Alt.OnPlayerDisconnect += OnPlayerDisconnect;
            Alt.OnServer<IPlayer, string>("OnPlayerSendCommand", OnPlayerSendCommand);
            Alt.OnServer<IPlayer, string>("spawnCharacter:id", PlayerSelectCharacter);
            Alt.OnServer<IPlayer, Array>("CreateCharacter", CreateCharacter);
        }

        private void OnPlayerSendCommand(IPlayer myplayer, string msg)
        {
            var player = (MyPlayer)myplayer;

            var args = msg.Split(" ");//Split the message by space to use the args later. For example: "/ban Name Reason".
            var cmd = msg.Split(" ")[0].ToLower();//For a better overwiev we will create a variable called "cmd" which contains the first split. For example "/ban".

            switch (cmd)
            {
                case "/veh":
                case "/vehicle":
                    if (player.isAdmin)
                    {
                        VehicleRelated.CreateVehicle(player, args[1]);
                    }
                    else
                    {
                        CustomNotification.PermissionFailed(player);
                    }
                    break;

                case "/weapon":
                case "/gun":
                    if (player.isAdmin)
                    {
                        WeaponRelated.GiveWeapon(player, args[1], int.Parse(args[2]));
                    }
                    else
                    {
                        CustomNotification.PermissionFailed(player);
                    }
                    break;

                case "/pos":
                    break;

                case "/dv":
                    if (player.isAdmin)
                    {
                        VehicleRelated.DespawnVehicle(player);
                    }
                    else
                    {
                        CustomNotification.PermissionFailed(player);
                    }
                    break;

                case "/revive":
                case "/rev":
                    if (player.isAdmin)
                    {
                        if (args.Length == 1)
                        {
                            PlayerRelated.RevivePlayer(player);
                        }
                        else
                        {
                            PlayerRelated.RevivePlayer(player, int.Parse(args[1]));
                        }
                    }
                    else
                    {
                        CustomNotification.PermissionFailed(player);
                    }
                    break;

                case "/kill":
                    if (player.isAdmin)
                    {
                        if (args.Length == 1)
                        {
                            PlayerRelated.KillPlayer(player);
                        }
                        else
                        {
                            killPlayer(player, args[1]);
                        }
                    }
                    else
                    {
                        CustomNotification.PermissionFailed(player);
                    }
                    break;

                case "/pm":
                    //{
                    //    args = args.Where(val => val != args[0] && val != args[1]).ToArray(); //Ignore the 0 arg (the "/ban") and the 1 arg ("GangstaSunny")
                    //    string message = string.Join(" ", args); //The other args will now be in the "message"-variable
                    //    SendPMToPlayer(player, args[1], message);
                    //}
                    break;

                case "/tpto":

                    break;

                case "/test":
                    ChatHandler chat = new ChatHandler();
                    //NEED TO FIX
                    //chat.SendSuccessMessage(player, "test succes message");
                    // Alt.EmitAllClients("chatmessage", "test message");
                    break;

                case "/help":
                    break;

                default:
                    CustomNotification.NotifyClientError(player, 3000, $"Command {args[0].ToString()} has not been found");
                    break;
            }
        }

        private void OnPlayerDisconnect(IPlayer player, string reason)
        {
            var myPlayer = (MyPlayer)player;
            CustomFunctions custom = new CustomFunctions();
            myPlayer.GetData<int>("CharacterID", out var characterid);
            custom.SavePosition(myPlayer, characterid);
            logger.Info($"{myPlayer.Name} has disconnected: last Position {myPlayer.Position}");
            myPlayer.Despawn();
        }

        private void OnPlayerConnect(IPlayer player, string reason)
        {
            var myPlayer = (MyPlayer)player;
            CustomFunctions custom = new CustomFunctions();
            logger.Info($"{myPlayer.Name} has connected, Sid:{myPlayer.SocialClubId}");
            var plr = custom.PlayerInfo(myPlayer);
            var charsValues = plr.Count;
            switch (charsValues)
            {
                case 1:
                    myPlayer.Emit("OnPlayerConnected", 1, plr[0].CharacterID, plr[0].FirstName + " " + plr[0].LastName, plr[0].Date, plr[0].Gander, plr[0].Work, plr[0].Cash, plr[0].Bank);
                    break;

                case 2:
                    myPlayer.Emit("OnPlayerConnected", 2, plr[0].CharacterID, plr[0].FirstName + " " + plr[0].LastName, plr[0].Date, plr[0].Gander, plr[0].Work, plr[0].Cash, plr[0].Bank, plr[1].CharacterID, plr[1].FirstName + " " + plr[1].LastName, plr[1].Date, plr[1].Gander, plr[1].Work, plr[1].Cash, plr[1].Bank);
                    break;

                case 3:
                    myPlayer.Emit("OnPlayerConnected", 3, plr[0].CharacterID, plr[0].FirstName + " " + plr[0].LastName, plr[0].Date, plr[0].Gander, plr[0].Work, plr[0].Cash, plr[0].Bank, plr[1].CharacterID, plr[1].FirstName + " " + plr[1].LastName, plr[1].Date, plr[1].Gander, plr[1].Work, plr[1].Cash, plr[1].Bank, plr[2].CharacterID, plr[2].FirstName + " " + plr[2].LastName, plr[2].Date, plr[2].Gander, plr[2].Work, plr[2].Cash, plr[2].Bank);
                    break;

                default:
                    break;
            }
        }

        private void CreateCharacter(IPlayer player, Array data)
        {
        }

        private void PlayerSelectCharacter(IPlayer player, string characterid)
        {
            var myPlayer = (MyPlayer)player;
            CustomFunctions custom = new CustomFunctions();
            var playerInfo = custom.PlayerInfo(myPlayer);
            myPlayer.isAdmin = playerInfo[0].isAdmin;
            myPlayer.GlobalID = playerInfo[0].ClientID;
            var characterSelected = int.Parse(characterid);
            if (characterSelected == 0)
            {
                // CreateCharacter(myPlayer);
            }
            else if (characterSelected == 1 || characterSelected == 2 || characterSelected == 3)
            {
                List<CustomCharacter> playerSelected = new List<CustomCharacter>();

                var x = playerInfo.Count;
                for (int i = 0; i < x; i++)
                {
                    if (playerInfo[i].CharacterID == characterSelected)
                    {
                        playerSelected.Add(new CustomCharacter
                        {
                            ClientID = playerInfo[i].ClientID,
                            CharacterID = playerInfo[i].CharacterID,
                            FirstName = playerInfo[i].FirstName,
                            LastName = playerInfo[i].LastName,
                            Date = playerInfo[i].Date,
                            Age = playerInfo[i].Age,
                            Position = playerInfo[i].Position,
                            isJailed = playerInfo[i].isJailed,
                            Gander = playerInfo[i].Gander,
                            Energy = playerInfo[i].Energy,
                            Hunger = playerInfo[i].Hunger,
                            Thirst = playerInfo[i].Thirst,
                            Work = playerInfo[i].Work,
                            Cash = playerInfo[i].Cash,
                            Bank = playerInfo[i].Bank,
                            Model = playerInfo[i].Model
                        }
                       );
                    }
                }
                if (playerSelected[0].Gander == "Female")
                {
                    if (playerSelected[0].Model != "0x9C9EFFD8")
                    {
                        var model = Alt.Hash(playerSelected[0].Model);
                        myPlayer.Model = model;
                    }
                    else
                    {
                        myPlayer.Model = 0x9C9EFFD8;
                    }
                }
                else
                {
                    if (playerSelected[0].Model != "0x705E61F2")
                    {
                        var model = Alt.Hash(playerSelected[0].Model);
                        myPlayer.Model = model;
                    }
                    else
                    {
                        myPlayer.Model = 0x705E61F2;
                    }
                }

                if (playerSelected[0].Position != "")
                {
                    string[] array = playerSelected[0].Position.Split(',');
                    Vector3 result = new Vector3(
                        float.Parse(array[0]),
                        float.Parse(array[1]),
                        float.Parse(array[2])
                        );
                    myPlayer.Spawn(new Position(result.X, result.Y, result.Z), 10);
                }
                else
                {
                    myPlayer.Spawn(new Position(-1054, -2769, 5), 10);
                }

                myPlayer.SetData("CharacterID", 1);
                myPlayer.Emit("OnPlayerLoaded", myPlayer.GlobalID, playerSelected[0].CharacterID);
            }
        }

        public override void OnStop()
        {
        }

        public void revive(MyPlayer player, string target)
        {
            CustomFunctions custom = new CustomFunctions();
            if (player.Exists)
            {
                custom.RevivePlayer(player, target);
            }
        }

        public void killPlayer(MyPlayer player, string target)
        {
            CustomFunctions custom = new CustomFunctions();

            custom.KillPlayer(player, target);
        }
    }

    public class VehicleRelated : IScript
    {
        public static void CreateVehicle(MyPlayer player, string VehicleName)
        {
            IVehicle veh = Alt.CreateVehicle(Alt.Hash(VehicleName), new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z), player.Rotation);
            //If the Vehicle Creation was successfull, then it should notify you.
            if (veh != null)
            {
                CustomNotification.NotifyClientSuccess(player, 3000, $"You spawn {VehicleName}");
            }
        }

        public static void DespawnVehicle(MyPlayer player)
        {
            if (player.Exists)
            {
                if (player.IsInVehicle)
                {
                    Alt.RemoveVehicle(player.Vehicle);
                }
                else
                {
                    CustomNotification.NotifyClientError(player, 3000, "You are not in any vehicle");
                }
            }
        }
    }

    public class WeaponRelated : IScript
    {
        public static void GiveWeapon(MyPlayer player, string WeaponName, int ammo)
        {
            var model = Alt.Hash(WeaponName);

            player.GiveWeaponAsync(model, ammo, false);
            if (model != 0)
            {
                CustomNotification.NotifyClientSuccess(player, 3000, $"You spawn {WeaponName}");
            }
        }
    }

    public class PlayerEvent : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerDead)]
        public static void OnPlayerDeath(MyPlayer player, IEntity killer, uint reason)
        {
            player.isDead = true;
            lock (player)
            {
                player.Emit("player:death");
            }
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public static void OnPlayerEnterVehicle(IVehicle vehicle, MyPlayer player, byte seat)
        {
            logger.Info($"{player.Name} enter into: {vehicle.NumberplateText} seat:{seat}");
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public static void OnPlayerLeaveVehicle(IVehicle vehicle, MyPlayer player, byte seat)
        {
            logger.Info($"{player.Name} leave: {vehicle.NumberplateText}");
        }

        [ScriptEvent(ScriptEventType.PlayerChangeVehicleSeat)]
        public static void OnPlayerChangeVehicleSeat(IVehicle vehicle, MyPlayer player, byte oldSeat, byte newSeat)
        {
            logger.Info($"{player.Name} changed seat: {oldSeat} to {newSeat}");
        }
    }

    public class PlayerRelated : IScript
    {
        [ClientEvent("revive:player")]
        public static void RevivePlayer(MyPlayer player)
        {
            player.Spawn(player.Position, 100);
            player.Emit("player:revived");
        }

        public static void RevivePlayer(MyPlayer player, int target)
        {
        }

        public static void KillPlayer(MyPlayer player)
        {
            player.Health = 0;
        }
    }
}