using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : UIPage
{
    public Button btn_Play;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Play.onClick.AddListener(OnPlay);
    }

    public override void Close(object arg = null)
    {
        btn_Play.onClick.RemoveListener(OnPlay);

        base.Close(arg);
    }

    private void OnPlay()
    {
        UIManager.Instance.OpenPage(UIDef.ListPage);
    }
}
