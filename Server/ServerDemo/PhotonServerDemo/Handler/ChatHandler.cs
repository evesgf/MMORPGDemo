using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common.Tools;

namespace PhotonServerDemo.Handler
{
    public class ChatHandler : HandlerBase
    {
        public ChatHandler()
        {
            opCode = OperationCode.Chat;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            string chatInfo = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.ChatInfo) as string;

            ServerEntry.log.Info(chatInfo+"_"+ ServerEntry.peerList.Count);

            //通知其他客户端有人发了群聊
            foreach (Client p in ServerEntry.peerList)
            {
                if (p.loginUserId != 0)
                {
                    EventData ed = new EventData((byte)EventCode.Chat);
                    Dictionary<byte, object> data = new Dictionary<byte, object>();
                    data.Add((byte)ParameterCode.ChatInfo, "["+ DateTime.Now.ToString("HH:mm:ss") +"]"+ chatInfo);
                    ed.Parameters = data;
                    p.SendEvent(ed, sendParameters);
                }
            }
        }
    }
}
