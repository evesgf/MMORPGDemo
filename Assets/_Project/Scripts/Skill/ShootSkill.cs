using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSkill : MonoBehaviour {

    public GameObject bullet;
    public Transform instancePos;

    public void Shoot(Transform target)
    {
        var b = Instantiate(bullet);
        b.GetComponent<BulletBase>().Init(instancePos, target);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
