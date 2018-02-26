using Common;
using LarkFramework;
using LarkFramework.Net;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PE_LoginPage : UIPage
{
    public InputField input_userName;
    public InputField input_passWord;

    public Button btn_Login;
    public Button btn_Register;

    private LoginRequest LoginRequest;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        btn_Login.onClick.AddListener(OnLogin);
        btn_Register.onClick.AddListener(OnRegister);

        LoginRequest = GetComponent<LoginRequest>();
    }

    public override void Close(object arg = null)
    {
        btn_Login.onClick.RemoveListener(OnLogin);
        btn_Register.onClick.RemoveListener(OnRegister);

        base.Close(arg);
    }

    private void OnLogin()
    {
        LoginRequest.userName = input_userName.text;
        LoginRequest.passWord = input_passWord.text;
        LoginRequest.DefaultRequest();
    }

    private void OnRegister()
    {

    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            Close();
            //UIManager.Instance.OpenPage("PE_HomePage");
            SceneManager.LoadScene("Photon_Example2");
        }
    }
}
