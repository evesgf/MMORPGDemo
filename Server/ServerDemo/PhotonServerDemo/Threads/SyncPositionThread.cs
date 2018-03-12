using Common;
using LitJson;
using Photon.SocketServer;
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
        public void Run()
        {
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
                //同步位置
                SendPosition();
            }
        }

        private void SendPosition()
        {
            List<PlayerData> playerDatatList = new List<PlayerData>();
            foreach (Client p in ServerEntry.peerList)
            {
                if (p.loginUserId!=0)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.gameUserId = p.loginUserId;
                    playerData.pos = new Vector3Data() { x = p.x, y = p.y, z = p.z };
                    playerDatatList.Add(playerData);
                }

                string playerDatatListStr = JsonMapper.ToJson(playerDatatList);
                Dictionary<byte, object> data = new Dictionary<byte, object>();
                data.Add((byte)ParameterCode.PlayerDataList, playerDatatListStr);

                foreach (Client p1 in ServerEntry.peerList)
                {
                    if (p.loginUserId!=0)
                    {
                        EventData ed = new EventData((byte)EventCode.SyncPosition);
                        ed.Parameters = data;
                        p1.SendEvent(ed, new SendParameters());
                    }
                }
            }
        }
    }
}
