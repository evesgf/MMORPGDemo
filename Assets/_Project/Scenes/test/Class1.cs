using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Class1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("SceneA");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("test/SceneA");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var a = SceneManager.GetActiveScene();
            Debug.Log(a.name+"_"+a.GetHashCode()+"_"+a.buildIndex+""+a.path);
        }
    }
}
