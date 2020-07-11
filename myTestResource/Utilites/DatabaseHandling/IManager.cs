using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using le4ge.Utilites.Custom;

namespace le4ge
{
    public interface IManager
    {
        void SavePosition(int characterID, string position);

        List<CustomCharacter> SelectInfo(ulong Sid);
    }
}