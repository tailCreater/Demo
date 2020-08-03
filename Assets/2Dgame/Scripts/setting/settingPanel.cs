using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingPanel : MonoBehaviour
{
    private GameObject backButton;
    private GameObject aboutThisGameButton;
    private GameObject aboutHowToPlayButton;
    public GameObject aboutThisGamePanel;
    public GameObject closeAboutThisGamePanel;
    public GameObject aboutHowToPlayPanel;
    public GameObject closeAboutHowToPlayPanel;
    public GameObject startPanel;

    void Awake()
    {
        backButton = GameObject.Find("back");
        aboutHowToPlayButton = GameObject.Find("HowToPlay");
        aboutThisGameButton =  GameObject.Find("aboutButton");
        UIEventListener.Get(backButton).onClick += BackToStartPanel;
        UIEventListener.Get(aboutHowToPlayButton).onClick += AboutHowToPlay;
        UIEventListener.Get(closeAboutHowToPlayPanel).onClick += CloseAboutHowToPlay;
        UIEventListener.Get(aboutThisGameButton).onClick += AboutThisGame;
        UIEventListener.Get(closeAboutThisGamePanel).onClick += CloseAboutThisGame;
    }



    private void AboutHowToPlay(GameObject go)
    {
        aboutHowToPlayPanel.gameObject.SetActive(true);
    }

    private void CloseAboutHowToPlay(GameObject go)
    {
        aboutHowToPlayPanel.gameObject.SetActive(false);
    }

    private void AboutThisGame(GameObject go)
    {
        aboutThisGamePanel.gameObject.SetActive(true);
    }

    private void CloseAboutThisGame(GameObject go)
    {
        aboutThisGamePanel.SetActive(false);
    }

    private void BackToStartPanel(GameObject go)
    {
        this.gameObject.SetActive(false);
        aboutHowToPlayPanel.gameObject.SetActive(false);
        aboutThisGamePanel.SetActive(false);
        startPanel.gameObject.SetActive(true);
    }


}
