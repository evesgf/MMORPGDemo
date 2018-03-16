using Common;
using Common.Tools;
using LitJson;
using Photon.SocketServer;
using PhotonServerDemo.Cache;
using PhotonServerDemo.DLL;
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
            int isMatchRoom = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.MatchRoom);
            //bool isStopMatch = (bool)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.StopMatch);

            switch (isMatchRoom)
            {
                case 1: //Only Mode

                    break;

                case 2: //Room Mode
                    RoomMode(operationRequest, sendParameters,peer);
                    break;

                case 3: //Muliti Mode

                    break;

                case -1: //StopMatch
                    StopMatch(operationRequest, sendParameters, peer);
                    break;
            }
            
        }

        private void RoomMode(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            RoomBase r;
            if (roomCacahe.dict_RoomList.Count != 0)
            {
                r = roomCacahe.dict_RoomList.First().Value;
                if (r.ClientList.Count < r.Count)
                {
                    //玩家加入
                    roomCacahe.dict_playerRoom.TryAdd(peer.loginUserId, r.Id);
                    r.ClientList.Add(peer);

                    List<PlayerData> playerDatatList = new List<PlayerData>();

                    foreach (var p in r.ClientList)
                    {
                        //通知刷新房间数据
                        PlayerData playerData = new PlayerData();
                        playerData.userId = p.loginUserId;

                        //查询玩家相关数据
                        using (var db = new MySqlContext())
                        {
                            var gameUser = (from v in db.Game_User
                                            join c in db.Sys_User
                                            on v.User.Id equals c.Id
                                            where c.Id == p.loginUserId
                                            select v).FirstOrDefault();

                            if (gameUser != null)
                            {
                                var character = (from v in db.Game_Character
                                                 join c in db.Game_User
                                                 on v.Id equals c.Game_Character.Id
                                                 where c.Id == gameUser.Id
                                                 select v).FirstOrDefault();

                                playerData.gameUserId = gameUser.Id;
                                playerData.characterId = character.Id;

                                playerDatatList.Add(playerData);
                            }
                        }
                    }

                    string playerDatatListStr = JsonMapper.ToJson(playerDatatList);
                    Dictionary<byte, object> data = new Dictionary<byte, object>();
                    data.Add((byte)ParameterCode.PlayerDataList, playerDatatListStr);

                    EventData ed = new EventData((byte)EventCode.RoomUsersInfo);
                    ed.Parameters = data;
                    foreach (var p in r.ClientList)
                    {
                        p.SendEvent(ed, sendParameters);
                    }
                }
                else
                {
                    //TODO:房间满了
                }
            }
            else
            {
                //没房间了
                r = roomCacahe.CreateRoom();
                roomCacahe.dict_RoomList.TryAdd(roomCacahe.dict_RoomList.Count + 1, r);
                roomCacahe.dict_playerRoom.TryAdd(peer.loginUserId, r.Id);
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

        internal void StopMatch(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            if (!roomCacahe.dict_playerRoom.TryGetValue(peer.loginUserId, out int roomId)) return;

            //从房间中移除
            var room=roomCacahe.dict_RoomList[roomId];
            room.ClientList.Remove(peer);

            //移除映射表
            int temp_playerId;
            roomCacahe.dict_playerRoom.TryRemove(peer.loginUserId, out temp_playerId);

            //销毁房间
            if (room.ClientList.Count == 0)
            {
                RoomBase temp_Room;
                roomCacahe.dict_RoomList.TryRemove(roomId, out temp_Room);
            }
        }
    }
}
