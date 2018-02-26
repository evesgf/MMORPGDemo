using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;
using LitJson;

public class SyncPositionEvent : EventBase
{
    private PE_Player player;

    public override void Start()
    {
        base.Start();
        player = GetComponent<PE_Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        string playerDataListStr = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerDataList);
        List<PlayerData> playerDataList = JsonMapper.ToObject<List<PlayerData>>(playerDataListStr);
        player.OnSyncPositionEvent(playerDataList);
    }
}
