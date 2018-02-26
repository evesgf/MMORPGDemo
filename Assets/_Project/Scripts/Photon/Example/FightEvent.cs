using LarkFramework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;

class FightEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        //获取参数
        //服务器通知我我放了个技能
        //服务器通知我我被打了
        string userName = (string)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.UserName);
        switch (userName)
        {
            case "OnSkill":
                OnSkill();
                break;

            case "OnDamage":
                OnDamage();
                break;
        }
    }

    private void OnSkill()
    {

    }


    public void OnDamage()
    {

    }
}
