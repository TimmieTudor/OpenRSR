using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public GameObject balus;
    public GameObject gameOverPanel;
    public GameObject gamePlayCanvas;
    public GameObject settingsPanel;
    public GroundRenderer gre;
    public GameFreeze GFreeze;
    public CameraFollow CFollow;
    public string geoBufferJsonFilePath = "LevelData/GeoBuffer1.json";
    private GameObject percentTextLabel;
    private TextMeshProUGUI percentTextMesh;
    private string realPercent;
    public EnemyRenderer ere;
    private LevelThemeChanger themeChanger;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private ThemeChanger themeChanger2;
    private GameObject levelRenderer;
    public LevelConfigurator levelConfig;
    private LevelEditor levelEdit;
    public bool isGameOver = false;
    public bool isDataDownloaded = false;
    private bool isDataDownloadedCache = false;
    public bool isGamePaused = true;
    public bool isInMainMenu = true;
    private Rigidbody rb;
    private AudioPlayer audioPlayer;
    public bool isDeathDisabled = false;
    private IEnumerator updateCacheCoroutine;
    private ObjectPool objectPool;
    private GeoBufferJson geoBufferJson;
    private GameObject mainMenuNormalTile;
    public int num_collectedGems = 0;
    private int totalGemCount = 0;
    private List<GameObject> collectedGems = new List<GameObject>();
    private GameObject levelEditButton;
    private GameObject levelEndObject;
    private GameObject mainMenuCanvas;
    public bool hasFallen = false;
    public bool hasHitObstacle = false;
    public bool delayed = false;
    public static GameManager instance;
    IEnumerator UpdateCache()
    {
        while (true)
        {
            // Update the cached value
            isDataDownloadedCache = !isDataDownloadedCache;

            // Wait for a few seconds before updating again
            yield return new WaitForSeconds(1f);
        }
    }
    void Start()
    {
        instance = this;
        percentTextLabel = GameObject.Find("Percent");
        percentTextMesh = percentTextLabel.GetComponent<TextMeshProUGUI>();
        balus = GameObject.FindGameObjectWithTag("Balus");
        levelRenderer = GameObject.Find("LevelRenderer");
        GameObject objPoolObj = GameObject.Find("ObjectPool");
        objectPool = objPoolObj.GetComponent<ObjectPool>();
        rb = GetComponent<Rigidbody>();
        sphd = GetComponent<SphereDragger>();
        themeChanger = levelRenderer.GetComponent<LevelThemeChanger>();
        sphm = GetComponent<SphereMovement>();
        themeChanger2 = levelRenderer.GetComponent<ThemeChanger>();
        levelConfig = levelRenderer.GetComponent<LevelConfigurator>();
        audioPlayer = balus.GetComponent<AudioPlayer>();
        sphm.speed = levelConfig.levelSpeed;
        audioPlayer.audioPath = levelConfig.musicPath;
        audioPlayer.LoadAudioClip();
        levelEdit = GetComponent<LevelEditor>();
        levelEditButton = GameObject.Find("EditButton");
        levelEndObject = GameObject.Find("End");
        mainMenuCanvas = GameObject.Find("MainMenu");

        //Application.targetFrameRate = 60;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config1.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer1.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground2.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies2.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes2.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config2.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer2.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music2.mp3"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "MenuData/MenuData.json")))
        {
            isDataDownloaded = true;
            isDataDownloadedCache = true;
        }
        else
        {
            isDataDownloaded = false;
            isDataDownloadedCache = true;
        }
        updateCacheCoroutine = UpdateCache();
        StartCoroutine(updateCacheCoroutine);

        isGamePaused = GFreeze.gamePaused;

        if (isDataDownloaded)
        {
            CFollow.enabled = false;
            Camera.main.transform.position = new Vector3(0f, 2.25f, -2f);
            GFreeze.enabled = false;
            gre.enabled = false;
            ere.enabled = false;
            themeChanger.enabled = false;
            sphm.enabled = false;
            sphd.enabled = false;
            themeChanger2.enabled = true;
            string jsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
            geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(jsonString);
            objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
            gamePlayCanvas.SetActive(false);
        }
        else 
        {
            CFollow.enabled = false;
            Camera.main.transform.position = new Vector3(0f, 2.25f, -2f);
            GFreeze.enabled = false;
            rb.useGravity = false;
            gre.enabled = false;
            ere.enabled = false;
            themeChanger.enabled = false;
            sphm.enabled = false;
            sphd.enabled = false;
            themeChanger2.enabled = false;
            LoadData();
            gamePlayCanvas.SetActive(false);
            themeChanger.enabled = true;
            themeChanger2.enabled = true;
            themeChanger2.UpdateTheme(themeChanger.themeID);
            string jsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
            geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(jsonString);
            objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
            isDataDownloadedCache = false;
            isDataDownloaded = true;
        }
        mainMenuNormalTile = Instantiate(gre.prefabs[1], new Vector3(0f, 0f, rb.position.z), Quaternion.identity);
    }

    public void ExitToMainMenu() {
        themeChanger.themeID = 0;
        themeChanger2.UpdateTheme(themeChanger.themeID);
        if (levelConfig.startPortal) {
            levelConfig.startPortalObject2.SetActive(false);
        }
        CFollow.enabled = false;
        Camera.main.transform.position = new Vector3(0f, 2.25f, -2f);
        audioPlayer.SeekToZero();
        sphm.ClearFallingObstacles();
        objectPool.ClearAllPools();
        gre.ClearPrefabPositions();
        ere.ClearPrefabPositions();
        GFreeze.enabled = false;
        gre.enabled = false;
        ere.enabled = false;
        themeChanger.enabled = false;
        sphm.enabled = false;
        sphd.enabled = false;
        themeChanger2.enabled = true;
        string jsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(jsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        gamePlayCanvas.SetActive(false);
        levelEdit.ClearEverything();
        balus.transform.position = new Vector3(0f, 0.5f, 0f);
        rb.position = new Vector3(0f, 0.5f, 0f);
        mainMenuNormalTile = Instantiate(gre.prefabs[1], new Vector3(0f, 0f, rb.position.z), Quaternion.identity);
        MainMenuScripts.instance.ResetMenu();
        mainMenuCanvas.SetActive(true);
        SetIsInMainMenu(true);
    }

    void Update()
    {
        if (!isDataDownloadedCache) {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground1.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies1.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes1.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config1.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer1.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground2.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies2.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes2.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config2.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer2.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music2.mp3"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "MenuData/MenuData.json")))
            {
                isDataDownloaded = true;
                StopCoroutine(updateCacheCoroutine);
                isDataDownloadedCache = true;
            }
            else
            {
                isDataDownloaded = false;
                isDataDownloadedCache = true;
            }
        }
        isGamePaused = GFreeze.gamePaused;
        
        if (isDataDownloaded && !isGameOver && !isGamePaused)
        {
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            //sphm.enabled = false;
            GFreeze.enabled = true;
            isGamePaused = GFreeze.gamePaused;
            //sphd.enabled = true;
            themeChanger2.enabled = true;
        } else if (isDataDownloaded && isGamePaused) {

        }
        else
        {
            gre.enabled = false;
            ere.enabled = false;
            themeChanger.enabled = false;
            sphm.enabled = false;
            sphd.enabled = false;
            themeChanger2.enabled = false;
        }
        if (isGameOver) {
            gre.enabled = false;
            ere.enabled = false;
            themeChanger.enabled = false;
            sphm.enabled = false;
            sphd.enabled = false;
            themeChanger2.enabled = false;
            rb.velocity = Vector3.zero;
        }

        sphm.speed = levelConfig.levelSpeed;

        float balusPercent = (balus.transform.position.z / (float)gre.positionsCount) * 100f;
        balusPercent = Mathf.Clamp(balusPercent, 0f, 100f);
        realPercent = Math.Round(balusPercent).ToString() + "%";
        percentTextMesh.SetText(realPercent);

        // Check if Balus falls under Y position 0
        if (balus.transform.position.y < 0f && !isGameOver && !isDeathDisabled && !delayed)
        {
            hasFallen = true;
            StartCoroutine(DelayGameOver(realPercent, 0.5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isGameOver && !isDeathDisabled)
        {
            hasHitObstacle = true;
            StartCoroutine(DelayGameOver(realPercent, 0.1f));
        } else if (other.gameObject.CompareTag("DiamondCollision") && !isGameOver) {
            GameObject diamondParent = other.gameObject.transform.parent.gameObject;
            SoundPlayer snd = diamondParent.GetComponent<SoundPlayer>();
            snd.PlayAudio();
            GameObject diamond1stChild = diamondParent.transform.GetChild(0).gameObject;
            if (diamond1stChild.activeSelf) {
                num_collectedGems++;
            }
            diamond1stChild.SetActive(false);
        } else if (other.gameObject.CompareTag("MoverArrowCollision") && !isGameOver) {
            Vector3 direction = Vector3.zero;
            GameObject otherParent = other.gameObject.transform.parent.gameObject;
            GameObject otherActive = otherParent.transform.GetChild(0).gameObject;
            GameObject otherNormal = otherParent.transform.GetChild(1).gameObject;
            if (otherNormal.transform.rotation.eulerAngles.y == 0f) {
                direction = Vector3.forward;
            } else if (otherNormal.transform.rotation.eulerAngles.y == 90f) {
                direction = Vector3.right;
            } else if (otherNormal.transform.rotation.eulerAngles.y == 180f) {
                direction = Vector3.back;
            } else if (otherNormal.transform.rotation.eulerAngles.y == 270f) {
                direction = Vector3.left;
            }
            GameObject[] moverTiles = GameObject.FindGameObjectsWithTag("MoverCollisionGroup1");
            foreach (GameObject tile in moverTiles) {
                if (tile.transform.position.x == other.gameObject.transform.position.x && tile.transform.position.z == other.gameObject.transform.position.z) {
                    sphm.ActivateMoverTilesG1(direction);
                    break;
                }
            }
            GameObject[] moverTiles2 = GameObject.FindGameObjectsWithTag("MoverCollisionGroup2");
            foreach (GameObject tile in moverTiles2) {
                if (tile.transform.position.x == other.gameObject.transform.position.x && tile.transform.position.z == other.gameObject.transform.position.z) {
                    sphm.ActivateMoverTilesG2(direction);
                    break;
                }
            }
            GameObject[] moverTiles3 = GameObject.FindGameObjectsWithTag("MoverCollisionGroup3");
            foreach (GameObject tile in moverTiles3) {
                if (tile.transform.position.x == other.gameObject.transform.position.x && tile.transform.position.z == other.gameObject.transform.position.z) {
                    sphm.ActivateMoverTilesG3(direction);
                    break;
                }
            }
            //otherParent.transform.Translate(direction);
            otherActive.SetActive(true);
            otherNormal.SetActive(false);
            other.gameObject.SetActive(false);
        } else if (other.gameObject.CompareTag("LevelEnd") && !isGameOver) {
            rb.position = new Vector3(rb.position.x, 0.5f, rb.position.z - 0.1f);
            sphm.isNotFalling = true;
            GameOver(realPercent);
        }
    }

    public void SetIsInMainMenu(bool isInMainMenu)
    {
        this.isInMainMenu = isInMainMenu;
    }

    public void LoadLevel(int level) {
        gre.jsonFilePath = "LevelData/Ground" + level.ToString();
        ere.jsonFilePath = "LevelData/Enemies" + level.ToString();
        themeChanger.jsonFilePath = "LevelData/Themes" + level.ToString();
        levelConfig.jsonFilePath = "LevelData/Config" + level.ToString();
        levelConfig.LoadLevelConfig();
        audioPlayer.audioPath = levelConfig.musicPath;
        audioPlayer.UpdateAudioClip();
        themeChanger.UpdateData();
        CFollow.enabled = true;
        gre.enabled = true;
        ere.enabled = true;
        ere.Initialize();
        gre.Initialize();
        totalGemCount = ere.CountEnemies(18);
        num_collectedGems = 0;
        themeChanger.enabled = true;
        sphm.enabled = false;
        GFreeze.enabled = true;
        sphd.enabled = true;
        themeChanger2.enabled = true;
        isGamePaused = GFreeze.gamePaused;
        gamePlayCanvas.SetActive(true);
        gameOverPanel.SetActive(false);
        Destroy(mainMenuNormalTile);
        geoBufferJsonFilePath = "LevelData/GeoBuffer" + level.ToString() + ".json";
        string geoBufferJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(geoBufferJsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        if (levelConfig.startPortal) {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        } else {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        }
        levelEditButton.GetComponent<Button>().onClick.AddListener(() => LoadLevelInEditor(level));
    }

    public void LoadLevelInEditor(int level) {
        gre.jsonFilePath = "LevelData/Ground" + level.ToString();
        ere.jsonFilePath = "LevelData/Enemies" + level.ToString();
        themeChanger.jsonFilePath = "LevelData/Themes" + level.ToString();
        levelConfig.jsonFilePath = "LevelData/Config" + level.ToString();
        levelConfig.LoadLevelConfig();
        audioPlayer.audioPath = levelConfig.musicPath;
        audioPlayer.UpdateAudioClip();
        themeChanger.UpdateData();
        CFollow.enabled = true;
        gre.enabled = true;
        ere.enabled = true;
        ere.Initialize();
        gre.Initialize();
        totalGemCount = ere.CountEnemies(18);
        num_collectedGems = 0;
        themeChanger.enabled = true;
        sphm.enabled = false;
        GFreeze.enabled = true;
        sphd.enabled = true;
        themeChanger2.enabled = true;
        isGamePaused = GFreeze.gamePaused;
        gamePlayCanvas.SetActive(true);
        gameOverPanel.SetActive(false);
        Destroy(mainMenuNormalTile);
        geoBufferJsonFilePath = "LevelData/GeoBuffer" + level.ToString() + ".json";
        string geoBufferJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(geoBufferJsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        if (levelConfig.startPortal) {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        } else {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        }
        levelEditButton.GetComponent<Button>().onClick.AddListener(() => LoadLevelInEditor(level));
        levelEndObject.transform.position = new Vector3(-90f, 0f, 0f);
        levelEdit.editorTransition();
    }

    public void CreateNewLevel(int previousLevel) {
        StreamWriter configWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Config{previousLevel+1}.json"));
        configWriter.Write(@"{""level_name"":""Unnamed Level"",""level_author"":""Anonymous"",""level_speed"":7.55,""start_pos"":0,""music_path"":""Music/Music1"",""worldshow_path"":null,""start_portal"":false}");
        configWriter.Close();
        StreamWriter groundWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Ground{previousLevel+1}.json"));
        groundWriter.Write(@"{""positions"":[[0,0,0,0,0]]}");
        groundWriter.Close();
        StreamWriter enemiesWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Enemies{previousLevel+1}.json"));
        enemiesWriter.Write(@"{""positions"":[[0,0,0,0,0]]}");
        enemiesWriter.Close();
        StreamWriter themeWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Themes{previousLevel+1}.json"));
        themeWriter.Write(@"{""themeZPositions"":[0],""themeIds"":[0]}");
        themeWriter.Close();
        StreamWriter geoBufferWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"GeoBuffer{previousLevel+1}.json"));
        geoBufferWriter.Write(@"{""ground"":[200,200,200,200,200,200,200,190,190,190,190,190],""enemies"":[200,190,190,190,190,190,195,195,195,195,190,190,195,190,190,190,190,190,20,125]}");
        geoBufferWriter.Close();
        LoadLevelInEditor(previousLevel+1);
    }

    public void SetDeathDisabled(bool isDisabled)
    {
        isDeathDisabled = isDisabled;
    }

    public void ShowSettingsPanel() {
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel() {
        settingsPanel.SetActive(false);
    }

    public IEnumerator DelayGameOver(string percent, float delay) {
        delayed = true;
        yield return new WaitForSeconds(delay);
        GameOver(percent);
        delayed = false;
    }

    void GameOver(string percent)
    {
        isGameOver = true;
        //Time.timeScale = 0f;
        rb.velocity = Vector3.zero;
        gameOverPanel.SetActive(true);
        GameObject percentTextLabel2 = GameObject.Find("Percent2");
        TextMeshProUGUI percentTextMesh2 = percentTextLabel2.GetComponent<TextMeshProUGUI>();
        percentTextMesh2.SetText(percent);
        GameObject gemCountTextLabel = GameObject.Find("GemCountText");
        TextMeshProUGUI gemCountTextMesh = gemCountTextLabel.GetComponent<TextMeshProUGUI>();
        gemCountTextMesh.SetText($"{num_collectedGems}/{totalGemCount}");
        GameObject levelNameTextLabel = GameObject.Find("LevelName");
        TextMeshProUGUI levelNameTextMesh = levelNameTextLabel.GetComponent<TextMeshProUGUI>();
        levelNameTextMesh.SetText(levelConfig.levelName);
        GameObject levelAuthorTextLabel = GameObject.Find("LevelAuthor");
        TextMeshProUGUI levelAuthorTextMesh = levelAuthorTextLabel.GetComponent<TextMeshProUGUI>();
        levelAuthorTextMesh.SetText(levelConfig.levelAuthor);
        gamePlayCanvas.SetActive(false);
        sphm.isJumping = true;
        sphm.isNotFalling = true;
        sphd.enabled = false;
        sphm.enabled = false;
        //CFollow.enabled = false;
        GFreeze.PauseGame();
        isGamePaused = GFreeze.gamePaused;
        //GFreeze.enabled = false;
        audioPlayer.PauseAudio();
        //rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void RestartGame()
    {
        sphm.isJumping = false;
        hasFallen = false;
        objectPool.ClearAllPools();
        num_collectedGems = 0;
        string jsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(jsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        //sphm.enabled = false;
        //sphd.enabled = false;
        //CFollow.enabled = false;
        gre.enabled = false;
        ere.enabled = false;
        gamePlayCanvas.SetActive(true);
        gameOverPanel.SetActive(false);
        Vector3 balusPos = levelConfig.startPortal ? new Vector3(0f, 0.5f, levelConfig.startPos) : new Vector3(0f, 0.5f, levelConfig.startPos);
        gre.enabled = true;
        ere.enabled = true;
        themeChanger.enabled = true;
        themeChanger.themeID = 0;
        themeChanger2.enabled = true;
        themeChanger2.UpdateTheme(themeChanger.themeID);
        levelEdit.ClearEverything();
        gre.ClearPrefabPositions();
        ere.ClearPrefabPositions();
        GFreeze.enabled = true;
        //Time.timeScale = 1f;
        sphd.enabled = true;
        CFollow.enabled = true;
        audioPlayer.SeekToZero();
        //rb.velocity = Vector3.zero;
        rb.position = balusPos;
        balus.transform.position = balusPos;
        sphm.collisionZ = levelConfig.startPos + 0.5f;
        sphm.isJumping = true;
        sphm.isNotFalling = true;
        GFreeze.PauseGame();
        //rb.velocity = Vector3.zero;
        //rb.isKinematic = false;
        //rb.position = new Vector3(0f, 0.5f, 0f);
        //sphm.enabled = true;
        sphm.ClearFallingObstacles();
        isGameOver = false;
    }

    public void EnsureCorrectPosAfterRestart() {
        RestartGame();
        if (levelConfig.startPortal) {
            rb.position = new Vector3(0f, 0.55f, levelConfig.startPos);
            balus.transform.position = new Vector3(0f, 0.55f, levelConfig.startPos);
            sphm.isJumping = true;
            sphm.isNotFalling = true;
            if (!sphm.isNotFalling) {
                Debug.Log("Error");
            }
        } else {
            rb.position = new Vector3(0f, 0.5f, levelConfig.startPos);
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
            if (rb.position != new Vector3(0f, 0.5f, levelConfig.startPos)) {
                Debug.Log("Error");
            }
        }
    }

    private void LoadData()
    {
        TextAsset ground1 = Resources.Load<TextAsset>("LevelData/Ground1");
        TextAsset enemies1 = Resources.Load<TextAsset>("LevelData/Enemies1");
        TextAsset theme1 = Resources.Load<TextAsset>("LevelData/Themes1");
        TextAsset theme2 = Resources.Load<TextAsset>("ThemeData/ThemeData");
        TextAsset config1 = Resources.Load<TextAsset>("LevelData/Config1");
        TextAsset geoBuffer1 = Resources.Load<TextAsset>("LevelData/GeoBuffer1");
        TextAsset ground2 = Resources.Load<TextAsset>("LevelData/Ground2");
        TextAsset enemies2 = Resources.Load<TextAsset>("LevelData/Enemies2");
        TextAsset theme3 = Resources.Load<TextAsset>("LevelData/Themes2");
        TextAsset config2 = Resources.Load<TextAsset>("LevelData/Config2");
        TextAsset geoBuffer2 = Resources.Load<TextAsset>("LevelData/GeoBuffer2");
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "LevelData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "ThemeData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Backgrounds"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Music"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "WorldShow"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "MenuData"));
        StreamWriter groundWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Ground1.json"));
        groundWriter.Write(ground1.text);
        groundWriter.Close();
        StreamWriter enemiesWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Enemies1.json"));
        enemiesWriter.Write(enemies1.text);
        enemiesWriter.Close();
        StreamWriter themeWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Themes1.json"));
        themeWriter.Write(theme1.text);
        themeWriter.Close();
        StreamWriter theme2Writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"));
        theme2Writer.Write(theme2.text);
        theme2Writer.Close();
        StreamWriter configWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Config1.json"));
        configWriter.Write(config1.text);
        configWriter.Close();
        StreamWriter geoBufferWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer1.json"));
        geoBufferWriter.Write(geoBuffer1.text);
        geoBufferWriter.Close();
        StreamWriter groundWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Ground2.json"));
        groundWriter2.Write(ground2.text);
        groundWriter2.Close();
        StreamWriter enemiesWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Enemies2.json"));
        enemiesWriter2.Write(enemies2.text);
        enemiesWriter2.Close();
        StreamWriter themeWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Themes2.json"));
        themeWriter2.Write(theme3.text);
        themeWriter2.Close();
        StreamWriter configWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Config2.json"));
        configWriter2.Write(config2.text);
        configWriter2.Close();
        StreamWriter geoBufferWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer2.json"));
        geoBufferWriter2.Write(geoBuffer2.text);
        geoBufferWriter2.Close();
        Texture2D background1 = Resources.Load<Texture2D>("Backgrounds/Background1");
        byte[] data = background1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"), data);
        Texture2D enemy1 = Resources.Load<Texture2D>("Enemy1");
        byte[] enemyData = enemy1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy1.png"), enemyData);
        Texture2D general1 = Resources.Load<Texture2D>("General1");
        byte[] generalData = general1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General1.png"), generalData);
        Texture2D background2 = Resources.Load<Texture2D>("Backgrounds/Background2");
        byte[] data2 = background2.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"), data2);
        Texture2D enemy2 = Resources.Load<Texture2D>("Enemy2");
        byte[] enemyData2 = enemy2.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy2.png"), enemyData2);
        Texture2D general2 = Resources.Load<Texture2D>("General2");
        byte[] generalData2 = general2.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General2.png"), generalData2);
        Texture2D background3 = Resources.Load<Texture2D>("Backgrounds/Background3");
        byte[] data3 = background3.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background3.png"), data3);
        Texture2D enemy3 = Resources.Load<Texture2D>("Enemy3");
        byte[] enemyData3 = enemy3.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy3.png"), enemyData3);
        Texture2D general3 = Resources.Load<Texture2D>("General3");
        byte[] generalData3 = general3.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General3.png"), generalData3);
        Texture2D background6 = Resources.Load<Texture2D>("Backgrounds/Background6");
        byte[] data6 = background6.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background6.png"), data6);
        Texture2D enemy6 = Resources.Load<Texture2D>("Enemy6");
        byte[] enemyData6 = enemy6.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy6.png"), enemyData6);
        Texture2D general6 = Resources.Load<Texture2D>("General6");
        byte[] generalData6 = general6.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General6.png"), generalData6);
        AudioClip music1 = Resources.Load<AudioClip>("Music/Music1");
        byte[] mp3 = WavToMp3.ConvertWavToMp3(music1, 128);
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"), mp3);
        AudioClip music2 = Resources.Load<AudioClip>("Music/Music2");
        byte[] mp32 = WavToMp3.ConvertWavToMp3(music2, 128);
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Music/Music2.mp3"), mp32);
        Texture2D worldShow1 = Resources.Load<Texture2D>("WorldShow/World1");
        byte[] worldShowData = worldShow1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "WorldShow/World1.png"), worldShowData);
        string menuDataJson = Resources.Load<TextAsset>("MenuData/MenuData").text;
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "MenuData/MenuData.json"), menuDataJson);
        isDataDownloaded = true;
    }
}
