using LarkFramework.Procedure;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : UIPage
{
    public Button btn_Login;
    public Button btn_Register;

    public Button btn_Exit;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Login.onClick.AddListener(OnLogin);
        btn_Register.onClick.AddListener(OnRegister);
        btn_Exit.onClick.AddListener(OnExit);
    }

    public override void Close(object arg = null)
    {
        btn_Login.onClick.RemoveListener(OnLogin);
        btn_Register.onClick.RemoveListener(OnRegister);
        btn_Exit.onClick.RemoveListener(OnExit);

        base.Close(arg);
    }

    private void OnLogin()
    {
        ProcedureManager.Instance.ChangeProcedure<ProcedureHome>();
    }

    private void OnRegister()
    {

    }

    private void OnExit()
    {
        Application.Quit();
    }
}
