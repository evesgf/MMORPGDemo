using Common;
using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PE_Player : MonoBehaviour {

    public string userName;
    public GameObject playerPrefab;

    public GameObject player;

    //public Text txtName;
    public float speed = 10f;

    public bool isLocalPlayer = true;

    private SyncPositionRequest syncPositionRequest;
    private SyncPlayerRequest syncPlayerRequest;

    private Vector3 lastPosition = Vector3.zero;

    private Dictionary<string, GameObject> dict_Player = new Dictionary<string, GameObject>();

	// Use this for initialization
	void Start () {
        foreach (var item in player.GetComponentsInChildren<Renderer>())
        {
            item.material.color = Color.green;
        }
        player.GetComponentInChildren<Text>().text = userName;

        syncPositionRequest = GetComponent<SyncPositionRequest>();
        syncPlayerRequest = GetComponent<SyncPlayerRequest>();
        syncPlayerRequest.DefaultRequest();

        InvokeRepeating("OnSyncPosition", 1, 0.2f);
    }

    //public void SetName(string userName)
    //{
    //    txtName.text = userName;
    //}

    /// <summary>
    /// 同步位置
    /// </summary>
    void OnSyncPosition()
    {
        if (Vector3.Distance(player.transform.position, lastPosition) > 0.1f)
        {
            lastPosition = player.transform.position;
            syncPositionRequest.pos = player.transform.position;
            syncPositionRequest.DefaultRequest();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        player.transform.Translate(new Vector3(h, transform.position.y, v) * Time.deltaTime * speed);
    }

    public void OnSyncPlayerResponse(List<string> onlineUserNameList)
    {
        //创建其他客户端
        foreach (var user in onlineUserNameList)
        {
            OnNewPlaterEvent(user);
        }
    }

    public void OnNewPlaterEvent(string user)
    {
        var go = Instantiate(playerPrefab);

        go.GetComponentInChildren<Text>().text = user;
        Debug.Log("OnNewPlaterEvent:" + user);
        dict_Player.Add(user, go);
    }

    public void OnSyncPositionEvent(List<PlayerData> playerDataList)
    {
        foreach (var pd in playerDataList)
        {
            GameObject go =DictTool.GetValue<string, GameObject>(dict_Player,pd.userName);
            if (go != null)
            {
                go.transform.position = Vector3.Lerp(go.transform.position,new Vector3((float)pd.pos.x, (float)pd.pos.y, (float)pd.pos.z),0.2f);
            }
        }
    }
}
