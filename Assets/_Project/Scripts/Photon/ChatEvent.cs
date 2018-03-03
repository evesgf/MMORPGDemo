using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;

public class ChatEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        string chatInfo = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.ChatInfo);
        GetComponent<HomePage>().RefreshChatBox(chatInfo);
    }
}
