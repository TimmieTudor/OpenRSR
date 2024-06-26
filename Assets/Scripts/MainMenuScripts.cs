using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using UnityEngine.UI;
using TMPro;
using SimpleFileBrowser;

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
    public int level_id;
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
    private GameObject levelUIComponent;
    private GameObject levelItem;
    private GameObject leftButton;
    private GameObject rightButton;
    private int menuIdx = 0;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private int levelCount = 1;
    public static MainMenuScripts instance;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        instance = this;
        mainMenuCanvas = GameObject.Find("MainMenu");
        leftButton = mainMenuCanvas.transform.Find("LeftButton").gameObject;
        rightButton = mainMenuCanvas.transform.Find("RightButton").gameObject;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        titleText = GameObject.Find("TitleImage");
        levelUIComponent = GameObject.Find("LevelUI");
        levelItem = levelUIComponent.transform.Find("LevelItem").gameObject;
        menuData = JsonConvert.DeserializeObject<MenuDataJson>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "MenuData/MenuData.json")));
        CreateLevelObjects();
        foreach (MenuLevelJson level in menuData.level_list) {
            GameObject levelCoverObject = GameObject.Find($"Level{level.level_id}Cover_Button");
            levelCoverObject.GetComponent<Button>().onClick.AddListener(() => {
                gameManager.LoadLevel(level.level_id);
                mainMenuCanvas.SetActive(false);
            });
            GameObject levelTitleObject = GameObject.Find($"Level{level.level_id}Title");
            levelTitleObject.GetComponent<TextMeshProUGUI>().text = level.display_title;
            GameObject levelCoverObjectImage = GameObject.Find($"Level{level.level_id}Cover");
            try {
                byte[] levelCoverBytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, $"WorldShow/World{level.level_id}.png"));
                Texture2D levelCoverTexture = new Texture2D(1024, 2048);
                levelCoverTexture.LoadImage(levelCoverBytes);
                Image levelCoverImage = levelCoverObjectImage.GetComponent<Image>();
                levelCoverImage.sprite = Sprite.Create(levelCoverTexture, new Rect(0, 0, levelCoverTexture.width, levelCoverTexture.height), new Vector2(0.5f, 0.5f));
            } catch (System.Exception e) {
                levelCoverObjectImage.GetComponent<Image>().sprite = Sprite.Create(Resources.Load<Texture2D>("WorldShow/default_world"), new Rect(0, 0, 256, 512), new Vector2(0.5f, 0.5f), 100f);
            }

            GameObject levelPercentObject = GameObject.Find($"Level{level.level_id}Percent");
            levelPercentObject.GetComponent<TextMeshProUGUI>().text = "0%";
            GameObject levelGemCountObject = GameObject.Find($"Level{level.level_id}Gems");
            levelGemCountObject.GetComponent<TextMeshProUGUI>().text = $"0/{level.display_gems.ToString()}";
        }
        levelCount = menuData.level_list.Count;
        Debug.Log(levelCount);
        GameObject importLevelItem = Instantiate(levelItem, new Vector3(levelItem.transform.localPosition.x, 0f, 0f), Quaternion.identity, levelUIComponent.transform);
        importLevelItem.transform.localPosition = new Vector3(levelItem.transform.localPosition.x + 800f * (levelCount + 2), 0f, 0f);
        importLevelItem.transform.localRotation = Quaternion.identity;
        GameObject importLevelThumbnail = importLevelItem.transform.GetChild(0).gameObject;
        importLevelThumbnail.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Sprite.Create(Resources.Load<Texture2D>("WorldShow/default_world"), new Rect(0, 0, 256, 512), new Vector2(0.5f, 0.5f), 100f);
        importLevelThumbnail.GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("Not implemented yet");
        });
        GameObject importLevelTitle = importLevelItem.transform.GetChild(1).gameObject;
        importLevelTitle.GetComponent<TextMeshProUGUI>().text = "Import";
        GameObject importLevelPercentage = importLevelItem.transform.GetChild(2).gameObject;
        importLevelPercentage.GetComponent<TextMeshProUGUI>().text = "?%";
        GameObject importLevelGems = importLevelItem.transform.GetChild(3).gameObject;
        importLevelGems.GetComponent<TextMeshProUGUI>().text = "?/?";
        GameObject newLevelItem = Instantiate(levelItem, new Vector3(levelItem.transform.localPosition.x, 0f, 0f), Quaternion.identity, levelUIComponent.transform);
        newLevelItem.transform.localPosition = new Vector3(levelItem.transform.localPosition.x + 800f * (levelCount + 3), 0f, 0f);
        newLevelItem.transform.localRotation = Quaternion.identity;
        GameObject newLevelThumbnail = newLevelItem.transform.GetChild(0).gameObject;
        newLevelThumbnail.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Sprite.Create(Resources.Load<Texture2D>("WorldShow/default_world"), new Rect(0, 0, 256, 512), new Vector2(0.5f, 0.5f), 100f);
        newLevelThumbnail.GetComponent<Button>().onClick.AddListener(() => { 
            CreateNewLevel();
        });
        GameObject newLevelTitle = newLevelItem.transform.GetChild(1).gameObject;
        newLevelTitle.GetComponent<TextMeshProUGUI>().text = "New";
        GameObject newLevelPercentage = newLevelItem.transform.GetChild(2).gameObject;
        newLevelPercentage.GetComponent<TextMeshProUGUI>().text = "?%";
        GameObject newLevelGems = newLevelItem.transform.GetChild(3).gameObject;
        newLevelGems.GetComponent<TextMeshProUGUI>().text = "?/?";
    }

    public void ResetMenu() {
        menuIdx = 0;
        levelUIComponent.transform.localPosition = new Vector3(800f, 0f, 0f);
        Debug.Log(levelUIComponent.transform.localPosition);
        titleText.SetActive(true);
        isInTitleScreen = true;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void CreateLevelObjects() {
        foreach (MenuLevelJson level in menuData.level_list) {
            GameObject mhn_levelItem = Instantiate(levelItem, new Vector3(levelItem.transform.localPosition.x, 0f, 0f), Quaternion.identity, levelUIComponent.transform);
            mhn_levelItem.transform.localPosition = new Vector3(levelItem.transform.localPosition.x + 800f * (level.level_id + 1), 0f, 0f);
            mhn_levelItem.transform.localRotation = Quaternion.identity;
            GameObject mhn_CoverObject = mhn_levelItem.transform.GetChild(0).gameObject;
            mhn_CoverObject.name = $"Level{level.level_id}Cover_Button";
            GameObject mhn_CoverObjectImage = mhn_CoverObject.transform.GetChild(0).gameObject;
            mhn_CoverObjectImage.name = $"Level{level.level_id}Cover";
            GameObject mhn_LevelTitle = mhn_levelItem.transform.GetChild(1).gameObject;
            mhn_LevelTitle.name = $"Level{level.level_id}Title";
            GameObject mhn_LevelPercentage = mhn_levelItem.transform.GetChild(2).gameObject;
            mhn_LevelPercentage.name = $"Level{level.level_id}Percent";
            GameObject mhn_LevelGems = mhn_levelItem.transform.GetChild(3).gameObject;
            mhn_LevelGems.name = $"Level{level.level_id}Gems";
        }
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
            } else if (isMovingLeft) {
                MoveMenuLeft();
            } else if (isMovingRight) {
                MoveMenuRight();
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
                    leftButton.SetActive(true);
                    rightButton.SetActive(true);
                }
            }
        }
    }

    public void LoadLevel(int level) {
        gameManager.LoadLevel(level);
        mainMenuCanvas.SetActive(false);
    }

    public void CreateNewLevel() {
        gameManager.CreateNewLevel(levelCount);
        mainMenuCanvas.SetActive(false);
        levelCount++;
    }

    private void MoveCamera(Vector3 targetPosition) {
        if (Camera.main.transform.position != targetPosition) {
            Camera.main.transform.position = Vector3.Lerp(new Vector3(0f, 2.25f, -2f), targetPosition, t);
            levelUIComponent.transform.localPosition = Vector3.Lerp(new Vector3(800f, 0f, 0), new Vector3(0f, 0f, 0f), t);
            t += 0.02f;
            if (t >= 0.98f) { 
                levelUIComponent.transform.localPosition = new Vector3(0f, 0f, 0f);
                if (isInTitleScreen) {
                    isInTitleScreen = false;
                }
                isMovingCamera = false;
                t = 0f;
                return;
            }
        }
    }
    public void MoveMenuRight() {
        if (!isMovingRight) isMovingRight = true;
        levelUIComponent.transform.localPosition = Vector3.Lerp(new Vector3(800f * (float)(menuIdx), 0f, 0f), new Vector3(800f * (float)(menuIdx - 1), 0f, 0f), t);
        t += 0.02f;
        if (t >= 0.98f) { 
            levelUIComponent.transform.localPosition = new Vector3(800f * (float)(menuIdx - 1), 0f, 0f);
            menuIdx--;
            t = 0f;
            isMovingRight = false;
        }
    }

    public void MoveMenuLeft() {
        if (!isMovingLeft) isMovingLeft = true;
        levelUIComponent.transform.localPosition = Vector3.Lerp(new Vector3(800f * (float)(menuIdx), 0f, 0f), new Vector3(800f * (float)(menuIdx + 1), 0f, 0f), t);
        t += 0.02f;
        if (t >= 0.98f) {
            levelUIComponent.transform.localPosition = new Vector3(800f * (float)(menuIdx + 1), 0f, 0f);
            menuIdx++;
            t = 0f;
            isMovingLeft = false;
        }
    }
}
