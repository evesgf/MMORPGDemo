using Common;
using LitJson;
using Photon.SocketServer;
using PhotonServerDemo.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotonServerDemo.Threads
{
    /// <summary>
    /// 用于位置同步
    /// </summary>
    public class SyncPositionThread
    {
        private Thread t;
        private RoomBase room;

        public void Run(RoomBase r)
        {
            room = r;

            t = new Thread(UpdatePosition);
            t.IsBackground = true;
            t.Start();
        }

        public void Stop()
        {
            t.Abort();
        }

        private void UpdatePosition()
        {
            Thread.Sleep(5);

            while (true)
            {
                Thread.Sleep(200);
                //同步玩家数据
                SendPlayersData();
            }
        }

        private void SendPlayersData()
        {
            List<PlayerData> playerDatatList = new List<PlayerData>();
            foreach (Client p in room.ClientList)
            {
                if (p.loginUserId != 0)
                {
                    var playerData = room.dict_UserData[p.loginUserId];
                    playerDatatList.Add(playerData);
                }
            }

            string playerDatatListStr = JsonMapper.ToJson(playerDatatList);
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList, playerDatatListStr);

            foreach (Client p1 in room.ClientList)
            {
                if (p1.loginUserId != 0)
                {
                    EventData ed = new EventData((byte)EventCode.SyncPlayerData);
                    ed.Parameters = data;
                    p1.SendEvent(ed, new SendParameters());
                }
            }
        }
    }
}
