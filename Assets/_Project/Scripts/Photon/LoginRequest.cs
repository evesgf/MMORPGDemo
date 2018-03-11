using Common;
using ExitGames.Client.Photon;
using LarkFramework.Net;
using System.Collections.Generic;
using LarkFramework;
using UnityEngine;
using LarkFramework.Procedure;
using Common.Tools;
using System;

public class LoginRequest : RequestBase
{
    [HideInInspector]
    public string userName;
    [HideInInspector]
    public string passWord;
    [HideInInspector]
    public bool isDebugLogin;

    private Action act;

    public override void DefaultRequest(Action action = null)
    {
        act = action;

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
            //获取参数
            var userId = (int)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UserId);
            SingletonMono<PhotonManager>.Instance.loginUserId = userId;
            Debug.Log("当前登录用户Id:"+userId);

            act.Invoke();
        }
        else
        {
            Debug.LogError("LoginError");
        }
    }
}
