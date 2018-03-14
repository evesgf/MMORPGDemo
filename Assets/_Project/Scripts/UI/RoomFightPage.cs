using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFightPage : UIPage
{
    public override void Open(object arg = null)
    {
        base.Open(arg);

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("-------" + GameObject.Find("Map02Mgr").name);
        GameObject.Find("Map02Mgr").GetComponent<Map02Mgr>().LoadOver();
    }
}
