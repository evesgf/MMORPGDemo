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
        selectModeBoard.gameObject.SetActive(false);
        matchBoard.gameObject.SetActive(false);


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
    }

    /// <summary>
    /// 多人模式
    /// </summary>
    public void MulitiMode()
    {
        ShowBoard(matchBoard);

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
}
