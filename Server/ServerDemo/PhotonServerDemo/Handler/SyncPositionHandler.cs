using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using LitJson;

namespace PhotonServerDemo.Handler
{
    class SyncPositionHandler : HandlerBase
    {
        public SyncPositionHandler()
        {
            opCode = OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionX);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionY);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.PositionZ);

            peer.x = x; peer.y = y; peer.z = z;
        }
    }
}
