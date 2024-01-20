using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class LevelConfigJson {
    public string level_name;
    public string level_author;
    public float level_speed;
    public string music_path;
    public bool end_portal;
}

public class LevelConfigurator : MonoBehaviour
{
    public string jsonFilePath;
    public string jsonString;
    public string levelName;
    public string levelAuthor;
    public float levelSpeed;
    public string musicPath;
    public bool endPortal;
    private GameObject balus;
    private GameManager gameManager;
    public GameObject levelConfigPanel;
    private GameObject levelNameObject;
    private GameObject levelAuthorObject;
    private GameObject levelSpeedObject;
    private GameObject musicPathObject;
    private GameObject endPortalObject;
    TMP_InputField levelNameInput;
    TMP_InputField levelAuthorInput;
    TMP_InputField musicPathInput;
    Slider levelSpeedSlider;
    Toggle endPortalToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        gameManager = balus.GetComponent<GameManager>();
        if (gameManager.isDataDownloaded) {
            jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
            Debug.Log("Data downloaded");
        } else {
            jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
            Debug.Log("Data not downloaded");
        }
        LevelConfigJson config = JsonConvert.DeserializeObject<LevelConfigJson>(jsonString);
        levelName = config.level_name;
        levelAuthor = config.level_author;
        levelSpeed = config.level_speed;
        musicPath = config.music_path;
        endPortal = config.end_portal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowConfigPanel() {
        levelConfigPanel.SetActive(true);
        levelNameObject = GameObject.Find("LevelNameInput");
        levelNameInput = levelNameObject.GetComponent<TMP_InputField>();
        levelNameInput.text = levelName;
        levelAuthorObject = GameObject.Find("LevelAuthorInput");
        levelAuthorInput = levelAuthorObject.GetComponent<TMP_InputField>();
        levelAuthorInput.text = levelAuthor;
        levelSpeedObject = GameObject.Find("LevelSpeedSlider");
        levelSpeedSlider = levelSpeedObject.GetComponent<Slider>();
        levelSpeedSlider.value = levelSpeed;
        musicPathObject = GameObject.Find("MusicPathInput");
        musicPathInput = musicPathObject.GetComponent<TMP_InputField>();
        musicPathInput.text = musicPath;
        endPortalObject = GameObject.Find("EndPortalToggle");
        endPortalToggle = endPortalObject.GetComponent<Toggle>();
        endPortalToggle.isOn = endPortal;
        LevelEditor le = GameObject.Find("Balus").GetComponent<LevelEditor>();
        le.SetPopupOpen(true);
    }

    public void CloseConfigPanel() {
        levelName = levelNameInput.text;
        levelAuthor = levelAuthorInput.text;
        levelSpeed = levelSpeedSlider.value;
        musicPath = musicPathInput.text;
        endPortal = endPortalToggle.isOn;
        levelConfigPanel.SetActive(false);
        LevelEditor le = GameObject.Find("Balus").GetComponent<LevelEditor>();
        le.SetPopupOpen(false);
    }

    public LevelConfigJson SaveConfig() {
        LevelConfigJson config = new LevelConfigJson();
        config.level_name = levelName;
        config.level_author = levelAuthor;
        config.level_speed = levelSpeed;
        config.music_path = musicPath;
        config.end_portal = endPortal;
        return config;
    }
}
