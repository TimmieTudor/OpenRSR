using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        gameManager = balus.GetComponent<GameManager>();
        if (gameManager.isDataDownloaded) {
            jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        } else {
            jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
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
}
