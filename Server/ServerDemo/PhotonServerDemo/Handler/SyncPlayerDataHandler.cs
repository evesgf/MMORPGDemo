using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using LitJson;
using Common.Tools;
using PhotonServerDemo.Cache;

namespace PhotonServerDemo.Handler
{
    class SyncPlayerDataHandler : HandlerBase
    {
        public SyncPlayerDataHandler()
        {
            opCode = OperationCode.SyncPlayerData;
        }

        private RoomCacahe roomCacahe = Caches.roomCacahe;

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionX);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionY);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionZ);

            var roomId = roomCacahe.dict_playerRoom[peer.loginUserId];
            var room= roomCacahe.dict_RoomList[roomId];
            var userData = room.dict_UserData[peer.loginUserId];
            userData.pos.x = x;
            userData.pos.y = y;
            userData.pos.z = z;
            room.dict_UserData[peer.loginUserId] = userData;
        }
    }
}
