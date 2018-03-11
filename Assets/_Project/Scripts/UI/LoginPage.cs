using LarkFramework.Procedure;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Common;
using LarkFramework;
using LarkFramework.Net;

public class LoginPage : UIPage
{
    public InputField input_UserName;
    public InputField input_PassWord;

    public Button btn_Login;
    public Button btn_Register;

    public Button btn_Exit;

    private LoginRequest loginRequest;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Login.onClick.AddListener(OnLogin);
        btn_Register.onClick.AddListener(OnRegister);
        btn_Exit.onClick.AddListener(OnExit);

        loginRequest = GetComponent<LoginRequest>();
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
        loginRequest.userName = input_UserName.text;
        loginRequest.passWord = input_PassWord.text;
        loginRequest.DefaultRequest(()=> { GetComponent<LoginPage>().OnLoginResponse(ReturnCode.Success); });
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            Close();
            ProcedureManager.Instance.ChangeProcedure<ProcedureHome>();
        }
    }

    private void OnRegister()
    {

    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {

        }
    }

    private void OnExit()
    {
        Application.Quit();
    }
}
