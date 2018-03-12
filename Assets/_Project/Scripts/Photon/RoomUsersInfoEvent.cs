using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUsersInfoEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        string playerListStr = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerDataList);

        GetComponent<MatchPage>().RefshRoomUsers(LitJson.JsonMapper.ToObject<List<PlayerData>>(playerListStr));

        Debug.Log("刷新房间玩家");
    }
}
