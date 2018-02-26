using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPage : UIPage
{

    public InputField input_userName;
    public InputField input_passWord1;
    public InputField input_passWord2;

    public Button btn_Register;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Register.onClick.AddListener(OnRegister);
    }

    public override void Close(object arg = null)
    {
        btn_Register.onClick.RemoveListener(OnRegister);

        base.Close(arg);
    }

    private void OnRegister()
    {

    }
}
