using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LarkFramework;
using LarkFramework.Net;
using LarkFramework.UI;

public class Photon_Example : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        UIManager.Instance.OpenPage("PE_LoginPage");
    }
}

