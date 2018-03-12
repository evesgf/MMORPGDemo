using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using LarkFramework;
using Common;
using Common.Tools;
using System;
using LarkFramework.UI;

public class GameUserRequest : RequestBase
{
    public override void DefaultRequest(Action action = null)
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.UserId, SingletonMono<PhotonManager>.Instance.loginUserId);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        Debuger.Log(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            //获取参数
            var reJsonStr = DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.GameUser) as string;
            SingletonMono<PhotonManager>.Instance.gameUserData = LitJson.JsonMapper.ToObject<GameUserData>(reJsonStr);
            GetComponent<HomePage>().Init();
        }
        else
        {
            UIManager.Instance.OpenWindow<DialogWindow>("GameUser Request Error");
        }
    }
}
