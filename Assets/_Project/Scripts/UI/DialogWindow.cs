using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LarkFramework.UI;
using UnityEngine.UI;

public class DialogWindow : UIWindow
{
    public Button btn_Save;
    public Button btn_Canel;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Save.onClick.AddListener(OnSave);
    }

    public override void Close(object arg = null)
    {
        base.Close(arg);

        btn_Save.onClick.RemoveListener(OnSave);
    }

    private void OnSave()
    {
        Close();
    }
}
