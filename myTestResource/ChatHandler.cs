using AltV.Net;
using AltV.Net.Elements.Entities;
using le4ge.Utilites.Custom.Entity;
using System;
using System.Drawing;

namespace le4ge
{
    public class ChatHandler
    {
        private readonly Action<string> broadcast;
        private readonly Action<IPlayer, string> send;

        public ChatHandler()
        {
            Alt.Import("chat", "broadcast", out broadcast);
            Alt.Import("chat", "send", out send);
        }

        public void Brodcast(string message)
        {
            broadcast?.Invoke(message);
        }

        public void Send(IPlayer player, string message)
        {
            send?.Invoke(player, message);
        }

        public void SendErrorMessage(IPlayer player, string message)
        {
            Send(player, "{FF0000}[System]{FFFFFF}: " + message);
        }

        public void SendSuccessMessage(IPlayer player, string message)
        {
            Send(player, "{00FF00}[System]{FFFFFF}: " + message);
        }

        /// <summary>
        /// Send System message to player with custom kolor
        /// </summary>
        /// <param name="player">Message target</param>
        /// <param name="message">Message</param>
        /// <param name="r">Red channel for rgbColor</param>
        /// <param name="g">Green channel for rgbColor</param>
        /// <param name="b">Blue channel for rgbColor</param>
        public void SendCustomHexMessage(IPlayer player, string message, int r, int g, int b)
        {
            Color myColor = Color.FromArgb(r, g, b);
            string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");
            var hexString = "{" + hex + "} [System] {FFFFFF}: ";
            Send(player, hexString + message);
        }

        public void SendPM(IPlayer player, IPlayer target, string message)
        {
            Send(target, player.Name + "{B3046A}[PM]{FFFFFF}: " + message);
        }

        public void PMSended(IPlayer player, IPlayer target, string message)
        {
            Send(player, target.Name + "{B3046A}[PM]{FFFFFF}: " + message);
        }
    }
}