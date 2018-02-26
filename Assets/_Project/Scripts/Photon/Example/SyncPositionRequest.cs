using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common;
using UnityEngine;
using LarkFramework;
using LitJson;

public class SyncPositionRequest : RequestBase
{
    [HideInInspector]
    public Vector3 pos;

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        //LitJson不支持Float
        //var p = JsonMapper.ToJson(new Vector3Data() { x = pos.x, y = pos.y, z = pos.z });
        data.Add((byte)ParameterCode.PositionX, pos.x);
        data.Add((byte)ParameterCode.PositionY, pos.y);
        data.Add((byte)ParameterCode.PositionZ, pos.z);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new NotImplementedException();
    }
}