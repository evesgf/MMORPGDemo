using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
using Common;
using LarkFramework;

public class RoomFightRequest : RequestBase
{
    public override void DefaultRequest(Action action = null)
    {
        throw new NotImplementedException();
    }

    public void OnFightRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.RoomFight, OnRoomFight.Enter);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public void OnGameOverRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.RoomFight, OnRoomFight.GameOver);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }
}
