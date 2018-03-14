using LarkFramework.FSM;
using LarkFramework.Procedure;
using LarkFramework.Scenes;
using LarkFramework.UI;
using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedureRoomFight : ProcedureBase
{
    protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        Debug.Log("--------ProcedureRoomFight");
        ScenesManager.Instance.LoadScene(SceneDef.Map_02);
        UIManager.Instance.OpenPage(UIDef.RoomFightPage);
        Debug.Log("ProcedureRoomFight-----------");
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
