using System;

namespace le4ge.Utilites.Custom
{
    public class CustomCharacter
    {
        public int ClientID { get; set; }
        public int CharacterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public int Age { get; set; }

        public string Position { get; set; }
        public bool isJailed { get; set; }

        public string Gander { get; set; }
        public int Energy { get; set; }
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public string Work { get; set; }
        public int Cash { get; set; }
        public int Bank { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public string Model { get; set; }
    }
}