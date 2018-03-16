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
            double x = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionX);
            double y = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionY);
            double z = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionZ);

            double rx = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.RotationX);
            double ry = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.RotationY);
            double rz = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.RotationZ);
            double rw = (double)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.RotationW);

            int ani = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Animation);

            var roomId = roomCacahe.dict_playerRoom[peer.loginUserId];
            var room= roomCacahe.dict_RoomList[roomId];
            var userData = room.dict_UserData[peer.loginUserId];
            userData.pos.x = x;
            userData.pos.y = y;
            userData.pos.z = z;

            userData.rotation.x = rx;
            userData.rotation.y = ry;
            userData.rotation.z = rz;
            userData.rotation.w = rw;

            userData.animation = ani;

            room.dict_UserData[peer.loginUserId] = userData;
        }
    }
}
