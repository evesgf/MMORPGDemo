using Common;
using LarkFramework.Net;
using LarkFramework.Procedure;
using LarkFramework.Tick;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPage : UIPage
{
    public GameObject selectModeBoard;
    public GameObject matchBoard;
    public GameObject playBoard;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        ShowBoard(selectModeBoard);
    }

    public override void Close(object arg = null)
    {
        foreach (var item in selectModeBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }
        foreach (var item in matchBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }
        foreach (var item in playBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }

        base.Close(arg);
    }

    private void ShowBoard(GameObject root)
    {

        foreach (var item in selectModeBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }
        foreach (var item in matchBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }
        foreach (var item in playBoard.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveOut();
        }

        selectModeBoard.gameObject.SetActive(false);
        matchBoard.gameObject.SetActive(false);
        playBoard.gameObject.SetActive(false);

        root.gameObject.SetActive(true);
        foreach (var item in root.GetComponentsInChildren<GUIAnim>())
        {
            item.MoveIn();
        }
    }

    /// <summary>
    /// 单人模式
    /// </summary>
    public void SoloMode()
    {
        ShowBoard(matchBoard);
        StartMatch();

    }

    /// <summary>
    /// 房间模式
    /// </summary>
    public void RoomMode()
    {
        ShowBoard(matchBoard);
        StartMatch();
        SwitchCamPos(1);
        GetComponent<MatchRequest>().RoomRequest();
    }

    /// <summary>
    /// 多人模式
    /// </summary>
    public void MulitiMode()
    {
        ShowBoard(matchBoard);
        StartMatch();
        GetComponent<MatchRequest>().MulitiRequest();
    }

    #region 多人模式
    /// <summary>
    /// 匹配时间
    /// </summary>
    [SerializeField]
    private Text txtTime;
    /// <summary>
    /// 是否开始计时
    /// </summary>
    private bool start = false;

    private float minute = 0;
    private float second = 0;

    private void StartMatch()
    {
        txtTime.text = "00:01";
        minute = 0;
        second = 0;
        start = true;

        TickManager.Instance.m_TickComponent.onUpdate += OnUpdate;
    }

    public void StopMatch()
    {
        TickManager.Instance.m_TickComponent.onUpdate -= OnUpdate;

        ShowBoard(selectModeBoard);
        start = false;
        GetComponent<MatchRequest>().StopRequest();
        SwitchCamPos(2);

    }

    #endregion

    public void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        if (start)
        {
            second += elapseSeconds;
            if (second >= 60)
            {
                minute++;
                second = 0;
            }
            txtTime.text = minute.ToString().PadLeft(2, '0') + ":" + second.ToString("00");
        }
    }

    private void SwitchCamPos(int index)
    {
        var pos1 = GameObject.Find("CamPos1").transform;
        var pos2 = GameObject.Find("CamPos2").transform;
        var cam = GameObject.Find("Main Camera").transform;

        if (index == 1)
        {
            cam.position = pos2.position;
            cam.rotation = pos2.rotation;
        }
        else
        {
            cam.position = pos1.position;
            cam.rotation = pos1.rotation;
        }
    }

    public void RefshRoomUsers(List<PlayerData> list)
    {
        foreach (var p in list)
        {
            if (p.userId != PhotonManager.Instance.loginUserId)
            {
                var root = GameObject.Find("CharacterRoot2").transform;

                for (int i = 0; i < root.childCount; i++)
                {
                    Destroy(root.GetChild(i).gameObject);
                }

                var obj = Resources.Load("Character/" + p.characterId);
                var o = Instantiate(obj, root) as GameObject;
                if (o.GetComponent<PlayerCtrl>() != null)
                {
                    o.GetComponent<PlayerCtrl>().enabled = false;
                }
            }
        }
    }

    public void OnMatchOK()
    {
        ShowBoard(playBoard);
    }

    public void OnPlayBoard()
    {
        //开始游戏
        ProcedureManager.Instance.ChangeProcedure<ProcedureRoomFight>();
    }

    public void OnCancelPlayBoard()
    {
        //取消
        GetComponent<MatchRequest>().StopRequest();
        SwitchCamPos(2);
        ShowBoard(selectModeBoard);
    }

    private void OnDestroy()
    {
        //取消
        GetComponent<MatchRequest>().StopRequest();
    }
}
