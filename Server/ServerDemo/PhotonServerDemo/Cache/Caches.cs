using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.Cache
{
    public class Caches
    {
        public static RoomCacahe roomCacahe;

        static Caches()
        {
            roomCacahe = new RoomCacahe();
        }
    }
}
