using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Core;
using Core.Template;
using UnityEngine.UI;
/// <summary>
/// HomeUI逻辑类
/// </summary>
public class HomeUILogic : MonoBehaviour {

    public GameObject homeUI;

    public GameObject signInPanel;

    public GameObject[] listDayObjs;    //每日签到列表

    public Text surplusDayText;         //剩余签到清零文字

    public Sprite[] dayNormalSprite;    //签到默认图片
    public Sprite[] dayClickSprite;     //已签到图片

    const string SIGNDAYS = "signdays";                     //上次签到日期
    const string SIGNSTARTDAY = "signstartday";             //开始这轮签到日期
    const string SIGNTOTLENUM = "signtotlenum";             //已经签到天数

    const int SIGNROUNDNUM = 31;                            //一轮签到天数

	// Use this for initialization
	void Start () {

        


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
        signInPanel.gameObject.SetActive(true);
        DataLoader.enableclick = false;

        PlayerPrefs.DeleteAll();
        updateSignData();
    }
    private void updateSignData()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        int totalDaysNow = Convert.ToInt32(ts.TotalDays);
        string signStartDaystr = PlayerPrefs.GetString(SIGNSTARTDAY, "");
        //第一次进入
        if (signStartDaystr.Length == 0)
        {
            //PlayerPrefs.SetString(SIGNDAYS, totalDaysNow.ToString());
            PlayerPrefs.SetString(SIGNSTARTDAY, totalDaysNow.ToString());
            PlayerPrefs.SetInt(SIGNTOTLENUM, 0);
        }

        //double saveDays = Convert.ToDouble(signdaystr);
        int signStartdays = Convert.ToInt32(PlayerPrefs.GetString(SIGNSTARTDAY, ""));
        int signNum = PlayerPrefs.GetInt(SIGNTOTLENUM, 0);
        //剩余天数
        int surplueDayNum = SIGNROUNDNUM - (totalDaysNow - signStartdays) - signNum;
        //如果签满一轮
        if (surplueDayNum == 0)
        {
            PlayerPrefs.SetString(SIGNSTARTDAY, totalDaysNow.ToString());
            PlayerPrefs.SetInt(SIGNTOTLENUM, 0);
            surplueDayNum = SIGNROUNDNUM;
        }

        surplusDayText.gameObject.SetActive(true);
        //UI显示剩余天数
        surplusDayText.text = "剩余" + surplueDayNum + "天清零";
        if (surplueDayNum == SIGNROUNDNUM) {
            surplusDayText.gameObject.SetActive(false);
        }

        List<PrototypeData> dayProtoList = PrototypeManager.Instance.GetDataListByType("SignInPrototype");
        for (int i = 0; i < SIGNROUNDNUM; i++)
        {
            SignInPrototype datyData = dayProtoList[i] as SignInPrototype;
            //GameDebug.Log("-----prop name=" + datyData.Name);
            GameObject dayObj = listDayObjs[i];
            Sprite daySprite = null;

            Image imageSource = dayObj.GetComponent<Image>();
            switch (datyData.Type)
            {
                case 1:
                    daySprite = dayNormalSprite[0];
                    dayObj.transform.localScale = new Vector3(1, 1, 1);
                    break;
                case 2:
                    daySprite = dayNormalSprite[1];
                    dayObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    break;
                case 3:
                    daySprite = dayNormalSprite[2];
                    dayObj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    break;
                case 4:
                    daySprite = dayNormalSprite[3];
                    dayObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    break;
                case 5:
                    daySprite = dayNormalSprite[4];
                    dayObj.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                    break;
            }
            //int signNum = PlayerPrefs.GetInt(SIGNTOTLENUM, 0);
            if (i < signNum)
            {
                int daytype = datyData.Type - 1;
                daySprite = dayClickSprite[daytype];
            }
            imageSource.sprite = daySprite;

        }
    }
    /// <summary>
    /// 关闭签到
    /// </summary>
    public void closeSignInPanel()
    {
        signInPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 点击今日签到
    /// </summary>
    public void ClickSignInToday()
    {
        GameDebug.Log("点击今日签到");
        int signNum = PlayerPrefs.GetInt(SIGNTOTLENUM, 0);
        signNum++;
        PlayerPrefs.SetInt(SIGNTOTLENUM, signNum);
        List<PrototypeData> dayProtoList = PrototypeManager.Instance.GetDataListByType("SignInPrototype");
        SignInPrototype datyData = dayProtoList[signNum - 1] as SignInPrototype;
        GameDebug.Log("-----奖励=" + datyData.Value);
        updateSignData();
    }

    /// <summary>
    /// 点击每一天
    /// </summary>
    /// <param name="id"></param>
    public void ClickDay(int id)
    {

    }

    /// <summary>
    /// 点击商店
    /// </summary>
    public void ClickShop()
    {
        GameDebug.Log("--------点击商店");
    }
}
