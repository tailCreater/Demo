using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;




public class StartPanel : MonoBehaviour
{
    [Serializable]
    public class SaveData
    {
        public string playerName;
        public int playerChip;

        public List<int> Ints = new List<int>()
        {
            1,
            2
        };
    }

    

    public GameObject startButton;
    public GameObject startGameButton;
    public GameObject settingPanelButton;
    public GameObject reLifeButton;
    public GameObject playPanel;
    public GameObject settingPanel;
    public GameObject reLifePanel;
    public GameObject reLifeYes;
    public GameObject reLifeNo;
    public UISlider slider;

    public SaveData t1;
    public string jsonName = "SaveData.json";
    private string dirpath;
    public string path;

    void Awake()
    {
        startButton = GameObject.Find("StartButton");
        UIEventListener.Get(startButton).onClick += StartButton;
     
        UIEventListener.Get(startGameButton).onClick += StartGame;

        UIEventListener.Get(settingPanelButton).onClick += SettingPanelOpen;

        UIEventListener.Get(reLifeButton).onClick += Relife;

        UIEventListener.Get(reLifeYes).onClick += ReLifeYes;

        UIEventListener.Get(reLifeNo).onClick += ReLifeNo;
        startGameButton.gameObject.SetActive(false);
        settingPanelButton.gameObject.SetActive(false);
        reLifeButton.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        reLifePanel.gameObject.SetActive(false);
        dirpath = Application.streamingAssetsPath + "/";
        path = dirpath + jsonName;
        if (!Directory.Exists(dirpath))
        {
            Directory.CreateDirectory(dirpath);
        }
        t1 = LoadDataFromJson(path);
        slider.value = 0.5f;
        //SaveDataToJson(path ,t1);
        
    }


    private void StartButton(GameObject go)
    {
        
        startGameButton.gameObject.SetActive(true);
        settingPanelButton.gameObject.SetActive(true);
        reLifeButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
    }

   private void StartGame(GameObject go)
    {
        this.gameObject.SetActive(false);
        playPanel.gameObject.SetActive(true);
    }

    private void SettingPanelOpen(GameObject go)
    {
        this.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(true);
    }
    
    private void Relife(GameObject go)
    {
        reLifePanel.gameObject.SetActive(true);
    }

    private void ReLifeYes(GameObject go)
    {
        //File.Delete(filename);
        t1.playerChip = 10000;
        SaveDataToJson(path, t1);
        reLifePanel.gameObject.SetActive(false);
    }

    private void ReLifeNo(GameObject go)
    {
        reLifePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 从json里面读取数据
    /// </summary>
    private SaveData LoadDataFromJson(string p)
    {
        if (File.Exists(p))
        {
            string json = File.ReadAllText(p);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            SaveData a = new SaveData();
            a.playerName = "player";
            a.playerChip = 10000;
            return a;
        }
    }
    /// <summary>
    /// 储存数据到json
    /// </summary>
    public void SaveDataToJson(string path, SaveData saveData)
    {

        string json = JsonUtility.ToJson(saveData, true);
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
        print("SaveComplete");
    }
}
