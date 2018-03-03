using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;
using LarkFramework;
using Common;
using Common.Tools;

public class ChangeCharacterRequest : RequestBase
{
    [HideInInspector]
    public int characterId;

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.CharacterId, characterId);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        ReturnCode returnCode = (ReturnCode)operationResponse.ReturnCode;
        Debuger.Log(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            //获取参数
            var characterId = (int)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.CharacterId);
            Debug.Log("characterId:"+ characterId);

        }
        else
        {
            Debug.LogError("LoginError");
        }
    }
}
