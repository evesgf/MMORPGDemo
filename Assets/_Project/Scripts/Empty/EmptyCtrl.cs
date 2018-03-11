using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCtrl : MonoBehaviour {

    public Transform attackPos;
    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
        gameObject.layer = layerMask;
	}
}
