using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPage : UIPage
{
    public override void Open(object arg = null)
    {
        base.Open(arg);

    }

    private void Init()
    {

    }

    public void ChangeCharacter(int id)
    {
        GetComponent<ChangeCharacterRequest>().characterId = id;
        GetComponent<ChangeCharacterRequest>().DefaultRequest();
    }

    public void ChangeAccess(int id)
    {
        var root = GameObject.Find("CharacterRoot").transform;

        for (int i = 0; i < root.childCount; i++)
        {
            Destroy(root.GetChild(i).gameObject);
        }


        var obj = Resources.Load("Character/" + id);
        var o = Instantiate(obj, root);
    }
}
