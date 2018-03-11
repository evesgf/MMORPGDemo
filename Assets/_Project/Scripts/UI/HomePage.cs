using LarkFramework;
using LarkFramework.Net;
using LarkFramework.Procedure;
using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : UIPage
{
    public Text nickName;
    public Text userLevel;
    public Text exp;
    public Slider expSlider;

    public override void Open(object arg = null)
    {
        base.Open(arg);
        StartCoroutine(test());

        InitChat();
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<GameUserRequest>().DefaultRequest();
    }

    public void Init()
    {
        var gameUserData = SingletonMono<PhotonManager>.Instance.gameUserData;
        nickName.text = gameUserData.NickName;
        userLevel.text = gameUserData.Level.ToString();
        exp.text = gameUserData.EXP.ToString();
        expSlider.value = (float)gameUserData.EXP / 100;

        ShowCharacter(gameUserData.GameCharacterId);
    }

    private void ShowCharacter(int index)
    {
        var root = GameObject.Find("CharacterRoot").transform;

        for (int i = 0; i < root.childCount; i++)
        {
            Destroy(root.GetChild(i).gameObject);
        }
        

        var obj=Resources.Load("Character/"+index);
        var o = Instantiate(obj, root) as GameObject;
        if (o.GetComponent<PlayerCtrl>() != null)
        {
            o.GetComponent<PlayerCtrl>().enabled = false;
        }
    }


    public override void Close(object arg = null)
    {
        base.Close(arg);
    }

    public void SwitchCharacter()
    {
        UIManager.Instance.OpenPage(UIDef.CharacterPage);
    }

    public void Play()
    {
        ProcedureManager.Instance.ChangeProcedure<ProcedureMatch>();
    }

    public void Exit()
    {
        Application.Quit();
    }

    #region Chat
    public Button btn_SendChatBox;
    public InputField txt_SendContent;
    public Text txt_ChatBox;

    public void InitChat()
    {
        txt_SendContent.text = "";
        txt_ChatBox.text = "";
    }

    public void SendChat()
    {
        GetComponent<ChatRequest>().chat = nickName.text+":"+txt_SendContent.text;
        GetComponent<ChatRequest>().DefaultRequest();
    }

    public void RefreshChatBox(string chatInfo)
    {
        txt_ChatBox.text += "\n" + chatInfo;
    }
    #endregion
}
