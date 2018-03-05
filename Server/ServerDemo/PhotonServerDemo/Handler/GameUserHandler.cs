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
    public class GameUserHandler : HandlerBase
    {
        public GameUserHandler()
        {
            opCode = OperationCode.GameUser;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            int userId = (int)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.UserId);

            using (var db = new MySqlContext())
            {
                var gameUser = (from v in db.Game_User
                         join c in db.Sys_User
                         on v.User.Id equals c.Id
                         where v.Id == userId
                          select v).FirstOrDefault();

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                if (gameUser != null)
                {
                    var character = (from v in db.Game_Character
                                     join c in db.Game_User
                                     on v.Id equals c.Game_Character.Id
                                     where c.Id == gameUser.Id
                                     select v).FirstOrDefault();

                    var gameUserData = new GameUserData();
                    gameUserData.Id = gameUser.Id;
                    gameUserData.User_Id = userId;
                    gameUserData.NickName = gameUser.NickName;
                    gameUserData.HeadImgPath = gameUser.HeadImgPath;
                    gameUserData.Level = gameUser.Level;
                    gameUserData.EXP = gameUser.EXP;
                    gameUserData.GameCharacterId = character.Id;

                    string reJosnStr = LitJson.JsonMapper.ToJson(gameUserData);

                    //回发Id
                    Dictionary<byte, object> userData = new Dictionary<byte, object>();
                    userData.Add((byte)ParameterCode.GameUser, reJosnStr);
                    response.SetParameters(userData);
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
