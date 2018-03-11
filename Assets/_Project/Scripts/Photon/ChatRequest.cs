using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common;
using LarkFramework;
using UnityEngine;

public class ChatRequest : RequestBase
{
    [HideInInspector]
    public string chat;

    public override void DefaultRequest(Action action = null)
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.ChatInfo, chat);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }
}
