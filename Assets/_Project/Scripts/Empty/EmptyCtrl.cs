using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EmptyCtrl : MonoBehaviour {

    public Transform characterRoot;
    public Transform character;
    public Transform attackPos;
    public LayerMask layerMask;

    public Text info;

    private float h;
    private float v;

	// Use this for initialization
	void Start () {
        gameObject.layer = layerMask;
	}

    public void SetRot(Quaternion q)
    {
        character.DORotateQuaternion(q, 0.2f);
    }

    public void SetAni(int value)
    {
        character.GetComponent<AnimCtrl>().SetAniKey(value);
    }

}
