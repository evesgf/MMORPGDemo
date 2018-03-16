using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour {

    public Transform characterRoot;
    public Transform character;

    public float speed_walk;
    public float speed_run;

    public Transform tragetRang;

    public Text info;

    private AnimCtrl animCtrl;

    private bool isMove;
    private bool isRun;

    private List<Transform> list_Empty = new List<Transform>();
    private Transform nowTarget;

    private float h;
    private float v;

    // Use this for initialization
    void Start () {

        syncPlayerDataRequest = GetComponent<SyncPlayerDataRequest>();

        foreach (var item in FindObjectsOfType<EmptyCtrl>())
        {
            list_Empty.Add(item.transform);
        }

        lastPosition = transform.position;
        syncPlayerDataRequest.pos = transform.position;
        syncPlayerDataRequest.rot = character.rotation;
        syncPlayerDataRequest.animationKey = animCtrl.GetAniKey();
        syncPlayerDataRequest.PositionRequest();

        InvokeRepeating("OnSyncPostition", 1, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
        #region KeyBoard Controller
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchRun();
        }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        info.text = "h:" + h + " v:" + v;

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

    private SyncPlayerDataRequest syncPlayerDataRequest;
    private Vector3 lastPosition = Vector3.zero;
    /// <summary>
    /// 同步位置
    /// </summary>
    void OnSyncPostition()
    {
        //if (Vector3.Distance(transform.position, lastPosition) > 0.1f)
        //{
        //    lastPosition = transform.position;
        //    syncPlayerDataRequest.pos = transform.position;
        //    syncPlayerDataRequest.rotation = character.rotation;
        //    syncPlayerDataRequest.animationKey = animCtrl.GetAniKey();
        //    syncPlayerDataRequest.PositionRequest();
        //}

        lastPosition = transform.position;
        syncPlayerDataRequest.pos = transform.position;
        syncPlayerDataRequest.rot = character.rotation;
        syncPlayerDataRequest.animationKey = animCtrl.GetAniKey();
        syncPlayerDataRequest.PositionRequest();
    }

    public void LoadCharacter(int index)
    {
        var c = Resources.Load("Character2/" + index);
        var ch = Instantiate(c, characterRoot) as GameObject;
        character = ch.transform;
        animCtrl = character.GetComponent<AnimCtrl>();

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
