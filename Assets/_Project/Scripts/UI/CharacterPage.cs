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
}
