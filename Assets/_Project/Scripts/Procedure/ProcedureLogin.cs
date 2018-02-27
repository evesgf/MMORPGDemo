using LarkFramework.FSM;
using LarkFramework.Procedure;
using LarkFramework.Scenes;
using LarkFramework.UI;
using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureLogin : ProcedureBase
{
    protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        ScenesManager.Instance.LoadScene(SceneDef.LoginScene);
        UIManager.Instance.OpenPage(UIDef.LoginPage);
    }

    protected internal override void OnInit(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);
    }

    protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
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
