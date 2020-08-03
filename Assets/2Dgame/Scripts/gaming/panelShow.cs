using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class panelShow : MonoBehaviour
{
    
    public UILabel playerChipsShow;
    
    public StartPanel save;
    public GameObject startPanel;
    private GameObject openQuitPanel;
    public GameObject quitPanel;
    public GameObject quitYesButton;
    public GameObject quitNoButton;

    private GameObject playPanel;

    public GameObject gameStartPanel;
    public GameObject cover;
    public GameObject buttonToStart;
    public GameObject addChipsPanel;

    private gameRole quitToClear;

    void Awake()
    {
        playerChipsShow = transform.Find("Player/PlayerChip/playerChip").GetComponent<UILabel>();
        save = startPanel.GetComponent<StartPanel>();
        openQuitPanel = GameObject.Find("Playing/GamingPanel/Quit");
        quitPanel = GameObject.Find("Playing/GamingPanel/QuitPanel");
        playPanel = GameObject.Find("Playing/GamingPanel");
        quitToClear = gameStartPanel.GetComponent<gameRole>();
        UIEventListener.Get(openQuitPanel).onClick += OpenQuitPanel;
        UIEventListener.Get(quitYesButton.gameObject).onClick += QuitYes;
        UIEventListener.Get(quitNoButton.gameObject).onClick += QuitNo;
        
    }

    //持续更新玩家的筹码数
    void Update()
    {
        UpdateShow();
    }


    public void UpdateShow()
    {
        playerChipsShow.text = save.t1.playerChip + "";
    }

    private void OpenQuitPanel(GameObject go)
    {
        quitPanel.gameObject.SetActive(true);
    }

    private void QuitYes(GameObject go)
    {
        quitPanel.gameObject.SetActive(false);
        addChipsPanel.gameObject.SetActive(false);
        gameStartPanel.gameObject.SetActive(false);
        quitToClear.isCover = false;
        cover.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(false);
        buttonToStart.gameObject.SetActive(true);
        startPanel.gameObject.SetActive(true);
        foreach (cardInfoGird temp in quitToClear.cardInfoList) //循环空格重新洗牌
        {
            temp.ClearCard();
        }
        foreach (cardInfoGird temp in quitToClear.cardInfoListForCom)
        {
            temp.ClearCard();
        }
    }

    void QuitNo(GameObject go)
    {
        quitPanel.SetActive(false);
    }
}
