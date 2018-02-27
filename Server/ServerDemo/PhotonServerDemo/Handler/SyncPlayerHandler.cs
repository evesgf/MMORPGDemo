using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using LitJson;

namespace PhotonServerDemo.Handler
{
    class SyncPlayerHandler : HandlerBase
    {
        public SyncPlayerHandler()
        {
            opCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            //取得所有已经登录的在线玩家用户名
            List<string> onlineUserNameList = new List<string>();
            foreach (Client p in ServerEntry.peerList)
            {
                if (!string.IsNullOrEmpty(p.loginUserName) && p != peer)
                {
                    onlineUserNameList.Add(p.loginUserName);
                }
            }
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.OnlineUserNameList, JsonMapper.ToJson(onlineUserNameList));
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response,sendParameters);

            //通知其他客户端有新客户端加入
            foreach (Client p in ServerEntry.peerList)
            {
                if (!string.IsNullOrEmpty(p.loginUserName) && p != peer)
                {
                    EventData ed = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte, object> data2 = new Dictionary<byte, object>();
                    data2.Add((byte)ParameterCode.UserName, peer.loginUserName);
                    ed.Parameters = data2;
                    p.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
