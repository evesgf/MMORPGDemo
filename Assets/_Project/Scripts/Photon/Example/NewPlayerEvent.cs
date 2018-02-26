using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;

public class NewPlayerEvent : EventBase
{
    private PE_Player player;

    public override void Start()
    {
        base.Start();
        player = GetComponent<PE_Player>();
    }

    public override void OnEvent(EventData eventData)
    {
        string userName = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.UserName);
        player.OnNewPlaterEvent(userName);
    }
}
