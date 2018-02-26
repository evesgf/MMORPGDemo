using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListPage : UIPage
{
    public Button btn_History;
    public Button btn_Save;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_History.onClick.AddListener(OnPlay);
        btn_Save.onClick.AddListener(OnSave);
    }

    public override void Close(object arg = null)
    {
        btn_History.onClick.RemoveListener(OnPlay);
        btn_Save.onClick.RemoveListener(OnSave);

        base.Close(arg);
    }

    private void OnPlay()
    {
        UIManager.Instance.OpenPage(UIDef.ClassListPage);
    }

    private void OnSave()
    {
        UIManager.Instance.OpenWindow<DialogWindow>();
    }
}
