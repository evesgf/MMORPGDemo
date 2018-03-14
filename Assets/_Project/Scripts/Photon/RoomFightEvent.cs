using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;
using UnityEngine;

public class RoomFightEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        byte onFight = (byte)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.RoomFight);

        switch (onFight)
        {
            case OnRoomFight.Enter:
                OnEnter();
                break;

            case OnRoomFight.RDY:
                OnRDY();
                break;

            default:
                break;
        }
    }

    private void OnEnter()
    {
        Debug.Log("-----OnEnter");
    }

    private void OnRDY()
    {
        Debug.Log("-----OnRDY");
    }
}
