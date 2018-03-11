using PhotonServerDemo.Room;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.Cache
{
    public class RoomCacahe
    {
        //房间Id对应的房间数据
        public ConcurrentDictionary<int, RoomBase> dict_RoomList = new ConcurrentDictionary<int, RoomBase>();

        //玩家Id对应的房间Id
        public ConcurrentDictionary<int, int> dict_playerRoom = new ConcurrentDictionary<int, int>();

        public RoomBase CreateRoom()
        {
            RoomBase room = null;
            room = new RoomBase(dict_RoomList.Count+1, 2);
            return room;
        }
    }
}
