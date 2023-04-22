using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

public class ThemeChanger : MonoBehaviour
{
    public string jsonFilePath;
    public int themeID;
    private List<Dictionary<string, Color>> themeList;
    private List<Dictionary<string, string>> backgroundList;
    private Dictionary<string, Color> colorDict;
    private Dictionary<string, string> backgroundDict;
    private GameManager gameManager;
    private string jsonString;
    private GameObject balus;
    private Texture2D backgroundFile;
    private List<Texture2D> files = new List<Texture2D>();
    List<string> filePaths = new List<string>();
    List<string> fixedFilePaths = new List<string>();
    private bool loadedImages = false;
    
    private void Start()
    {
        filePaths = new List<string>(Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Backgrounds")));
        foreach (string filePath in filePaths)
        {
            #if UNITY_EDITOR
            string fixedFilePath = Path.Combine(filePath.Split('/', '\\'));
            fixedFilePaths.Add(fixedFilePath);
            if (fixedFilePath.Contains(".png"))
            {
                StartCoroutine(LoadImage(filePath, files));
            }
            #else
            if (filePath.Contains(".png"))
            {
                StartCoroutine(LoadImage(filePath, files));
            }
            #endif
        }
        loadedImages = true;

        balus = GameObject.FindGameObjectWithTag("Balus");
        gameManager = balus.GetComponent<GameManager>();
        if (gameManager.isDataDownloaded)
        {
            jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        }
        else
        {
            jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
        }
        JArray jArray = JArray.Parse(jsonString);

        themeList = new List<Dictionary<string, Color>>();
        backgroundList = new List<Dictionary<string, string>>();
        colorDict = new Dictionary<string, Color>();
        backgroundDict = new Dictionary<string, string>();

        foreach (JObject jObject in jArray.Children<JObject>())
        {
            foreach (JProperty jProperty in jObject.Properties())
            {
                string key = jProperty.Name;
                if (key.Contains("color"))
                {
                    if (colorDict.Count == 5)
                    {
                        themeList.Add(new Dictionary<string, Color>(colorDict));
                        colorDict.Clear();
                    }
                    
                    Color color = new Color();
                    List<float> values = jProperty.Value.ToObject<List<float>>();
                    color.r = values[0] / 255f;
                    color.g = values[1] / 255f;
                    color.b = values[2] / 255f;
                    color.a = values[3] / 255f;
                    colorDict[key] = color;
                }
                else if (key == "background_path")
                {
                    backgroundDict[key] = jProperty.Value.ToObject<string>();
                    
                    if (backgroundDict.Count == 1)
                    {
                        backgroundList.Add(new Dictionary<string, string>(backgroundDict));
                        backgroundDict.Clear();
                    }
                }
            }
        }

        // add the last background dictionary to the list
        if (backgroundDict.Count > 0)
        {
            backgroundList.Add(new Dictionary<string, string>(backgroundDict));
        }

        // add the last theme dictionary to the list
        if (colorDict.Count > 0)
        {
            themeList.Add(new Dictionary<string, Color>(colorDict));
        }

        Dictionary<string, Color> currentTheme = themeList[themeID];

        GameObject[] obstacle1s = GameObject.FindGameObjectsWithTag("obstacle1");
        foreach (GameObject obstacle in obstacle1s)
        {
            Renderer renderer = obstacle.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["obstacle1_color"];
            }
        }
        GameObject[] obstacle2s = GameObject.FindGameObjectsWithTag("obstacle2");
        foreach (GameObject obstacle in obstacle2s)
        {
            Renderer renderer = obstacle.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["obstacle2_color"];
            }
        }
        GameObject[] baluses = GameObject.FindGameObjectsWithTag("Balus");
        foreach (GameObject balus in baluses)
        {
            Renderer renderer = balus.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["balus_color"];
            }
        }
        GameObject[] normalTiles = GameObject.FindGameObjectsWithTag("NormalTile");
        foreach (GameObject normalTile in normalTiles)
        {
            Renderer renderer = normalTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["tile_color"];
            }
        }
        GameObject[] jumpTiles = GameObject.FindGameObjectsWithTag("JumpTile");
        foreach (GameObject jumpTile in jumpTiles)
        {
            Renderer renderer = jumpTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["jumptile_color"];
            }
        }
        
        Dictionary<string, string> backgroundFilePath = backgroundList[themeID];
        if (gameManager.isDataDownloaded && loadedImages)
        {
            #if UNITY_EDITOR
            string fixedBackgroundPath = Path.Combine(Application.persistentDataPath, backgroundFilePath["background_path"] + ".png");
            fixedBackgroundPath = Path.Combine(fixedBackgroundPath.Split('/', '\\'));
            backgroundFile = files[fixedFilePaths.IndexOf(fixedBackgroundPath)];
            #else
            string fixedBackgroundPath = Application.persistentDataPath + "/" + backgroundFilePath["background_path"] + ".png";
            backgroundFile = files[filePaths.IndexOf(fixedBackgroundPath)];
            #endif
        }
        else
        {
            backgroundFile = Resources.Load<Texture2D>(backgroundFilePath["background_path"]);
        }
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        if (backgroundFile != null)
        {
            Renderer renderer = background.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = backgroundFile;
            }
        }
    }

    IEnumerator LoadImage(string url, List<Texture2D> images)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            webRequest.SendWebRequest();
 
            while (!webRequest.isDone)
            {
                // Do other stuff while the image is loading
                Debug.Log("Loading...");
                yield return new WaitForEndOfFrame();
            }
 
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || 
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                images.Add(texture);
            }
        }
    }

    private void Update()
    {
        Dictionary<string, Color> currentTheme = themeList[themeID];

        GameObject[] obstacle1s = GameObject.FindGameObjectsWithTag("obstacle1");
        foreach (GameObject obstacle in obstacle1s)
        {
            Renderer renderer = obstacle.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["obstacle1_color"];
            }
        }
        GameObject[] obstacle2s = GameObject.FindGameObjectsWithTag("obstacle2");
        foreach (GameObject obstacle in obstacle2s)
        {
            Renderer renderer = obstacle.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["obstacle2_color"];
            }
        }
        GameObject[] baluses = GameObject.FindGameObjectsWithTag("Balus");
        foreach (GameObject balus in baluses)
        {
            Renderer renderer = balus.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["balus_color"];
            }
        }
        GameObject[] normalTiles = GameObject.FindGameObjectsWithTag("NormalTile");
        foreach (GameObject normalTile in normalTiles)
        {
            Renderer renderer = normalTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["tile_color"];
            }
        }
        GameObject[] jumpTiles = GameObject.FindGameObjectsWithTag("JumpTile");
        foreach (GameObject jumpTile in jumpTiles)
        {
            Renderer renderer = jumpTile.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = currentTheme["jumptile_color"];
            }
        }

        Dictionary<string, string> backgroundFilePath = backgroundList[themeID];

        if (gameManager.isDataDownloaded && loadedImages)
        {
            #if UNITY_EDITOR
            string fixedBackgroundPath = Path.Combine(Application.persistentDataPath, backgroundFilePath["background_path"] + ".png");
            fixedBackgroundPath = Path.Combine(fixedBackgroundPath.Split('/', '\\'));
            backgroundFile = files[themeID];
            #else
            string fixedBackgroundPath = Application.persistentDataPath + "/" + backgroundFilePath["background_path"] + ".png";
            backgroundFile = files[themeID];
            #endif
        }
        else
        {
            backgroundFile = Resources.Load<Texture2D>(backgroundFilePath["background_path"]);
        }

        GameObject background = GameObject.FindGameObjectWithTag("Background");
        if (backgroundFile != null)
        {
            Renderer renderer = background.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = backgroundFile;
            }
        }
    }
}
