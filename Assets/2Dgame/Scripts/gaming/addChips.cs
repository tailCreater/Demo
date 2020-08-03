using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class addChips : MonoBehaviour
{
    private static addChips instance;
    public GameObject gameStartPanel;
    public GameObject chipsPanel;
    public GameObject errorPanel;
    public GameObject aChipsToStart;
    public string chipsShow;
    private panelShow panelShowChange;
    private gameRole openWithFourCard;
    public GameObject coverForCom;
    public UIInput ChipsIn;
    void Awake()
    {
        instance = this;
        openWithFourCard = GameObject.Find("Playing/GamingPanel/GameStart").GetComponent<gameRole>();
        panelShowChange = this.GetComponentInParent<panelShow>();
        ChipsIn = GameObject.Find("ChipsInput").GetComponent<UIInput>();
        GameObject determineChipsIn = GameObject.Find("Playing/GamingPanel/ChipsPanel/ChipsInput/Determine");
        GameObject backToChipsIn = GameObject.Find("Playing/GamingPanel/ChipsPanel/Error/Back");
        GameObject closeChipsPanel = GameObject.Find("Playing/GamingPanel/ChipsPanel/Close");
        UIEventListener.Get(closeChipsPanel).onClick += CloseChipsPanel;
        UIEventListener.Get(determineChipsIn).onClick += DetermineChipsIn;
        UIEventListener.Get(backToChipsIn).onClick += CloseErrorPanel;
    }

    

    void DetermineChipsIn(GameObject determineChipsIn)
    {
        chipsShow = ChipsIn.value;
        if (int.Parse(ChipsIn.value) == 0 || int.Parse(ChipsIn.value) > panelShowChange.save.t1.playerChip)
        {
            errorPanel.SetActive(true);
        }
        else
        {
            //开始切换界面游戏
            aChipsToStart.gameObject.SetActive(false);
            chipsPanel.gameObject.SetActive(false);
            gameStartPanel.gameObject.SetActive(true);
            panelShowChange.save.t1.playerChip -= int.Parse(ChipsIn.value);
            //更新筹码
            panelShowChange.UpdateShow();
            openWithFourCard.Shuffle();
            for (int i = 0; i < 4; i++)
            {
                int getCardId = openWithFourCard.newCards[i];
                print(openWithFourCard.newCards[i]);
                openWithFourCard.GetId(getCardId);
                openWithFourCard.cardNumInTab++;
            }
            openWithFourCard.restCard.text = 48 + "";
            //记录玩家目前的点数并显示
            ObjectInfo info0 = ObjectsInfo.instance.GetObjectInfoId(openWithFourCard.cardInfoList[0].id);
            ObjectInfo info2 = ObjectsInfo.instance.GetObjectInfoId(openWithFourCard.cardInfoList[2].id);
            
            openWithFourCard.playerPoint = info0.value + info2.value;
            openWithFourCard.UpdatePlayerPointShow();

            //记录庄家目前的点数
            ObjectInfo info1 = ObjectsInfo.instance.GetObjectInfoId(openWithFourCard.cardInfoList[1].id);
            ObjectInfo info3 = ObjectsInfo.instance.GetObjectInfoId(openWithFourCard.cardInfoList[3].id);
            openWithFourCard.comPoint = info1.value + info3.value;

            //开局将庄家其中一张牌反过来
            if (openWithFourCard.isCover == false)
            {
                coverForCom.gameObject.SetActive(true);
            }
            //保存玩家筹码数
            panelShowChange.save.SaveDataToJson(panelShowChange.save.path, panelShowChange.save.t1);
        }
    }

    void CloseErrorPanel(GameObject backToChipsIn)
    {
        errorPanel.SetActive(false);
    }

    void CloseChipsPanel(GameObject closeChipsPanel)
    {
        chipsPanel.gameObject.SetActive(false);
        errorPanel.SetActive(false);
    }
}
