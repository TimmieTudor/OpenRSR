using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class LevelEditor : MonoBehaviour
{
    public GameObject balus;
    public GameObject gameOverPanel;
    public GameObject gamePlayCanvas;
    public GameObject gameEditCanvas;
    public GroundRenderer gre;
    public GameFreeze GFreeze;
    public Camera m_camera;
    public CameraFollow CFollow;
    public GameObject grid;
    private string realPercent;
    public EnemyRenderer ere;
    public int gridSize = 10;
    private bool isInEditor = false;
    private bool isPopupOpen = false;
    private GameObject themeText;
    private LevelThemeChanger themeChanger;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private ThemeChanger themeChanger2;
    private GameObject levelRenderer;
    private Rigidbody rb;
    private GameManager manager;
    private string groundJsonString;
    private string enemyJsonString;
    private string themeJsonString;
    private PositionsData gdata;
    private EnemyPositionsData edata;
    private LevelThemeData ltdata;
    public int objectLayer = 0;
    public int objectID = 0;
    public GameObject tilesPanel;
    public GameObject obstaclesPanel;
    public GameObject themesPanel;
    private LevelConfigurator levelConfig;
    // Start is called before the first frame update
    void Start()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        levelRenderer = GameObject.Find("LevelRenderer");
        rb = GetComponent<Rigidbody>();
        sphd = GetComponent<SphereDragger>();
        themeChanger = levelRenderer.GetComponent<LevelThemeChanger>();
        sphm = GetComponent<SphereMovement>();
        themeChanger2 = levelRenderer.GetComponent<ThemeChanger>();
        levelConfig = levelRenderer.GetComponent<LevelConfigurator>();
        manager = balus.GetComponent<GameManager>();
        if (!manager.isDataDownloaded) {
            groundJsonString = Resources.Load<TextAsset>("LevelData/Ground1").text;
            enemyJsonString = Resources.Load<TextAsset>("LevelData/Enemies1").text;
            themeJsonString = Resources.Load<TextAsset>("LevelData/Themes1").text;
        } else {
            groundJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, gre.jsonFilePath + ".json"));
            enemyJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, ere.jsonFilePath + ".json"));
            themeJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, themeChanger.jsonFilePath + ".json"));
        }
        gdata = JsonConvert.DeserializeObject<PositionsData>(groundJsonString);
        edata = JsonConvert.DeserializeObject<EnemyPositionsData>(enemyJsonString);
        ltdata = JsonConvert.DeserializeObject<LevelThemeData>(themeJsonString);
        themeText = GameObject.Find("DeceBalus_Theme_Text");
    }

    public void ClearEverything() {
        GameObject[] normalTiles = GameObject.FindGameObjectsWithTag("NormalTile");
        GameObject[] jumpTiles = GameObject.FindGameObjectsWithTag("JumpTile");
        GameObject[] glassTiles = GameObject.FindGameObjectsWithTag("GlassTile");
        GameObject[] glassTilesGroup1 = GameObject.FindGameObjectsWithTag("GlassGroup1");
        GameObject[] glassTilesGroup2 = GameObject.FindGameObjectsWithTag("GlassGroup2");
        GameObject[] glassTilesGroup3 = GameObject.FindGameObjectsWithTag("GlassGroup3");
        GameObject[] moverTilesGroup1 = GameObject.FindGameObjectsWithTag("MoverGroup1");
        GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
        Vector3 jumpTilePos = new Vector3(-32f, 0.2f, -2f);
        Vector3 glassTilePos = new Vector3(-46f, 0f, 0f);
        Vector3 glassGroup1Pos = new Vector3(-49f, 0f, 0f);
        Vector3 glassGroup2Pos = new Vector3(-50f, 0f, 0f);
        Vector3 glassGroup3Pos = new Vector3(-51f, 0f, 0f);
        Vector3 moverGroup1Pos = new Vector3(-99f, 0f, 0f);
        Vector3 normalTilePos = new Vector3(-35f, 0.2f, 0.08f);
        List<Vector3> ObstaclePoses = new List<Vector3>();
        ObstaclePoses.Add(new Vector3(-33f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-40f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-42f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-51f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-54f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-56f, 0.2f, 0f));
        ObstaclePoses.Add(new Vector3(-60f, 0.2f, 0f));
        ObstaclePoses.Add(new Vector3(-64f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-66f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-68f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-70f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-72f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-74f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-76f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-5f, 0f, 0f));
        ObstaclePoses.Add(new Vector3(-83f, 0f, 0f));
        foreach (GameObject tile in normalTiles) {
            if (tile.transform.position == normalTilePos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in jumpTiles) {
            if (tile.transform.position == jumpTilePos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in glassTiles) {
            if (tile.transform.position == glassTilePos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in glassTilesGroup1) {
            if (tile.transform.position == glassGroup1Pos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in glassTilesGroup2) {
            if (tile.transform.position == glassGroup2Pos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in glassTilesGroup3) {
            if (tile.transform.position == glassGroup3Pos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in moverTilesGroup1) {
            if (tile.transform.position == moverGroup1Pos) {
                continue;
            }
            Destroy(tile);
        }
        foreach (GameObject tile in risers) {
            if (ObstaclePoses.Contains(tile.transform.position)) {
                continue;
            }
            Destroy(tile);
        }
    }

    // This is what replaced Mihnea with Workspace
    public void editorTransition() {
        gre.enabled = false;
        ere.enabled = false;
        manager.enabled = false;
        GFreeze.enabled = false;
        gamePlayCanvas.SetActive(false);
        gameEditCanvas.SetActive(true);
        CFollow.enabled = false;
        sphd.enabled = false;
        sphm.enabled = false;
        balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        Vector3 camPos = new Vector3(0f, 10f, 0f);
        Quaternion q = Quaternion.Euler(90f, 0f, 0f);
        m_camera.transform.position = camPos;
        m_camera.transform.rotation = q;
        ClearEverything();
        gre.UpdateData();
        ere.UpdateData();
        themeChanger.UpdateData();
        gdata = gre.GetData();
        edata = ere.GetData();
        ltdata = themeChanger.GetData();
        List<List<int>> gpositions = gdata.positions;
        List<List<int>> epositions = edata.positions;
        for (int i = 0; i < gpositions.Count; i++) {
            for (int j = 0; j < gpositions[i].Count; j++) {
                int hasPrefab = gpositions[i][j];
                float x = j - 2;
                float z = i * gre.prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                GameObject spawnedPrefab = Instantiate(gre.prefabs[hasPrefab], spawnPosition, Quaternion.identity);
                if (hasPrefab == 4) {
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "1";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                } else if (hasPrefab == 5) {
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "2";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                } else if (hasPrefab == 6) {
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "3";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                } else if (hasPrefab == 7) {
                    if (spawnedPrefab.TryGetComponent<LeftMovingTileAnim>(out LeftMovingTileAnim leftMovingTileAnim)) {
                        leftMovingTileAnim.enabled = false;
                        GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
                        tileBaseObject.transform.position = new Vector3(x, 0f, z);
                        GameObject canvasObject = new GameObject("Text_Canvas");
                        Canvas canvas = canvasObject.AddComponent<Canvas>();
                        GameObject textObject = new GameObject("Text");
                        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                        textComponent.text = "<~";
                        textComponent.alignment = TextAlignmentOptions.Center;
                        canvas.renderMode = RenderMode.WorldSpace;
                        textComponent.fontStyle = FontStyles.Bold;
                        textComponent.fontSize = 0.4f;
                        canvasObject.transform.SetParent(spawnedPrefab.transform);
                        textObject.transform.SetParent(canvasObject.transform);
                        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                        canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                        canvasObject.transform.position = new Vector3(x, 1f, z);
                    }
                } else if (hasPrefab == 8) {
                    if (spawnedPrefab.TryGetComponent<RightMovingTileAnim>(out RightMovingTileAnim rightMovingTileAnim)) {
                        rightMovingTileAnim.enabled = false;
                        GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
                        tileBaseObject.transform.position = new Vector3(x, 0f, z);
                        GameObject canvasObject = new GameObject("Text_Canvas");
                        Canvas canvas = canvasObject.AddComponent<Canvas>();
                        GameObject textObject = new GameObject("Text");
                        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                        textComponent.text = "~>";
                        textComponent.alignment = TextAlignmentOptions.Center;
                        canvas.renderMode = RenderMode.WorldSpace;
                        textComponent.fontStyle = FontStyles.Bold;
                        textComponent.fontSize = 0.4f;
                        canvasObject.transform.SetParent(spawnedPrefab.transform);
                        textObject.transform.SetParent(canvasObject.transform);
                        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                        canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                        canvasObject.transform.position = new Vector3(x, 1f, z);
                    }
                } else if (hasPrefab == 9) {
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "1";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                }
                // Obsolete code. Will remove later
                /*
                if (gpositions[i][j] == 1) {
                    float x = j - 2;
                    float z = i * gre.prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    GameObject spawnedPrefab = Instantiate(gre.prefab, spawnPosition, Quaternion.identity);
                }
                else if (gpositions[i][j] == 2) {
                    float x = j - 2;
                    float z = i * gre.prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    GameObject spawnedPrefab = Instantiate(gre.prefab2, spawnPosition, Quaternion.identity);
                } else if (gpositions[i][j] == 3) {
                    float x = j - 2;
                    float z = i * gre.prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    GameObject spawnedPrefab = Instantiate(gre.prefab3, spawnPosition, Quaternion.identity);
                } */
            }
        }
        for (int i = 0; i < epositions.Count; i++) {
            for (int j = 0; j < epositions[i].Count; j++) {
                int hasPrefab = epositions[i][j];
                float x = j - 2;
                float z = i * ere.prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0.55f, z);
                if (hasPrefab == 4 || hasPrefab == 5 || (hasPrefab >= 10 && hasPrefab <= 15)) {
                    spawnPosition = new Vector3(x, 0f, z);
                } else if (hasPrefab == 6 || hasPrefab == 7) {
                    spawnPosition = new Vector3(x, 0.2f, z);
                } else if (hasPrefab == 8) {
                    spawnPosition = new Vector3(x + 1f, 0.2f, z);
                } else if (hasPrefab == 9) {
                    spawnPosition = new Vector3(x - 1f, 0.2f, z);
                } else {
                    spawnPosition = new Vector3(x, 0.55f, z);
                }
                GameObject spawnedPrefab = Instantiate(ere.prefabs[hasPrefab], spawnPosition, Quaternion.identity);
                
                if (hasPrefab == 3) {
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "SUS";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 2f, z);
                } else if (hasPrefab == 6 || hasPrefab == 7 || hasPrefab == 15) {
                    if (gpositions[i][j] == 3
                    || gpositions[i][j] == 4
                    || gpositions[i][j] == 5
                    || gpositions[i][j] == 6) {
                        GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    } else {
                        GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    }
                } else if (hasPrefab == 8) {
                    if (gpositions[i][j] == 3
                    || gpositions[i][j] == 4
                    || gpositions[i][j] == 5
                    || gpositions[i][j] == 6) {
                        GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    } else {
                        GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    }
                } else if (hasPrefab == 9) {
                    if (gpositions[i][j] == 3
                    || gpositions[i][j] == 4
                    || gpositions[i][j] == 5
                    || gpositions[i][j] == 6) {
                        GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    } else {
                        GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                        rotor.transform.SetParent(spawnedPrefab.transform);
                    }
                } else if (hasPrefab == 16) {
                    if (spawnedPrefab.TryGetComponent<LeftRollerAnim>(out LeftRollerAnim leftRollerAnim)) {
                        leftRollerAnim.enabled = false;
                        GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Roller_Base").gameObject;
                        tileBaseObject.transform.position = new Vector3(x, 0.55f, z);
                        GameObject canvasObject = new GameObject("Text_Canvas");
                        Canvas canvas = canvasObject.AddComponent<Canvas>();
                        GameObject textObject = new GameObject("Text");
                        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                        textComponent.text = "<~";
                        textComponent.alignment = TextAlignmentOptions.Center;
                        canvas.renderMode = RenderMode.WorldSpace;
                        textComponent.fontStyle = FontStyles.Bold;
                        textComponent.fontSize = 0.4f;
                        canvasObject.transform.SetParent(spawnedPrefab.transform);
                        textObject.transform.SetParent(canvasObject.transform);
                        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                        canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                        canvasObject.transform.position = new Vector3(x, 1f, z);
                    }
                } else if (hasPrefab == 17) {
                    if (spawnedPrefab.TryGetComponent<RightRollerAnim>(out RightRollerAnim rightRollerAnim)) {
                        rightRollerAnim.enabled = false;
                        GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Roller_Base").gameObject;
                        tileBaseObject.transform.position = new Vector3(x, 0.55f, z);
                        GameObject canvasObject = new GameObject("Text_Canvas");
                        Canvas canvas = canvasObject.AddComponent<Canvas>();
                        GameObject textObject = new GameObject("Text");
                        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                        textComponent.text = "~>";
                        textComponent.alignment = TextAlignmentOptions.Center;
                        canvas.renderMode = RenderMode.WorldSpace;
                        textComponent.fontStyle = FontStyles.Bold;
                        textComponent.fontSize = 0.4f;
                        canvasObject.transform.SetParent(spawnedPrefab.transform);
                        textObject.transform.SetParent(canvasObject.transform);
                        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                        canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                        canvasObject.transform.position = new Vector3(x, 1f, z);
                    }
                }
            }
        }
        List<float> themeZPositions = ltdata.themeZPositions;
        Quaternion themetextRotation = Quaternion.Euler(90f, 0f, 0f);
        for (int i = 0; i < themeZPositions.Count; i++) {
            float z = themeZPositions[i];
            Vector3 spawnPosition = new Vector3(-1.4f, 0.1f, z - 0.4f);
            GameObject spawnedPrefab = Instantiate(themeText, spawnPosition, themetextRotation);
            ThemeEditor te = spawnedPrefab.GetComponent<ThemeEditor>();
            te.themeID = ltdata.themeIds[i];
        }

        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < 5; j++) {
                float x = j - 2;
                float z = i * gre.prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                GameObject spawnedPrefab = Instantiate(grid, spawnPosition, Quaternion.identity);
            }
        }
        isInEditor = true;
    }

    public void saveLevelData() {
        for (int i = gdata.positions.Count - 1; i >= 0; i--) {
            List<bool> isZero = new List<bool>();
            for (int j = gdata.positions[i].Count - 1; j >= 0; j--) {
                if (gdata.positions[i][j] == 0) {
                    isZero.Add(true);
                } else {
                    isZero.Add(false);
                }
            }
            if (isZero.All(x => x == true)) {
                gdata.positions.RemoveAt(i);
                edata.positions.RemoveAt(i);
            } else {
                break;
            }
        }
        string groundJsonString = JsonConvert.SerializeObject(gdata);
        string enemyJsonString = JsonConvert.SerializeObject(edata);
        List<int> m_themeIDs = new List<int>();
        List<float> m_themeZPositions = new List<float>();
        GameObject[] m_themes = GameObject.FindGameObjectsWithTag("Theme");
        Vector3 m_themePos = new Vector3(-37f, 0f, 0f);
        foreach (GameObject m_theme in m_themes) {
            if (m_theme.transform.position == m_themePos) {
                continue;
            }
            ThemeEditor te = m_theme.GetComponent<ThemeEditor>();
            m_themeIDs.Add(te.themeID);
            m_themeZPositions.Add(m_theme.transform.position.z + 0.4f);
            Destroy(m_theme);
        }
        float previousZ = 0f;
        int previousID = 0;
        for (int i = 0; i < m_themeZPositions.Count; i++) {
            if (m_themeZPositions[i] < previousZ) {
                m_themeZPositions[i-1] = m_themeZPositions[i];
                m_themeZPositions[i] = previousZ;
                m_themeIDs[i-1] = m_themeIDs[i];
                m_themeIDs[i] = previousID;
            }
            previousZ = m_themeZPositions[i];
            previousID = m_themeIDs[i];
        }
        ltdata.themeIds = m_themeIDs;
        ltdata.themeZPositions = m_themeZPositions;
        string themeJsonString = JsonConvert.SerializeObject(ltdata);
        LevelConfigJson m_config = levelConfig.SaveConfig();
        string configJsonString = JsonConvert.SerializeObject(m_config);
        string SaveFileFolder = Application.persistentDataPath;
        File.WriteAllText(Path.Combine(SaveFileFolder, gre.jsonFilePath + ".json"), groundJsonString);
        File.WriteAllText(Path.Combine(SaveFileFolder, ere.jsonFilePath + ".json"), enemyJsonString);
        File.WriteAllText(Path.Combine(SaveFileFolder, levelConfig.jsonFilePath + ".json"), configJsonString);
        File.WriteAllText(Path.Combine(SaveFileFolder, themeChanger.jsonFilePath + ".json"), themeJsonString);
        Quaternion defaultCameraRotation = Quaternion.Euler(40f, 0f, 0f);
        m_camera.transform.rotation = defaultCameraRotation;
        gre.enabled = true;
        ere.enabled = true;
        themeChanger.enabled = true;
        gre.UpdateData();
        ere.UpdateData();
        themeChanger.UpdateData();
        AudioPlayer audioPlayer = GetComponent<AudioPlayer>();
        audioPlayer.audioPath = levelConfig.musicPath;
        audioPlayer.UpdateAudioClip();
        Vector3 gridPosition = grid.transform.position;
        GameObject[] Grids = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject m_grid in Grids) {
            if (m_grid.transform.position == gridPosition) {
                continue;
            }
            Destroy(m_grid);
        }
        manager.enabled = true;
        manager.RestartGame();
        isInEditor = false;
        gameEditCanvas.SetActive(false);
        balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
    }

    public void OnGoToButtonClick() {
        TMP_InputField offsetInputField = GameObject.Find("GoToInputField").GetComponent<TMP_InputField>();
        int offset = int.Parse(offsetInputField.text);
        GoToPosition(offset);
    }

    public void GoToPosition(int position) {
        m_camera.transform.position = new Vector3(m_camera.transform.position.x, m_camera.transform.position.y, (float)position);
    }

    // scrolls sus
    public void ScrollUp() {
        float camPosZ = m_camera.transform.position.z;
        camPosZ += 0.2f;
        Vector3 camPos = new Vector3(m_camera.transform.position.x, m_camera.transform.position.y, camPosZ);
        m_camera.transform.position = camPos;
    }

    // scrolls jos
    public void ScrollDown() {
        float camPosZ = m_camera.transform.position.z;
        camPosZ -= 0.2f;
        Vector3 camPos = new Vector3(m_camera.transform.position.x, m_camera.transform.position.y, camPosZ);
        if (m_camera.transform.position.z > 0f) {
            m_camera.transform.position = camPos;
        }
    }

    public void SetObjectID(int id) {
        objectID = id;
    }

    public void SetObjectLayer(int layer) {
        objectLayer = layer;
    }

    private void UpdateLevelData(int i, int j) {
        float x = j - 2;
        float z = i * gre.prefabSpacing;
        GameObject[] normalTiles = GameObject.FindGameObjectsWithTag("NormalTile");
        GameObject[] jumpTiles = GameObject.FindGameObjectsWithTag("JumpTile");
        GameObject[] glassTiles = GameObject.FindGameObjectsWithTag("GlassTile");
        GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
        GameObject[] glassTilesGroup1 = GameObject.FindGameObjectsWithTag("GlassGroup1");
        GameObject[] glassTilesGroup2 = GameObject.FindGameObjectsWithTag("GlassGroup2");
        GameObject[] glassTilesGroup3 = GameObject.FindGameObjectsWithTag("GlassGroup3");
        GameObject[] moverTilesGroup1 = GameObject.FindGameObjectsWithTag("MoverGroup1");
        if (objectLayer == 0) {
            foreach (GameObject tile in normalTiles) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in jumpTiles) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in glassTiles) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in glassTilesGroup1) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in glassTilesGroup2) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in glassTilesGroup3) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            foreach (GameObject tile in moverTilesGroup1) {
                if (tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            int hasPrefab = gdata.positions[i][j];
            GameObject spawnedPrefab = Instantiate(gre.prefabs[hasPrefab], new Vector3(x, 0f, z), Quaternion.identity);
            if (hasPrefab == 4) {
                GameObject canvasObject = new GameObject("Text_Canvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                GameObject textObject = new GameObject("Text");
                TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                textComponent.text = "1";
                textComponent.alignment = TextAlignmentOptions.Center;
                canvas.renderMode = RenderMode.WorldSpace;
                textComponent.fontStyle = FontStyles.Bold;
                textComponent.fontSize = 0.4f;
                canvasObject.transform.SetParent(spawnedPrefab.transform);
                textObject.transform.SetParent(canvasObject.transform);
                RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                canvasObject.transform.position = new Vector3(x, 1f, z);
            } else if (hasPrefab == 5) {
                GameObject canvasObject = new GameObject("Text_Canvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                GameObject textObject = new GameObject("Text");
                TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                textComponent.text = "2";
                textComponent.alignment = TextAlignmentOptions.Center;
                canvas.renderMode = RenderMode.WorldSpace;
                textComponent.fontStyle = FontStyles.Bold;
                textComponent.fontSize = 0.4f;
                canvasObject.transform.SetParent(spawnedPrefab.transform);
                textObject.transform.SetParent(canvasObject.transform);
                RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                canvasObject.transform.position = new Vector3(x, 1f, z);
            } else if (hasPrefab == 6) {
                GameObject canvasObject = new GameObject("Text_Canvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                GameObject textObject = new GameObject("Text");
                TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                textComponent.text = "3";
                textComponent.alignment = TextAlignmentOptions.Center;
                canvas.renderMode = RenderMode.WorldSpace;
                textComponent.fontStyle = FontStyles.Bold;
                textComponent.fontSize = 0.4f;
                canvasObject.transform.SetParent(spawnedPrefab.transform);
                textObject.transform.SetParent(canvasObject.transform);
                RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                canvasObject.transform.position = new Vector3(x, 1f, z);
            } else if (hasPrefab == 7) {
                if (spawnedPrefab.TryGetComponent<LeftMovingTileAnim>(out LeftMovingTileAnim leftMovingTileAnim)) {
                    leftMovingTileAnim.enabled = false;
                    GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
                    tileBaseObject.transform.position = new Vector3(x, 0f, z);
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "<~";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                }
            } else if (hasPrefab == 8) {
                if (spawnedPrefab.TryGetComponent<RightMovingTileAnim>(out RightMovingTileAnim rightMovingTileAnim)) {
                    rightMovingTileAnim.enabled = false;
                    GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Normal_Tile_Base").gameObject;
                    tileBaseObject.transform.position = new Vector3(x, 0f, z);
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "~>";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                }
            } else if (hasPrefab == 9) {
                GameObject canvasObject = new GameObject("Text_Canvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                GameObject textObject = new GameObject("Text");
                TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                textComponent.text = "1";
                textComponent.alignment = TextAlignmentOptions.Center;
                canvas.renderMode = RenderMode.WorldSpace;
                textComponent.fontStyle = FontStyles.Bold;
                textComponent.fontSize = 0.4f;
                canvasObject.transform.SetParent(spawnedPrefab.transform);
                textObject.transform.SetParent(canvasObject.transform);
                RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                canvasObject.transform.position = new Vector3(x, 1f, z);
            }
            /*
            if (gdata.positions[i][j] == 1) {
                Instantiate(gre.prefab, new Vector3(x, 0f, z), Quaternion.identity);
            } else if (gdata.positions[i][j] == 2) {
                Instantiate(gre.prefab2, new Vector3(x, 0f, z), Quaternion.identity);
            } else if (gdata.positions[i][j] == 3) {
                Instantiate(gre.prefab3, new Vector3(x, 0f, z), Quaternion.identity);
            } */
        } else if (objectLayer == 1) {
            foreach (GameObject tile in risers) {
                if (tile.transform.position == new Vector3(x, 0.55f, z) || tile.transform.position == new Vector3(x, 0.2f, z) || tile.transform.position == new Vector3(x, 0f, z)) {
                    Destroy(tile);
                    break;
                } else if (tile.transform.position == new Vector3(x + 1f, 0.2f, z) || tile.transform.position == new Vector3(x - 1f, 0.2f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            int hasPrefab = edata.positions[i][j];
            Vector3 spawnPosition = new Vector3(x, 0.55f, z);
            if (hasPrefab == 4 || hasPrefab == 5 || (hasPrefab >= 10 && hasPrefab <= 15)) {
                spawnPosition = new Vector3(x, 0f, z);
            } else if (hasPrefab == 6 || hasPrefab == 7) {
                spawnPosition = new Vector3(x, 0.2f, z);
            } else if (hasPrefab == 8) {
                spawnPosition = new Vector3(x + 1f, 0.2f, z);
            } else if (hasPrefab == 9) {
                spawnPosition = new Vector3(x - 1f, 0.2f, z);
            }
            GameObject spawnedPrefab = Instantiate(ere.prefabs[hasPrefab], spawnPosition, Quaternion.identity);
            if (hasPrefab == 3) {
                GameObject canvasObject = new GameObject("Text_Canvas");
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                GameObject textObject = new GameObject("Text");
                TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                textComponent.text = "SUS";
                textComponent.alignment = TextAlignmentOptions.Center;
                canvas.renderMode = RenderMode.WorldSpace;
                textComponent.fontStyle = FontStyles.Bold;
                textComponent.fontSize = 0.4f;
                canvasObject.transform.SetParent(spawnedPrefab.transform);
                textObject.transform.SetParent(canvasObject.transform);
                RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                canvasObject.transform.position = new Vector3(x, 2f, z);
            } else if (hasPrefab == 6 || hasPrefab == 7 || hasPrefab == 15) {
                List<List<int>> gpositions = gdata.positions;
                if (gpositions[i][j] == 3
                || gpositions[i][j] == 4
                || gpositions[i][j] == 5
                || gpositions[i][j] == 6) {
                    GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 8) {
                List<List<int>> gpositions = gdata.positions;
                if (gpositions[i][j] == 3
                || gpositions[i][j] == 4
                || gpositions[i][j] == 5
                || gpositions[i][j] == 6) {
                    GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 9) {
                List<List<int>> gpositions = gdata.positions;
                if (gpositions[i][j] == 3
                || gpositions[i][j] == 4
                || gpositions[i][j] == 5
                || gpositions[i][j] == 6) {
                    GameObject rotor = Instantiate(ere.glassRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(ere.groundRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 16) {
                if (spawnedPrefab.TryGetComponent<LeftRollerAnim>(out LeftRollerAnim leftRollerAnim)) {
                    leftRollerAnim.enabled = false;
                    GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Roller_Base").gameObject;
                    tileBaseObject.transform.position = new Vector3(x, 0.55f, z);
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "<~";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                }
            } else if (hasPrefab == 17) {
                if (spawnedPrefab.TryGetComponent<RightRollerAnim>(out RightRollerAnim rightRollerAnim)) {
                    rightRollerAnim.enabled = false;
                    GameObject tileBaseObject = spawnedPrefab.transform.Find("DeceBalus_Roller_Base").gameObject;
                    tileBaseObject.transform.position = new Vector3(x, 0.55f, z);
                    GameObject canvasObject = new GameObject("Text_Canvas");
                    Canvas canvas = canvasObject.AddComponent<Canvas>();
                    GameObject textObject = new GameObject("Text");
                    TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
                    textComponent.text = "~>";
                    textComponent.alignment = TextAlignmentOptions.Center;
                    canvas.renderMode = RenderMode.WorldSpace;
                    textComponent.fontStyle = FontStyles.Bold;
                    textComponent.fontSize = 0.4f;
                    canvasObject.transform.SetParent(spawnedPrefab.transform);
                    textObject.transform.SetParent(canvasObject.transform);
                    RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
                    canvasObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    canvasRectTransform.sizeDelta = new Vector2(1.2f, 1.2f);
                    canvasObject.transform.position = new Vector3(x, 1f, z);
                }
            }
        }
    }

    public void SetPopupOpen(bool open) {
        isPopupOpen = open;
    }

    public void ClearThemes() {
        GameObject[] m_themes = GameObject.FindGameObjectsWithTag("Theme");
        Vector3 m_themePos = new Vector3(-37f, 0f, 0f);
        foreach (GameObject m_theme in m_themes) {
            if (m_theme.transform.position == m_themePos) {
                continue;
            }
            Destroy(m_theme);
        }
        GameObject first_theme = Instantiate(themeText, new Vector3(-1.4f, 0.1f, -0.4f), Quaternion.Euler(90f, 0f, 0f));
    }

    public void ClearLevel() {
        List<List<int>> groundNewPositions = new List<List<int>>();
        for (int i = 0; i < 10; i++) {
            groundNewPositions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
        }
        List<List<int>> enemyNewPositions = new List<List<int>>();
        for (int i = 0; i < 10; i++) {
            enemyNewPositions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
        }
        gdata.positions = groundNewPositions;
        edata.positions = enemyNewPositions;
        ClearEverything();
        ClearThemes();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_camera.transform.position.z + 6.9f >= gridSize && isInEditor) {
            for (int i = 0; i < 5; i++) {
                float x = i - 2;
                float z = gridSize * gre.prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                GameObject spawnedPrefab = Instantiate(grid, spawnPosition, Quaternion.identity);
            }
            gridSize++;
        } else if (Input.GetMouseButton(0) && isInEditor && !isPopupOpen) {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = m_camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 9.5f));
            //Debug.Log(worldPos);
            worldPos.z = Mathf.Round(worldPos.z);
            worldPos.x = Mathf.Round(worldPos.x);
            int i = (int)worldPos.z;
            int j = (int)worldPos.x + 2;
            //Debug.Log(i + " " + j);
            if (objectLayer == 0) {
                if (i >= gdata.positions.Count) {
                    for (int k = 0; k <= i - gdata.positions.Count; k++) {
                        gdata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                        edata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                    }
                }
                if (i >= 0 && i < gdata.positions.Count && j >= 0 && j < 5) {
                    gdata.positions[i][j] = objectID;
                    UpdateLevelData(i, j);
                }
            } else if (objectLayer == 1) {
                if (i >= edata.positions.Count) {
                    for (int k = 0; k <= i - gdata.positions.Count; k++) {
                        gdata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                        edata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                    }
                }
                if (i >= 0 && i < edata.positions.Count && j >= 0 && j < 5) {
                    edata.positions[i][j] = objectID;
                    UpdateLevelData(i, j);
                }
            } else if (objectLayer == 2) {
                if (objectID == 0 && i >= 0 && j >= 0 && j < 5) {
                    int k = 0;
                    bool themeAlreadyRemoved = false;
                    GameObject[] m_themes = GameObject.FindGameObjectsWithTag("Theme");
                    foreach (GameObject m_theme in m_themes) {
                        if (m_theme.transform.position == new Vector3(-1.4f, 0.1f, i - 0.4f)) {
                            Destroy(m_theme);
                            themeAlreadyRemoved = false;
                            break;
                        } else {
                            themeAlreadyRemoved = true;
                        }
                        k++;
                    }
                    //Debug.Log(ltdata.themeZPositions.Count);
                    //Debug.Log(k);
                    if (!themeAlreadyRemoved) {
                        ltdata.themeZPositions.RemoveAt(k-1);
                        ltdata.themeIds.RemoveAt(k-1);
                    }
                }
                if (objectID == 1 && i >= 0 && j >= 0 && j < 5) {
                    bool themeAlreadyExists = false;
                    GameObject[] m_themes = GameObject.FindGameObjectsWithTag("Theme");
                    GameObject currentTheme = null;
                    foreach (GameObject m_theme in m_themes) {
                        Debug.Log(m_theme.transform.position);
                        if (m_theme.transform.position == new Vector3(-1.4f, 0.1f, i - 0.4f)) {
                            themeAlreadyExists = true;
                            currentTheme = m_theme;
                            break;
                        }
                    }
                    if (!themeAlreadyExists) {
                        ltdata.themeZPositions.Add(i);
                        ltdata.themeIds.Add(0);
                        currentTheme = Instantiate(themeText, new Vector3(-1.4f, 0.1f, i - 0.4f), Quaternion.Euler(90f, 0f, 0f));
                        ThemeEditor te = currentTheme.GetComponent<ThemeEditor>();
                        te.themeID = 0;
                    } else {
                        ThemeEditor te = currentTheme.GetComponent<ThemeEditor>();
                        te.OpenEditThemePanel();
                    }
                }
            }
        } else if (objectLayer == 0) {
            tilesPanel.SetActive(true);
            obstaclesPanel.SetActive(false);
            themesPanel.SetActive(false);
        } else if (objectLayer == 1) {
            tilesPanel.SetActive(false);
            obstaclesPanel.SetActive(true);
            themesPanel.SetActive(false);
        } else if (objectLayer == 2) {
            tilesPanel.SetActive(false);
            obstaclesPanel.SetActive(false);
            themesPanel.SetActive(true);
        }
        if (isInEditor) {
            GameObject[] mhn_themes = GameObject.FindGameObjectsWithTag("Theme");
            for (int i = 1; i < mhn_themes.Length; i++) {
                if (mhn_themes[i].transform.position.z < m_camera.transform.position.z && m_camera.transform.position.z > mhn_themes[i-1].transform.position.z) {
                    ThemeEditor te = mhn_themes[i].GetComponent<ThemeEditor>();
                    themeChanger2.UpdateTheme(te.themeID);
                }
            }
        }
    }
}
