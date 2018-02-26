using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using LarkFramework;
using LarkFramework.Tick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Net
{
    public class PhotonManager : SingletonMono<PhotonManager>, IPhotonPeerListener
    {
        public string ipAddress;                //服务器Ip地址
        public string port;                     //服务器端口号
        public string applicationName;          //服务器应用名

        public string loginUserName;

        //连接方式
        public ConnectionProtocol connectionProtocol = ConnectionProtocol.Udp;

        public const string LOG_TAG = "[PhotonManager]";

        private PhotonPeer peer;
        public PhotonPeer Peer { get { return peer; } }

        #region Request操作
        private Dictionary<OperationCode, RequestBase> dict_Reuqest;
        public void AddReuqest(RequestBase request)
        {
            dict_Reuqest.Add(request.opCode, request);
        }

        public void RemoveRequest(RequestBase request)
        {
            dict_Reuqest.Remove(request.opCode);
        }
        #endregion

        #region Event操作
        private Dictionary<EventCode, EventBase> dic_Event = new Dictionary<EventCode, EventBase>();
        public void AddEvent(EventBase e)
        {
            dic_Event.Add(e.eventCode, e);
        }

        public void RemoveEvent(EventBase e)
        {
            dic_Event.Remove(e.eventCode);
        }
        #endregion

        public void Init()
        {
            dict_Reuqest = new Dictionary<OperationCode, RequestBase>();

            //通过Listender接收服务器端的响应
            peer = new PhotonPeer(this, connectionProtocol);
            peer.Connect(ipAddress + ":" + port, applicationName);

            //加入Tick
            TickManager.Instance.m_TickComponent.onUpdate += OnUpdate;

            this.Log(LOG_TAG + "Init Finished");
        }

        /// <summary>
        /// Update轮询
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (peer != null)
            {
                peer.Service();     //photon规定必须调用,检测服务连接
            }
        }

        private void OnDestroy()
        {
            if (peer != null && peer.PeerState == PeerStateValue.Connected)
            {
                peer.Disconnect();      //断开连接
            }
        }

        #region Photon操作
        public void DebugReturn(DebugLevel level, string message)
        {

        }

        public void OnEvent(EventData eventData)
        {
            EventCode code = (EventCode)eventData.Code;
            EventBase eventHnadler = DictTool.GetValue<EventCode, EventBase>(dic_Event,code);
            eventHnadler.OnEvent(eventData);

            //switch (eventData.Code)
            //{
            //    case 1:
            //        Dictionary<byte, object> data = eventData.Parameters;
            //        object intValue;
            //        data.TryGetValue(5, out intValue);
            //        object stringValue;
            //        data.TryGetValue(6, out stringValue);
            //        Debug.Log(string.Format("[Peer]<5,{0}>,<6,{1}>", intValue, stringValue));
            //        break;

            //    case 2:
            //        break;

            //    default:
            //        break;
            //}
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            OperationCode opCode = (OperationCode)operationResponse.OperationCode;

            RequestBase request = null;
            if (dict_Reuqest.TryGetValue(opCode, out request))
            {
                request.OnOperationResponse(operationResponse);
            }
            else
            {
                Debug.Log(LOG_TAG+ opCode + " is Null!");
            }
        }

        /// <summary>
        /// 当连接状态改变的时候
        /// </summary>
        /// <param name="statusCode"></param>
        public void OnStatusChanged(StatusCode statusCode)
        {
            Debug.Log(statusCode);
        }
        #endregion
    }
}
