using Common;
using LarkFramework;
using LarkFramework.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Map02Mgr : MonoBehaviour {

    public Transform team1;
    public Transform team2;

    private Dictionary<int, Transform> dict_userCharacter;

    public void LoadOver()
    {
        //初始化玩家数据
        GetComponent<RoomFightRequest>().OnFightRequest();
    }

    public void SetCharacterPos(int userId, Vector3 pos,Quaternion rot,int ani)
    {

        dict_userCharacter[userId].DOMove(pos,0.2f);

        var ec = dict_userCharacter[userId].GetComponent<EmptyCtrl>();
        ec.SetRot(rot);
        ec.SetAni(ani);
    }

    public void InitPlayerList(List<PlayerData> list)
    {
        bool isTeam1=false;

        foreach (var p in list)
        {
            if (p.userId == SingletonMono<PhotonManager>.Instance.loginUserId)
            {
                //加载玩家控制器
                var obj = Resources.Load("Character/PlayerCtrl");

                GameObject o;
                if (p.userId == 1)
                {
                    isTeam1 = true;
                    o = Instantiate(obj,team1.parent.transform) as GameObject;
                }
                else
                {
                    isTeam1 = false;
                    o = Instantiate(obj,team2.parent.transform) as GameObject;
                }

                o.GetComponent<PlayerCtrl>().LoadCharacter(p.characterId);
            }
        }

        dict_userCharacter = new Dictionary<int, Transform>();
        foreach (var p in list)
        {
            if (p.userId != SingletonMono<PhotonManager>.Instance.loginUserId)
            {
                //加载敌人控制器
                var e = Resources.Load("Character/EmptyCtrl");
                GameObject emptyCtrl;
                if (isTeam1)
                {
                    emptyCtrl = Instantiate(e, team2.parent.transform) as GameObject;
                }
                else
                {

                    emptyCtrl = Instantiate(e, team1.parent.transform) as GameObject;
                }
                var c = Resources.Load("Character2/" + p.characterId);
                var character = Instantiate(c, emptyCtrl.GetComponent<EmptyCtrl>().characterRoot) as GameObject;
                emptyCtrl.GetComponent<EmptyCtrl>().character = character.transform;

                dict_userCharacter.Add(p.userId, emptyCtrl.transform);
            }
        }

    }
}
