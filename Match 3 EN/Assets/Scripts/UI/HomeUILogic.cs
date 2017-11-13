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
    public GameObject shopPanel;        //商店界面
    public GameObject propScrollView;   //道具列表
    public GameObject propItem;         //商店道具预设
    public GameObject shopContent;      //商店内容
    public Sprite[] propIconSprite;     //道具图片

    public GameObject goldScrollView;   //金币列表
    public GameObject goldItem;         //商店金币预设
    public GameObject goldContent;      //金币内容
    public Sprite[] goldIconSprite;     //金币图片

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

    public void closeShopPanele()
    {
        shopPanel.SetActive(false);
    }
    /// <summary>
    /// 点击商店
    /// </summary>
    public void ClickShop()
    {
        GameDebug.Log("--------点击商店");
        shopPanel.SetActive(true);
        onClickPropScrollView();

    }

    /// <summary>
    /// 点击商店道具界面
    /// </summary>
    public void onClickPropScrollView()
    {
        propScrollView.gameObject.SetActive(true);
        goldScrollView.gameObject.SetActive(false);

        for (int i = 0; i < shopContent.transform.childCount; i++)
        {
            GameObject go = shopContent.transform.GetChild(i).gameObject;
            Destroy(go);
        }

        List<PrototypeData> shopProtoList = PrototypeManager.Instance.GetDataListByType("ShopPrototype");

        for (int j = 0; j < shopProtoList.Count; j++)
        {
            GameObject shopItem = (GameObject)Instantiate(propItem);
            ShopPrototype shopData = shopProtoList[j] as ShopPrototype;
            GameDebug.Log("-----商店=" + shopData.Name);
            shopItem.transform.parent = shopContent.transform;
            shopItem.transform.localScale = new Vector3(1, 1, 1);

            Text xianjia = shopItem.transform.FindChild("xianjia").GetComponent<Text>();
            xianjia.text = shopData.Price.ToString();
            Text yuanjia = shopItem.transform.FindChild("yuanjia").GetComponent<Text>();
            yuanjia.text = shopData.OriginalPrice.ToString();
            Image icon = shopItem.transform.FindChild("icon").GetComponent<Image>();
            icon.sprite = propIconSprite[shopData.Icon - 1];
            Text num = shopItem.transform.FindChild("num").GetComponent<Text>();
            num.text = shopData.Num.ToString();
            Text saleNum = shopItem.transform.FindChild("saleNum").GetComponent<Text>();
            saleNum.text = shopData.Sale.ToString();

            Button shopButton = shopItem.GetComponent<Button>();
            shopButton.onClick.AddListener(() => onShopClick(shopData));
        }
    }
    void onShopClick(ShopPrototype data)
    {
        GameDebug.Log("-----商店=" + data.Name);
    }
    /// <summary>
    /// 点击商店金币界面
    /// </summary>
    public void onClickGoldScrollView()
    {
        propScrollView.gameObject.SetActive(false);
        goldScrollView.gameObject.SetActive(true);
    }
}
