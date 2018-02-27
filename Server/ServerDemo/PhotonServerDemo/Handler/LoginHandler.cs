using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using PhotonServerDemo.DLL;

namespace PhotonServerDemo.Handler
{
    public class LoginHandler : HandlerBase
    {
        public LoginHandler()
        {
            opCode = OperationCode.Login;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            string userName = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.UserName) as string;
            string passWord = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.passWord) as string;

            using (var db = new MySqlContext())
            {
                var user = (from v in db.Sys_User
                            where v.userName == userName && v.passWord==passWord
                            select v).Single();

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                if (user != null)
                {
                    response.ReturnCode = (short)ReturnCode.Success;
                    peer.loginUserName = userName;
                }
                else
                {
                    response.ReturnCode = (short)ReturnCode.Failed;
                }
                peer.SendOperationResponse(response, sendParameters);
            }
        }
    }
}
