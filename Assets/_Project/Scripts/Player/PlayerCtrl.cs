using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    public Transform character;

    public float speed_walk;
    public float speed_run;

    public Transform tragetRang;

    private AnimCtrl animCtrl;

    private bool isMove;
    private bool isRun;

    private List<Transform> list_Empty = new List<Transform>();
    private Transform nowTarget;

	// Use this for initialization
	void Start () {
        animCtrl = character.GetComponent<AnimCtrl>();

        foreach (var item in FindObjectsOfType<EmptyCtrl>())
        {
            list_Empty.Add(item.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
        #region KeyBoard Controller
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchRun();
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        IsMove(h, v);
        RotateCharacter(h,v);

        float speed = isRun ? speed_run : speed_walk;
        transform.Translate(new Vector3(h, transform.position.y, v) * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectTarget();
            UseSkill();
        }
        #endregion

        ShowTarget();
    }

    private void UseSkill()
    {
        if (nowTarget != null)
        {
            GetComponent<ShootSkill>().Shoot(nowTarget.GetComponent<EmptyCtrl>().attackPos);
        }
    }

    private void SelectTarget()
    {
        if (list_Empty.Count == 0) nowTarget = null;

        float temp_minDis = 0;
        for (int i=0;i<list_Empty.Count;i++)
        {
            var dis = Vector3.Distance(list_Empty[i].position, transform.position);

            if (i == 0)
            {
                temp_minDis = dis;
                nowTarget = list_Empty[i];
                continue;
            }

            if (dis < temp_minDis)
            {
                temp_minDis = dis;
                nowTarget = list_Empty[i];
            }
        }

    }

    private void ShowTarget()
    {
        if (nowTarget != null)
        {
            tragetRang.position = nowTarget.position;
        }
    }

    private void Attack1()
    {
        //获取目标Id
        //向服务器发起攻击请求
    }

    /// <summary>
    /// 走跑切换
    /// </summary>
    private void SwitchRun()
    {
        isRun = !isRun;
    }

    /// <summary>
    /// 根据输入数据旋转角色
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    private void RotateCharacter(float h,float v)
    {
        //设置角色的朝向（朝向当前坐标+摇杆偏移量）
        character.transform.LookAt(new Vector3(transform.position.x + h, transform.position.y, transform.position.z + v));
    }

    /// <summary>
    /// 根据输入数据切换移动状态
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    private void IsMove(float h, float v)
    {
        isMove = (h != 0 || v != 0);
        if (isMove)
        {
            if (isRun)
            {
                animCtrl.Run();
            }
            else
            {
                animCtrl.Walk();
            }
        }
        else
        {
            animCtrl.Idle();
        }
    }
}
