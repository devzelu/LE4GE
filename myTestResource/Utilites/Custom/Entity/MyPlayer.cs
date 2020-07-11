using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace le4ge.Utilites.Custom.Entity
{
    public class MyPlayer : Player
    {
        public int GlobalID { get; set; }
        public bool isAdmin { get; set; }
        public bool isDead { get; set; }
        public int ClientID { get; set; }
        public int CharacterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public int Age { get; set; }
        public bool isJailed { get; set; }

        public string Gander { get; set; }
        public int Energy { get; set; }
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public string Work { get; set; }
        public int Cash { get; set; }
        public int Bank { get; set; }

        public MyPlayer(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
        }
    }
}