using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LevelThemeChanger : MonoBehaviour
{
    public GameObject ball;
    public ThemeChanger themeChanger;
    private int themeID = 0;
    private float[] themeZPositions;
    private int[] themeIds;

    private void Start()
    {
        themeChanger = FindObjectOfType<ThemeChanger>();
        
        // Load JSON file from Resources folder
        TextAsset jsonFile = Resources.Load<TextAsset>("LevelData/Themes1");
        
        // Parse JSON file to get themeZPositions and themeIds arrays
        LevelThemeData jsonData = JsonConvert.DeserializeObject<LevelThemeData>(jsonFile.text);
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
