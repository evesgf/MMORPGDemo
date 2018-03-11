using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BulletBase : MonoBehaviour {

    public float speed;
    public float life;

    public float minDis;

    public GameObject muzzle;
    public ParticleSystem ps;

    private Vector3 vector;
    private Transform target;

    public void Init(Transform initPos,Transform t)
    {
        muzzle.SetActive(true);

        transform.position = initPos.position;
        transform.LookAt(t);

        vector = t.position - transform.position;
        target = t;

        Debug.DrawLine(transform.position, t.position, Color.red, life);

        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {

        for (float i= life;i>0;i-=Time.deltaTime)
        {


            if (Vector3.Distance(transform.position, target.position) > minDis)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return i;
            }
            else
            {
                break;
            }
        }
        EndEffect();
    }

    private void EndEffect()
    {
        muzzle.SetActive(false);
        ps.Play();
        Destroy(gameObject,0.6f);
    }
}
