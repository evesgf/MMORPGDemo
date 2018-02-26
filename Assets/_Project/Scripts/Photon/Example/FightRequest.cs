using ExitGames.Client.Photon;
using LarkFramework.Net;
using System;

public class FightRequest : RequestBase
{
    public override void DefaultRequest()
    {
        //通知服务器我放了技能&攻击
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }

}
