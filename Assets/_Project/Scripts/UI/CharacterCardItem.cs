using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCardItem : MonoBehaviour {

    public int characterId;

    public void Init(int id)
    {
        characterId = id;
    }

    public void OnClick()
    {
        FindObjectOfType<CharacterPage>().ChangeCharacter(characterId);
    }
}
