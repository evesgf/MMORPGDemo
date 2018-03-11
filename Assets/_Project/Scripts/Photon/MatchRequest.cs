using LarkFramework;
using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common;

public class MatchRequest : RequestBase
{
    public override void DefaultRequest(Action action = null)
    {

    }

    public void RoomRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.MatchRoom, true);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public void MulitiRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.MatchRoom, false);
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, data, true);
    }

    public void StopRequest()
    {
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        
    }
}
