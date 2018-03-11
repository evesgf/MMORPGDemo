using Common;
using Common.Tools;
using Photon.SocketServer;
using PhotonServerDemo.Cache;
using PhotonServerDemo.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.Handler
{
    public class StartMatchHandler: HandlerBase
    {
        public StartMatchHandler()
        {
            opCode = OperationCode.StartMatch;
        }

        private RoomCacahe roomCacahe = Caches.roomCacahe;

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            bool isMatchRoom = (bool)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.MatchRoom);
            //bool isStopMatch = (bool)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.StopMatch);

            if (isMatchRoom)
            {
                RoomBase r;
                if (roomCacahe.dict_RoomList.Count != 0)
                {
                    r = roomCacahe.dict_RoomList.First().Value;
                    if (r.ClientList.Count < r.Count)
                    {
                        roomCacahe.dict_playerRoom.TryAdd(r.ClientList.Count + 1, r.Id);
                        r.ClientList.Add(peer);

                    }
                    else
                    {
                        //房间满了
                    }
                }
                else
                {
                    //没房间了
                    r=roomCacahe.CreateRoom();
                    roomCacahe.dict_RoomList.TryAdd(roomCacahe.dict_RoomList.Count+1, r);
                    roomCacahe.dict_playerRoom.TryAdd(r.ClientList.Count + 1, r.Id);
                    r.ClientList.Add(peer);
                }

                //检查是否还要等人，不等就开了
                if (r.ClientList.Count == r.Count)
                {
                    //通知所有客户端开始准备游戏
                    foreach (Client p in r.ClientList)
                    {
                        EventData ed = new EventData((byte)EventCode.StartRoomGame);
                        p.SendEvent(ed, sendParameters);
                    }
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                    response.ReturnCode = (short)ReturnCode.Success;
                }
            }
            else
            {

            }
        }
    }
}
