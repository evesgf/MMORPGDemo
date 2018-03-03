using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common.Tools;
using PhotonServerDemo.DLL;

namespace PhotonServerDemo.Handler
{
    public class ChangeCharacterHandler : HandlerBase
    {
        public ChangeCharacterHandler()
        {
            opCode = OperationCode.ChangeCharacter;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            int characterId = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.CharacterId);

            //修改数据
            using (var db = new MySqlContext())
            {
                var user = (from v in db.Game_User
                            where v.User.Id== peer.loginUserId
                            select v).Single();

                var character= (from v in db.Game_Character
                                where v.Id == characterId
                                select v).Single();

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                if (user != null && character!=null)
                {
                    response.ReturnCode = (short)ReturnCode.Success;

                    //修改
                    user.Game_Character = character;
                    db.SaveChanges();

                    //回发Id
                    Dictionary<byte, object> reData = new Dictionary<byte, object>();
                    reData.Add((byte)ParameterCode.CharacterId, characterId);
                    response.SetParameters(reData);
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
