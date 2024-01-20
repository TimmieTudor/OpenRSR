using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class LevelThemeChanger : MonoBehaviour
{
    public GameObject ball;
    public ThemeChanger themeChanger;
    public string jsonFilePath;
    private int themeID = 0;
    private List<float> themeZPositions;
    private List<int> themeIds;
    private GameManager gameManager;
    private string jsonString;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Balus");
        themeChanger = GetComponent<ThemeChanger>();
        gameManager = ball.GetComponent<GameManager>();

        if (gameManager.isDataDownloaded)
        {
            jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        }
        else 
        {
            jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
        }
        
        // Parse JSON file to get themeZPositions and themeIds arrays
        LevelThemeData jsonData = JsonConvert.DeserializeObject<LevelThemeData>(jsonString);
        themeZPositions = jsonData.themeZPositions;
        themeIds = jsonData.themeIds;

        // Set initial theme ID for ThemeChanger script
        themeChanger.themeID = themeIds[0];
    }

    private void Update()
    {
        // Get current ball position
        float ballZPos = ball.transform.position.z;

        // Check if ball has reached the next theme Z position
        if (themeID < themeZPositions.Count && ballZPos >= themeZPositions[themeID])
        {
            // Update theme ID for ThemeChanger script
            if (themeID < themeIds.Count)
            {
                themeChanger.themeID = themeIds[themeID];
            }

            // Change to next theme ID
            themeID++;
        }
    }
    
    public void UpdateData() {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
    }
}

[System.Serializable]
public class LevelThemeData
{
    public List<float> themeZPositions;
    public List<int> themeIds;
}
