using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;

namespace PhotonServerDemo.Handler
{
    class DefaultHandler : HandlerBase
    {
        public DefaultHandler()
        {
            opCode = OperationCode.Default;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            throw new NotImplementedException();
        }
    }
}
