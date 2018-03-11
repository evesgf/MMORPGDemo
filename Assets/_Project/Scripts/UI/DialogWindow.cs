using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LarkFramework.UI;
using UnityEngine.UI;

public class DialogWindow : UIWindow
{
    public Text text;

    public override void Open(object arg = null)
    {
        base.Open(arg);
        text.text = arg as string;
    }

    public override void Close(object arg = null)
    {
        base.Close(arg);
    }
}
