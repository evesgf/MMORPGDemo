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
                            where v.User.Id == userId
                            select v).Single();

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                if (gameUser != null)
                {
                    var gameUserData = new GameUserData();
                    gameUserData.Id = gameUser.Id;
                    gameUserData.User_Id = userId;
                    gameUserData.NickName = gameUser.NickName;
                    gameUserData.HeadImgPath = gameUser.HeadImgPath;
                    gameUserData.Level = gameUser.Level;
                    gameUserData.EXP = gameUser.EXP;

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
