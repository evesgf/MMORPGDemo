using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotonHostRuntimeInterfaces;
using PhotonServerDemo.Handler;
using Common.Tools;
using Common;

namespace PhotonServerDemo
{
    public class Client : ClientPeer
    {
        public float x, y, z;
        public string loginUserName;

        public Client(InitRequest initRequest) : base(initRequest)
        {
        }

        /// <summary>
        /// 处理客户端断开链接的后续操作
        /// </summary>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDetail"></param>
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            ServerEntry.peerList.Remove(this);
        }

        /// <summary>
        /// 处理客户端的请求
        /// </summary>
        /// <param name="operationRequest"></param>
        /// <param name="sendParameters"></param>
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler = DictTool.GetValue<OperationCode, HandlerBase>(ServerEntry.dict_Handler, (OperationCode)operationRequest.OperationCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                HandlerBase defaultHandler=DictTool.GetValue<OperationCode, HandlerBase>(ServerEntry.dict_Handler, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }

            //switch (operationRequest.OperationCode)
            //{
            //    case 1:
            //        ServerEntry.log.Info("[Peer]Get Message");

            //        //接收数据
            //        Dictionary<byte, object> data = operationRequest.Parameters;
            //        object intValue;
            //        data.TryGetValue(1, out intValue);
            //        object stringValue;
            //        data.TryGetValue(2, out stringValue);
            //        ServerEntry.log.Info(string.Format("[Peer]<1,{0}>,<2,{1}>", intValue, stringValue));

            //        //回传数据
            //        OperationResponse onResponse = new OperationResponse(1);
            //        Dictionary<byte, object> data2 = new Dictionary<byte, object>();
            //        data2.Add(3, 200);
            //        data2.Add(4, "测试数据2");
            //        onResponse.SetParameters(data2);
            //        SendOperationResponse(onResponse, sendParameters);  //回传客户端

            //        //服务端主动推
            //        Dictionary<byte, object> data3 = new Dictionary<byte, object>();
            //        data3.Add(5, 300);
            //        data3.Add(6, "服务端主动推");

            //        EventData ed = new EventData(1);
            //        ed.Parameters = data3;
            //        SendEvent(ed, new SendParameters());
            //        break;

            //    case 2:
            //        break;

            //    default:
            //        break;
            //}
        }
    }
}
