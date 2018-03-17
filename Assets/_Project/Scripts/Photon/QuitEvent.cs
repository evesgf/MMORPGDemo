using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using LarkFramework.Net;
using LarkFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitEvent : EventBase
{
    public override void OnEvent(EventData eventData)
    {
        int user = (int)DictTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.UserId);

        Debug.Log(user + " 他退了");

        var math = FindObjectOfType<MatchPage>();
        if (math != null)
        {
            math.StopMatch();
        }

        var mgr = GetComponent<Map02Mgr>();
        if (mgr != null)
        {
            StartCoroutine(ExitFight());
        }
    }

    IEnumerator ExitFight()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(3);
        ProcedureManager.Instance.ChangeProcedure<ProcedureHome>();
    }
}
