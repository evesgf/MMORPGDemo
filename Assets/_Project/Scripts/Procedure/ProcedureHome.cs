using LarkFramework.FSM;
using LarkFramework.Procedure;
using LarkFramework.Scenes;
using LarkFramework.UI;
using Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ProcedureHome: ProcedureBase
{
    protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        ScenesManager.Instance.LoadScene(SceneDef.HomeScene);
        UIManager.Instance.OpenPage(UIDef.HomePage);
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
