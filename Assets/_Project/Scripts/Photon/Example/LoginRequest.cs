using Common;
using ExitGames.Client.Photon;
using LarkFramework.Net;
using System.Collections.Generic;
using LarkFramework;
using UnityEngine;

public class LoginRequest : RequestBase
{
    [HideInInspector]
    public string userName;
    [HideInInspector]
    public string passWord;

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.UserName, userName);
        data.Add((byte)ParameterCode.passWord, passWord);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        Debuger.Log(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            SingletonMono<PhotonManager>.Instance.loginUserName = userName;
        }
        GetComponent<PE_LoginPage>().OnLoginResponse(returnCode);
    }
}
