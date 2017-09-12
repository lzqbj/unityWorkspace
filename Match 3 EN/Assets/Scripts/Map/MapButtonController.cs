using UnityEngine;
using System.Collections;

public class MapButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 点击金币
    /// </summary>
    public void GoldClick()
    {
        Debug.Log("点击金币");
    }
    /// <summary>
    /// 点击背包
    /// </summary>
    public void BagClick()
    {
        Debug.Log("--点击背包");
    }
    /// <summary>
    /// 点击签到
    /// </summary>
    public void SignInClick()
    {
        Debug.Log("点击签到");
    }
}
