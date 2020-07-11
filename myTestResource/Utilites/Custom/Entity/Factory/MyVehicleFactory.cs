using AltV.Net;
using AltV.Net.Elements.Entities;
using System;

namespace le4ge.Utilites.Custom.Entity.Factory
{
    public class MyVehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(IntPtr vehiclePointer, ushort id)
        {
            return new MyVehicle(vehiclePointer, id);
        }
    }
}