using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Template;
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
        GameDebug.Log("点击金币");
    }
    /// <summary>
    /// 点击背包
    /// </summary>
    public void BagClick()
    {
        GameDebug.Log("--点击背包");
        List<PrototypeData> propList = PrototypeManager.Instance.GetDataListByType("PropPrototype");
        for (int i = 0; i < propList.Count; i++)
        {
            PropPrototype prop = propList[i] as PropPrototype;
            GameDebug.Log("-----prop name=" + prop.Name);
        }
    }
    /// <summary>
    /// 点击签到
    /// </summary>
    public void SignInClick()
    {
        GameDebug.Log("点击签到");
    }
}
