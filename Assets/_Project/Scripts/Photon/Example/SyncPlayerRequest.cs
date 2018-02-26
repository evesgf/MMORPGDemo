using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using LarkFramework;
using Common.Tools;
using Common;
using LitJson;

public class SyncPlayerRequest : RequestBase
{
    private PE_Player player;

    public override void Start()
    {
        base.Start();
        player = GetComponent<PE_Player>();
    }

    public override void DefaultRequest()
    {
        SingletonMono<PhotonManager>.Instance.Peer.OpCustom((byte)opCode, null, true);
    }

    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        var restr=(string)DictTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.OnlineUserNameList);
        List<string> onlineUserNameList = JsonMapper.ToObject<List<string>>(restr);
        player.OnSyncPlayerResponse(onlineUserNameList);
    }
}
