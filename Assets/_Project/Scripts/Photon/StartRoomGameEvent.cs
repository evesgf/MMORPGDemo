using Common.Tools;
using ExitGames.Client.Photon;
using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomGameEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        Debug.Log("准备开始RoomGame");
    }
}
