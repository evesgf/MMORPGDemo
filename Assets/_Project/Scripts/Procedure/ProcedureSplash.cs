using LarkFramework.FSM;
using LarkFramework.Procedure;
using LarkFramework.Scenes;
using LarkFramework.UI;
using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureSplash : ProcedureBase
{
    public float waitTime=2f;
    private float nowWaitTime;

    protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        UIManager.Instance.OpenPage(UIDef.SplashPage);
    }

    protected internal override void OnInit(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);

        nowWaitTime = 0;
    }

    protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        nowWaitTime += elapseSeconds;
        if (nowWaitTime > waitTime)
        {
            ProcedureManager.Instance.ChangeProcedure<ProcedureLogin>();
        }
    }

    protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
    }

    protected internal override void OnDestroy(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnDestroy(procedureOwner);
    }
}
