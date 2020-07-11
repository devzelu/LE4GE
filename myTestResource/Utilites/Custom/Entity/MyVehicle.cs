using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System;

namespace le4ge.Utilites.Custom.Entity
{
    public class MyVehicle : Vehicle
    {
        public int Owner { get; set; }

        public MyVehicle(uint model, Position position, Rotation rotation) : base(model, position, rotation)
        {
        }

        public MyVehicle(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
        }
    }
}