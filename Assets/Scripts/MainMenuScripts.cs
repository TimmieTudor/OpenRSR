using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public enum SortBy {
    difficulty_min,
    difficulty_max,
    difficulty_avg
}

[System.Serializable]
public enum SortOrder {
    ascending,
    descending
}

[System.Serializable]
public enum AvgType {
    // minimum
    min,
    // akin to the generalized mean with p=-10
    mihnea,
    // harmonic mean
    harmonic,
    // geometric mean
    geometric,
    // arithmetic mean
    arithmetic,
    // quadratic mean
    quadratic,
    // akin to the generalized mean with p=10
    maxea,
    // maximum
    max
}

[System.Serializable]
public class MenuLevelJson {
    public string display_title;
    public int display_gems;
    public int display_crowns;
    public bool display_author;
    public bool display_progress;
    public float level_difficulty_easy;
    public float level_difficulty_hard;
}

[System.Serializable]
public class MenuDataJson {
    [JsonConverter(typeof(StringEnumConverter))]
    public SortBy sortBy = SortBy.difficulty_avg;
    [JsonConverter(typeof(StringEnumConverter))]
    public SortOrder sortOrder = SortOrder.ascending;
    [JsonConverter(typeof(StringEnumConverter))]
    public AvgType avgType = AvgType.arithmetic;
    public List<MenuLevelJson> level_list = new List<MenuLevelJson>();
}

public class MainMenuScripts : MonoBehaviour
{
    private GameManager gameManager;
    private bool isMovingCamera = false;
    private float t = 0f;
    private GameObject mainMenuCanvas;
    private GameObject level1CoverObject;
    private GameObject level1TitleObject;
    public MenuDataJson menuData = new MenuDataJson();
    private string jsonString;
    private GameObject titleText;
    private bool isInTitleScreen = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        mainMenuCanvas = GameObject.Find("MainMenu");
        titleText = GameObject.Find("TitleImage");
        level1CoverObject = GameObject.Find("Level1Cover_Button");
        level1TitleObject = GameObject.Find("Level1Title");
        jsonString = File.ReadAllText(Application.persistentDataPath + "/MenuData/MenuData.json");
        menuData = JsonConvert.DeserializeObject<MenuDataJson>(jsonString);
        level1TitleObject.GetComponent<TextMeshProUGUI>().text = menuData.level_list[0].display_title;
    }

    public float GeneralizedMean(float[] values, float p) {
        float sum = 0f;
        foreach (float value in values) {
            sum += Mathf.Pow(value, p);
        }
        return Mathf.Pow(sum / values.Length, 1f / p);
    }

    public float HarmonicMean(float[] values) {
        float sum = 0f;
        foreach (float value in values) {
            sum += 1f / value;
        }
        return values.Length / sum;
    }

    public float GeometricMean(float[] values) {
        float product = 1f;
        foreach (float value in values) {
            product *= value;
        }
        return Mathf.Pow(product, 1f / values.Length);
    }

    public float ArithmeticMean(float[] values) {
        float sum = 0f;
        foreach (float value in values) {
            sum += value;
        }
        return sum / values.Length;
    }

    public float QuadraticMean(float[] values) {
        float sum = 0f;
        foreach (float value in values) {
            sum += value * value;
        }
        return Mathf.Sqrt(sum / values.Length);
    }

    public float Maxea(float[] values) {
        return GeneralizedMean(values, 10f);
    }

    public float Mihnea(float[] values) {
        return GeneralizedMean(values, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isInMainMenu) {
            DetectClick();
        } else {
            if (isMovingCamera && isInTitleScreen) {
                MoveCamera(new Vector3(5f, 2.25f, -2f));
            }
        }
    }

    void DetectClick()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    // Add your code here to handle the click event
                    gameManager.SetIsInMainMenu(false);
                    isMovingCamera = true;
                    titleText.SetActive(false);
                }
            }
        }
    }

    public void LoadLevel(int level) {
        gameManager.LoadLevel(level);
        mainMenuCanvas.SetActive(false);
    }

    private void MoveCamera(Vector3 targetPosition) {
        if (Camera.main.transform.position != targetPosition) {
            Camera.main.transform.position = Vector3.Lerp(new Vector3(0f, 2.25f, -2f), targetPosition, t);
            t += 0.02f;
            level1CoverObject.transform.position = new Vector3(level1CoverObject.transform.position.x - Screen.width / 180f, level1CoverObject.transform.position.y, level1CoverObject.transform.position.z);
            level1TitleObject.transform.position = new Vector3(level1TitleObject.transform.position.x - Screen.width / 180f, level1TitleObject.transform.position.y, level1TitleObject.transform.position.z);
            if (t >= 0.98f) { 
                level1CoverObject.transform.localPosition = new Vector3(0f, 0f, 0f);
                level1TitleObject.transform.localPosition = new Vector3(0f, 250f, 0f);
                if (isInTitleScreen) {
                    isInTitleScreen = false;
                }
                isMovingCamera = false;
                t = 0f;
                return;
            }
        }
    }
}
