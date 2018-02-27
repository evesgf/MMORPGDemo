using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashPage : UIPage
{
    public int waitTime = 2000;

    public override void Open(object arg = null)
    {
        base.Open(arg);

        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(waitTime);
        Close(2f);
        yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene();
    }
}
