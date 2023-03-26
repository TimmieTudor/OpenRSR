using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LevelThemeChanger : MonoBehaviour
{
    public GameObject ball;
    public ThemeChanger themeChanger;
    public string jsonFilePath;
    private int themeID = 0;
    private float[] themeZPositions;
    private int[] themeIds;
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
        if (themeID < themeZPositions.Length && ballZPos >= themeZPositions[themeID])
        {
            // Update theme ID for ThemeChanger script
            if (themeID < themeIds.Length)
            {
                themeChanger.themeID = themeIds[themeID];
            }

            // Change to next theme ID
            themeID++;
        }
    }
}

[System.Serializable]
public class LevelThemeData
{
    public float[] themeZPositions;
    public int[] themeIds;
}
