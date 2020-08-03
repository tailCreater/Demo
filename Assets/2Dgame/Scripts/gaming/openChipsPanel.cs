using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChipsPanel : MonoBehaviour
{
    private GameObject chipsToStartPanel;
    private gameRole restartToClear;
    void Awake()
    {
        chipsToStartPanel = GameObject.Find("Playing/GamingPanel/ChipsPanel");
        restartToClear = GameObject.Find("Playing/GamingPanel/GameStart").GetComponent<gameRole>();
        UIEventListener.Get(this.gameObject).onClick += Click;
    }

    // Update is called once per frame
    void Click(GameObject go)
    {
        chipsToStartPanel.gameObject.SetActive(true);
        foreach (cardInfoGird temp in restartToClear.cardInfoList) //循环空格重新洗牌
        {
            temp.ClearCard();
        }
        foreach (cardInfoGird temp in restartToClear.cardInfoListForCom)
        {
            temp.ClearCard();
        }
    }
}
