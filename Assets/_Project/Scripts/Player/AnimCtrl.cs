using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string aniKey="ani";

    [SerializeField]
    private int idle = 0;
    [SerializeField]
    private int walk = 1;
    [SerializeField]
    private int run = 2;

    [SerializeField]
    private int attack1 = 11;
    [SerializeField]
    private int attack2 = 12;

    [SerializeField]
    private int skill1 = 21;
    [SerializeField]
    private int skill2 = 22;
    [SerializeField]
    private int skill3 = 23;
    [SerializeField]
    private int skill4 = 24;

    public int GetAniKey()
    {
        return animator.GetInteger(aniKey);
    }
    public void SetAniKey(int key)
    {
        animator.SetInteger(aniKey, key);
    }


    public void Idle()
    {
        animator.SetInteger(aniKey, idle);
    }
    public void Walk()
    {
        animator.SetInteger(aniKey, walk);
    }
    public void Run()
    {
        animator.SetInteger(aniKey, run);
    }

    public void Attack1()
    {
        animator.SetInteger(aniKey, attack1);
    }

    public void Attack2()
    {
        animator.SetInteger(aniKey, attack1);
    }

    public void Skill1()
    {
        animator.SetInteger(aniKey, skill1);
    }

    public void Skill2()
    {
        animator.SetInteger(aniKey, skill2);
    }

    public void Skill3()
    {
        animator.SetInteger(aniKey, skill3);
    }

    public void Skill4()
    {
        animator.SetInteger(aniKey, skill4);
    }
}
