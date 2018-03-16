using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonServerDemo.Cache;
using Common.Tools;
using LitJson;
using PhotonServerDemo.DLL;

namespace PhotonServerDemo.Handler
{
    public class RoomFightHandler: HandlerBase
    {
        public RoomFightHandler()
        {
            opCode = OperationCode.RoomFight;
        }

        private RoomCacahe roomCacahe = Caches.roomCacahe;

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            object onRoomFight = (object)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.RoomFight);

            switch (onRoomFight)
            {
                case OnRoomFight.Enter:
                    OnRoomFightEnter(operationRequest, sendParameters, peer);
                    break;

                case OnRoomFight.GetInfo:
                    break;

                case OnRoomFight.Walk:
                    break;

                case OnRoomFight.Damage:
                    break;

                case OnRoomFight.GameOver:
                    break;
            }
        }

        private void OnRoomFightEnter(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            //检测是否在准备队列
            var roomId = roomCacahe.dict_playerRoom[peer.loginUserId];
            var room = roomCacahe.dict_RoomList[roomId];
            if (roomId != 0)
            {
                room.dict_UserData.TryGetValue(peer.loginUserId, out PlayerData data);
                if (data == null)
                {
                    //准备玩家数据
                    using (var db = new MySqlContext())
                    {
                        //查询gameUser
                        var gameUser = (from v in db.Game_User
                                        join c in db.Sys_User
                                        on v.User.Id equals c.Id
                                        where v.Id == peer.loginUserId
                                        select v).FirstOrDefault();

                        //查询character
                        var character = (from v in db.Game_Character
                                         join c in db.Game_User
                                         on v.Id equals c.Game_Character.Id
                                         where c.Id == gameUser.Id
                                         select v).FirstOrDefault();

                        //查询角色数据


                        //存入同步数据
                        PlayerData playerData = new PlayerData
                        {
                            userId = peer.loginUserId,
                            gameUserId = gameUser.Id,
                            characterId = character.Id,
                            pos = new Vector3Data(),
                            rotation = new RotationData(),
                            animation = 0
                        };

                        room.dict_UserData.TryAdd(peer.loginUserId, playerData);
                    }
                }
                else
                {
                    //已经准备了
                }
            }
            else
            {
                //房间不存在？！
            }

            //检测是否全都准备好了
            if (room.ClientList.Count != room.dict_UserData.Count)
            {
                return;
            }

            //开启同步线程
            room.syncPositionThread.Run(room);

            //广播通知客户端正式开始
            room.Broadcast(OnRoomFight.RDY);
        }
    }
}
