using Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonServerDemo.Handler
{
    public abstract class HandlerBase
    {
        public OperationCode opCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer);
    }
}
