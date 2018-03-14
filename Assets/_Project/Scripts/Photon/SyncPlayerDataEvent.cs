using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;

public class SyncPlayerDataEvent : EventBase
{
    private bool isFirstSync=true;

    public void Init()
    {
        isFirstSync = true;
    }

    public override void OnEvent(EventData eventData)
    {
        string str = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerDataList);
        List<PlayerData> list = LitJson.JsonMapper.ToObject<List<PlayerData>>(str);

        if (isFirstSync)
        {
            GetComponent<Map02Mgr>().InitPlayerList(list);
            isFirstSync = false;
        }

        foreach (var p in list)
        {
            if (p.userId == PhotonManager.Instance.loginUserId) continue;

            if (p.pos != null)
            {
                GetComponent<Map02Mgr>().SetCharacterPos(p.userId, new Vector3((float)p.pos.x, (float)p.pos.y, (float)p.pos.z));
            }

        }
    }
}
