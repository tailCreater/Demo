using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class gameRole : MonoBehaviour
{
    private static gameRole instance;
    private addChips chipsIn;
    private panelShow playerChips;


    public UILabel chipsOnThisOne;
    public UILabel resultText;
    public UILabel playerPointShow;

    public List<cardInfoGird> cardInfoList = new List<cardInfoGird>();
    public List<cardInfoGird> cardInfoListForCom = new List<cardInfoGird>();
    private GameObject hitMe;
    private GameObject stopAndCheck;
    public GameObject closeResultPanel;
    public GameObject cardItem;
    public GameObject resultPanel;
    public GameObject startAgainButton;

    public UILabel restCard;
    public GameObject cover;
    public bool isCover;
    private bool comIsUsingA;
    private bool getSameCard = false;
    public int playerPoint = 0;
    public int comPoint = 0;
    //准备卡牌
    public int[] newCards = new int[52];
    //现在场上的牌数
    public int cardNumInTab = 0;

    void Awake()
    {
        instance = this;
        chipsIn = GameObject.Find("Playing/GamingPanel/ChipsPanel").GetComponent<addChips>();
        playerChips = instance.GetComponentInParent<panelShow>();
        hitMe = GameObject.Find("Hit");
        stopAndCheck = GameObject.Find("Stop");
        UIEventListener.Get(closeResultPanel.gameObject).onClick += CloseResultPanel;
        UIEventListener.Get(hitMe.gameObject).onClick += HitMe;
        UIEventListener.Get(stopAndCheck.gameObject).onClick += StopAndCheck;
        isCover = false;
        comIsUsingA = false;
    }


    void Update()
    {
        chipsOnThisOne.text = chipsIn.chipsShow;
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    Shuffle();
        //}
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        //bool变量数组
        bool[] assigned = new bool[52];
        for (int i = 0; i < 51; i++)
        {
            int cardId = 0;
            //判断是否存在相同卡牌了
            bool foundCard = false;
            while (foundCard == false)
            {
                //生成一个1001到1053的随机数
                cardId = Random.Range(0, 52);
                //没有相同的卡牌
                if (assigned[cardId] == false)
                {
                    foundCard = true;
                }
            }
            assigned[cardId] = true;
            newCards[i] = cardId + 1001;
            
        }
        
    }

    /// <summary>
    /// 玩家拿牌并更新玩家的点数
    /// </summary>
    /// <param name="go"></param>
    public void HitMe(GameObject go)
    {
        //int hitId = Random.Range(1001, 1053);
        //根据桌上牌数取牌组的第几张(也就是从牌组第一张一直往下取)
        int getCardId = newCards[cardNumInTab];
        //取牌
        GetId(getCardId);
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoId(getCardId);
        cardNumInTab++;
        restCard.text = int.Parse(restCard.text) - 1 + "";
        //计算玩家点数并显示
        playerPoint += info.value;
        
        //点数爆了直接结束
        if (playerPoint > 21)
        {
            resultPanel.gameObject.SetActive(true);
            resultText.text = "本局" + "\n" + "你的点数是:" + playerPoint + "爆牌了\n" + "庄家的点数是:" + comPoint + "\n" + "很遗憾你输了";
            //翻开庄家底牌
            cover.gameObject.SetActive(false);
            isCover = false;
            playerChips.save.SaveDataToJson(playerChips.save.path, playerChips.save.t1);
            //防止重复点击
            hitMe.gameObject.SetActive(false);
            stopAndCheck.gameObject.SetActive(false);
        }
        //更新显示
        UpdatePlayerPointShow();
    }

    /// <summary>
    /// 玩家停牌庄家开始取牌然后进行结算判断胜负
    /// </summary>
    /// <param name="go"></param>
    public void StopAndCheck(GameObject go)
    {
        bool isComGetCard = true;
        //翻开底牌
        cover.gameObject.SetActive(false);
        isCover = false;
        //判断玩家的A是用作1还是11
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == 0 || i > 3)
            {
                if (cardInfoList[i].id == 1001 || cardInfoList[i].id == 1014 || cardInfoList[i].id == 1027 || cardInfoList[i].id == 1040) //在玩家牌中找到A
                {
                    if (playerPoint <= 11)  //作为11来使用
                    {
                        playerPoint += 10;
                        break;
                    }
                }
            }
        }
        //先判断庄家的A是用作1还是11
        for (int i = 0; i < 4; i++) //在庄家首发两张牌中找到A
        {
            if (i % 2 == 1)
            {
                if (cardInfoList[i].id == 1001 || cardInfoList[i].id == 1014 || cardInfoList[i].id == 1027 || cardInfoList[i].id == 1040)  //首发两张牌存在A
                {
                    if (comPoint < 7 || comPoint == 11)  //作为11来使用
                    {
                        comIsUsingA = true;
                        comPoint += 10;
                        break;
                    }
                }
            }
        }
        //判断庄家是否底牌超过17点来进行庄家拿牌
        while (isComGetCard)
        {
            int getCardId = newCards[cardNumInTab];
            if (comPoint < 17) //小于17点庄家必须拿牌
            {
                //int hitId = Random.Range(1001, 1053);
                ComGetId(getCardId);
                cardNumInTab++;
                restCard.text = int.Parse(restCard.text) - 1 + "";
                if (getCardId == 1001 || getCardId == 1014 || getCardId == 1027 || getCardId == 1040) //庄家拿到A
                {
                    //判断庄家现有的牌是否用到A作为11
                    //若已经有用A作为11
                    if (comIsUsingA)  
                    {
                        comPoint += 1;
                        if (comPoint > playerPoint && comPoint >= 17)
                        {
                            isComGetCard = false;
                        }
                    }
                    //没有用A作为11
                    else  
                    {
                        //判断点数小于等于10的可以先作11使用
                        if (comPoint <= 10)
                        {
                            comIsUsingA = true;
                            comPoint += 10;
                        }
                        else
                        {
                            comPoint += 1;
                        }
                    }
                }
                else //拿到的牌不是A
                {
                    ObjectInfo info = ObjectsInfo.instance.GetObjectInfoId(getCardId);
                    comPoint += info.value;
                }
            }
            //庄家大于17点
            else  
            {
                //判断庄家是否点数大于玩家，若大于或者等于就停牌，小于则继续取牌
                if (comPoint > playerPoint || comPoint == 21)  //大于玩家或者同为21点才停牌
                {
                    isComGetCard = false;
                }
                //不是21点并且小于玩家就继续取牌
                else  
                {
                    //取牌然后判断之前的牌是否用到A作为11
                    ComGetId(getCardId);
                    cardNumInTab++;
                    restCard.text = int.Parse(restCard.text) - 1 + "";
                    ObjectInfo info = ObjectsInfo.instance.GetObjectInfoId(getCardId);
                    comPoint += info.value;
                    //用到A作为11
                    if (comIsUsingA)
                    {
                        //并且判断在取得牌之后庄家是否超过21点,若超过则A作为1来使用 若没超过A继续作为11来使用 重新判断是否取牌
                        if (comPoint > 21)
                        {
                            comPoint -= 10;    //超过了21点A就重新作为1来使用 然后再重新判断
                            comIsUsingA = false;
                        }
                    }
                }
            }
        }

        //判断胜负
        //显示结果界面
        resultPanel.gameObject.SetActive(true);   
        //若玩家点数大于庄家点数亦或是庄家直接爆点，玩家赢
        if (playerPoint > comPoint || comPoint > 21)
        {
            //点数显示的保险（若之前之前判断失误存在超过21点还有A作为11使用的时候超过31点，点数显示必须减10
            if (comPoint > 31)
            {
                comPoint -= 10;
            } 
            //如果玩家取得黑杰克(21点并且只有手上2张牌)，赢得1.5倍的赌注
            if (playerPoint == 21 && cardInfoList[4].id == 0) 
            {
                resultText.text = "本局" + "\n" + "你的点数是:黑杰克" + playerPoint + "\n" + "庄家的点数是:" + comPoint + "\n" + "恭喜你赢得了" + Int32.Parse(chipsOnThisOne.text) * 3 + "筹码";
                playerChips.save.t1.playerChip += Int32.Parse(chipsOnThisOne.text) * 3;
            }
            else
            {
                resultText.text = "本局" + "\n" + "你的点数是:" + playerPoint + "\n" + "庄家的点数是:" + comPoint + "爆牌了\n" + "恭喜你赢得了" + Int32.Parse(chipsOnThisOne.text) * 2 + "筹码";
                playerChips.save.t1.playerChip += Int32.Parse(chipsOnThisOne.text) * 2;
            }
        }
        //若庄家都是21点
        else if (comPoint == 21 && playerPoint == comPoint)
        {
            //如果玩家取得黑杰克而庄家并不是黑杰克，玩家胜
            if (cardInfoList[4].id == 0 && cardInfoListForCom[0].id != 0) 
            {
                resultText.text = "本局" + "\n" + "你的点数是:黑杰克" + playerPoint + "\n" + "庄家的点数是:" + comPoint + "\n" + "恭喜你赢得了" + Int32.Parse(chipsOnThisOne.text) * 3 + "筹码";
                playerChips.save.t1.playerChip += Int32.Parse(chipsOnThisOne.text) * 3;
            }
            //如果庄家家取得黑杰克而玩家并不是黑杰克，庄家胜
            else if (cardInfoList[4].id != 0 && cardInfoListForCom[0].id == 0)  
            {
                resultText.text = "本局" + "\n" + "你的点数是:" + playerPoint + "\n" + "庄家的点数是:黑杰克" + comPoint + "\n" + "很遗憾你输了";
            }
            //双方一样
            else
            {
                resultText.text = "本局" + "\n" + "你的点数是:" + playerPoint + "\n" + "庄家的点数是:" + comPoint + "\n" + "这局是平局所以返回赌注";
                playerChips.save.t1.playerChip += Int32.Parse(chipsOnThisOne.text);
            }
        }
        else
        {
            resultText.text = "本局" + "\n" + "你的点数是:" + playerPoint + "\n" + "庄家的点数是:" + comPoint + "\n" + "很遗憾你输了";
        }
        playerChips.save.SaveDataToJson(playerChips.save.path, playerChips.save.t1);
        //防止重复点击
        hitMe.gameObject.SetActive(false);
        stopAndCheck.gameObject.SetActive(false);
    }

    /// <summary>
    /// 玩家取牌(通过id)
    /// </summary>
    /// <param name="id"></param>
    public void GetId(int id)
    {
        cardInfoGird gird = null;
        //找到新的空格
        foreach (cardInfoGird temp in cardInfoList)
        {
            if (temp.id == 0)
            {
                gird = temp;
                break;
            }
        }
        GameObject itemGO = NGUITools.AddChild(gird.gameObject, cardItem); //创建卡牌
        itemGO.transform.parent = gird.transform;
        itemGO.transform.localPosition = Vector3.zero;      //放在新空格上
        gird.SetId(id);

    }

    /// <summary>
    /// 庄家拿牌(通过id)
    /// </summary>
    /// <param name="id"></param>
    public void ComGetId(int id)
    {
        cardInfoGird gird = null;
        foreach (cardInfoGird temp in cardInfoListForCom) //找到新的空格
        {
            if (temp.id == 0)
            {
                gird = temp;
                break;
            }
        }
        GameObject itemGO = NGUITools.AddChild(gird.gameObject, cardItem); //创建卡牌
        itemGO.transform.parent = gird.transform;
        itemGO.transform.localPosition = Vector3.zero;      //放在新空格上
        gird.SetId(id);

    }

    //关闭结果窗口并清空双方的点数以及显示开始按钮
    private void CloseResultPanel(GameObject go)
    {
        cardNumInTab = 0;
        playerPoint = 0;
        comPoint = 0;
        resultPanel.gameObject.SetActive(false);
        instance.gameObject.SetActive(false);
        hitMe.gameObject.SetActive(true);
        stopAndCheck.gameObject.SetActive(true);
        startAgainButton.gameObject.SetActive(true);
    }

    //更新玩家点数的显示
    public void UpdatePlayerPointShow()
    {
        playerPointShow.text = playerPoint.ToString();
    }
}
