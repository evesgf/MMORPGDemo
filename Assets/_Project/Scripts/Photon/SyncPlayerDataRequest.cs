using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
using Common;
using LarkFramework;

public class SyncPlayerDataRequest : RequestBase
{
    [HideInInspector]
    public Vector3 pos;

    [HideInInspector]
    public Quaternion rot;

    [HideInInspector]
    public int animationKey;

    public override void DefaultRequest(Action action = null)
    {

    }

    public void PositionRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        //LitJson不支持Float
        //var p = JsonMapper.ToJson(new Vector3Data() { x = pos.x, y = pos.y, z = pos.z });
        data.Add((byte)ParameterCode.PositionX, (double)pos.x);
        data.Add((byte)ParameterCode.PositionY, (double)pos.y);
        data.Add((byte)ParameterCode.PositionZ, (double)pos.z);

        data.Add((byte)ParameterCode.RotationX,(double)rot.x);
        data.Add((byte)ParameterCode.RotationY, (double)rot.y);
        data.Add((byte)ParameterCode.RotationZ, (double)rot.z);
        data.Add((byte)ParameterCode.RotationW, (double)rot.w);

        data.Add((byte)ParameterCode.Animation, animationKey);

        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {

    }
}
