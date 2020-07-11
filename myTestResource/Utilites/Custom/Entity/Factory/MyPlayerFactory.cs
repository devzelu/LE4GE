using AltV.Net;
using AltV.Net.Elements.Entities;
using System;

namespace le4ge.Utilites.Custom.Entity.Factory
{
    public class MyPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IntPtr playerPointer, ushort id)
        {
            return new MyPlayer(playerPointer, id);
        }
    }
}