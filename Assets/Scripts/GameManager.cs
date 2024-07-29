using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using OpenRSR.Animation;

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
    public SphereMovement sphm;
    public NonSphereMovement nsm;
    private SphereDragger sphd;
    public ThemeChanger themeChanger2;
    private GameObject levelRenderer;
    public LevelConfigurator levelConfig;
    public LevelEditor levelEdit;
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
    public bool isPlayingAnimation = false;
    public bool isPlayingAnimationGroup = false;
    public bool isPlayingObjectAnimation = false;
    public string currentlyPlayingAnimation = "";
    public string currentlyPlayingAnimationGroup = "";
    public BaseObject currentlyPlayingObjectAnimation = null;
    public int minAnimationCount = 0;
    public static GameManager instance;
    public Scene currentScene;
    public Dictionary<string, FrameAnim> anims = new Dictionary<string, FrameAnim>();
    public Dictionary<string, List<FrameAnim>> animGroups = new Dictionary<string, List<FrameAnim>>();
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
    void Awake() {
        instance = this;
    }
    void Start()
    {
        instance = this;
        percentTextLabel = GameObject.Find("Percent");
        percentTextMesh = percentTextLabel.GetComponent<TextMeshProUGUI>();
        balus = GameObject.FindGameObjectWithTag("Balus");
        levelRenderer = GameObject.Find("LevelRenderer");
        GFreeze = levelRenderer.GetComponent<GameFreeze>();
        GameObject objPoolObj = GameObject.Find("ObjectPool");
        objectPool = objPoolObj.GetComponent<ObjectPool>();
        rb = balus.GetComponent<Rigidbody>();
        sphd = balus.GetComponent<SphereDragger>();
        themeChanger = levelRenderer.GetComponent<LevelThemeChanger>();
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "DebugScene") {
            nsm = GetComponent<NonSphereMovement>();
        }
        sphm = balus.GetComponent<SphereMovement>();
        themeChanger2 = levelRenderer.GetComponent<ThemeChanger>();
        levelConfig = levelRenderer.GetComponent<LevelConfigurator>();
        audioPlayer = balus.GetComponent<AudioPlayer>();
        if (sphm != null) {
        sphm.speed = levelConfig.levelSpeed;
        }
        audioPlayer.audioPath = levelConfig.musicPath;
        audioPlayer.LoadAudioClip();
        levelEdit = balus.GetComponent<LevelEditor>();
        levelEditButton = GameObject.Find("EditButton");
        levelEndObject = GameObject.Find("End");
        mainMenuCanvas = GameObject.Find("MainMenu");

        BaseAnim[] baseAnims = FindObjectsByType<BaseAnim>(FindObjectsSortMode.None);
        foreach (BaseAnim baseAnim in baseAnims) {
            foreach (FrameAnim anim in baseAnim.animators) {
                anims[anim.name.name] = anim;
            }
        }

        //Application.targetFrameRate = 60;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground_valea.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_valea.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes_valea.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config_valea.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_valea.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground_aperta.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_aperta.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes_aperta.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config_aperta.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_aperta.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background4.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background5.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy4.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy5.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General1.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General2.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General3.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General4.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General5.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "General6.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music2.mp3"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World_valea.png"))
        && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World_aperta.png"))
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
            if (sphm != null) {
            sphm.enabled = false;
            }
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
        //Debug.Log(anims.Count);
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
        if (sphm != null) sphm.ClearFallingObstacles();
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
            if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground_valea.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_valea.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes_valea.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config_valea.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_valea.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground_aperta.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_aperta.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes_aperta.json")) 
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Config_aperta.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_aperta.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background4.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background5.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Backgrounds/Background6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy4.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy5.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Enemy6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General1.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General2.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General3.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General4.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General5.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "General6.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "Music/Music2.mp3"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World_valea.png"))
            && File.Exists(Path.Combine(Application.persistentDataPath, "WorldShow/World_aperta.png"))
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
        if (anims.Count == minAnimationCount) {
            BaseAnim[] baseAnims = FindObjectsByType<BaseAnim>(FindObjectsSortMode.None);
            foreach (BaseAnim baseAnim in baseAnims) {
                foreach (FrameAnim anim in baseAnim.animators) {
                    anims[anim.name.name] = anim;
                }
            }
        }

        if (isPlayingAnimation) {
            if (anims[currentlyPlayingAnimation].currentFrame >= anims[currentlyPlayingAnimation].frames.Count) {
                isPlayingAnimation = false;
            }
            PlayAnimation(currentlyPlayingAnimation);
        }

        if (isPlayingAnimationGroup) {
            if (animGroups[currentlyPlayingAnimationGroup][0].currentFrame >= animGroups[currentlyPlayingAnimationGroup][0].frames.Count) {
                isPlayingAnimationGroup = false;
            }
            PlayAnimationGroup(currentlyPlayingAnimationGroup);
        }

        if (isPlayingObjectAnimation) {
            if (currentlyPlayingObjectAnimation.animators[0].currentFrame >= currentlyPlayingObjectAnimation.animators[0].frames.Count) {
                isPlayingObjectAnimation = false;
                currentlyPlayingObjectAnimation.ResetAnimation(currentlyPlayingObjectAnimation.transform.position);
            }
            PlayObjectAnimation(currentlyPlayingObjectAnimation.name);
        }
        
        if (isDataDownloaded && !isGameOver && !isGamePaused)
        {
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            if (currentScene.name == "DebugScene") {
                //sphm.enabled = false;
                sphd.enabled = false;
            }
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

        if (sphm != null && sphm.speed != levelConfig.levelSpeed) {
            sphm.speed = levelConfig.levelSpeed;
        }

        float balusPercent = (balus.transform.position.z / (float)gre.positionsCount) * 100f;
        balusPercent = Mathf.Clamp(balusPercent, 0f, 100f);
        realPercent = Math.Round(balusPercent).ToString() + "%";
        percentTextMesh.SetText(realPercent);

        // Check if Balus falls under Y position 0
        if (balus.transform.position.y < 0f && !isGameOver && !isDeathDisabled && !delayed)
        {
            hasFallen = true;
            //Debug.Log(transform.position.y);
            GameOver(realPercent, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && !isGameOver && !isDeathDisabled)
        {
            hasHitObstacle = true;
            GameOver(realPercent, true);
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
            List<GameObject> movers = GameObject.FindGameObjectsWithTag("MoverCollisionGroup1").ToList();
            movers.AddRange(GameObject.FindGameObjectsWithTag("MoverCollisionGroup2"));
            movers.AddRange(GameObject.FindGameObjectsWithTag("MoverCollisionGroup3"));
            foreach (GameObject mover in movers) {
                if (mover.transform.position.x == other.transform.position.x && mover.transform.position.z == other.transform.position.z) {
                    Domino domino = mover.GetComponent<Domino>();
                    if (domino != null) {
                        domino.TriggerManualDomino();
                    }
                }
            }
        } else if (other.gameObject.CompareTag("LevelEnd") && !isGameOver) {
            rb.position = new Vector3(rb.position.x, 0.5f, rb.position.z - 0.1f);
            sphm.isNotFalling = true;
            GameOver(realPercent, false);
        }
    }

    public void SetIsInMainMenu(bool isInMainMenu)
    {
        this.isInMainMenu = isInMainMenu;
    }

    public void LoadLevel(string level) {
        gre.jsonFilePath = "LevelData/Ground_" + level;
        ere.jsonFilePath = "LevelData/Enemies_" + level;
        themeChanger.jsonFilePath = "LevelData/Themes_" + level;
        levelConfig.jsonFilePath = "LevelData/Config_" + level;
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
        if (sphm != null) sphm.enabled = false;
        GFreeze.enabled = true;
        sphd.enabled = true;
        themeChanger2.enabled = true;
        themeChanger2.UpdateTheme(themeChanger.themeID);
        isGamePaused = GFreeze.gamePaused;
        gamePlayCanvas.SetActive(true);
        gameOverPanel.SetActive(false);
        Destroy(mainMenuNormalTile);
        geoBufferJsonFilePath = "LevelData/GeoBuffer_" + level + ".json";
        string geoBufferJsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(geoBufferJsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        if (levelConfig.startPortal) {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
            rb.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        } else {
            balus.transform.position = new Vector3(0f, 0.5f, levelConfig.startPos);
            rb.position = new Vector3(0f, 0.5f, levelConfig.startPos);
        }
        levelEditButton.GetComponent<Button>().onClick.AddListener(() => LoadLevelInEditor(level));
    }

    public void LoadLevelInEditor(string level) {
        gre.jsonFilePath = "LevelData/Ground_" + level;
        ere.jsonFilePath = "LevelData/Enemies_" + level;
        themeChanger.jsonFilePath = "LevelData/Themes_" + level;
        levelConfig.jsonFilePath = "LevelData/Config_" + level;
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
        geoBufferJsonFilePath = "LevelData/GeoBuffer_" + level + ".json";
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

    public void CreateNewLevel(string levelID) {
        StreamWriter configWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Config_{levelID}.json"));
        configWriter.Write(@"{""level_name"":""Unnamed Level"",""level_author"":""Anonymous"",""level_speed"":7.55,""start_pos"":0,""music_path"":""Music/Music1"",""worldshow_path"":null,""start_portal"":false}");
        configWriter.Close();
        StreamWriter groundWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Ground_{levelID}.json"));
        groundWriter.Write(@"{""positions"":[[0,0,0,0,0]]}");
        groundWriter.Close();
        StreamWriter enemiesWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Enemies_{levelID}.json"));
        enemiesWriter.Write(@"{""positions"":[[0,0,0,0,0]]}");
        enemiesWriter.Close();
        StreamWriter themeWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"Themes_{levelID}.json"));
        themeWriter.Write(@"{""themeZPositions"":[0],""themeIds"":[0]}");
        themeWriter.Close();
        StreamWriter geoBufferWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData", $"GeoBuffer_{levelID}.json"));
        geoBufferWriter.Write(@"{""ground"":[210,210,210,210,210,210,210,210,210,210,210,210,210,210,210],""enemies"":[200,210,210,210,210,210,195,195,195,195,210,210,195,210,210,210,210,210,20,125,125,125,125,125,125,125,125,170]}");
        geoBufferWriter.Close();
        LoadLevelInEditor(levelID);
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
        GameOver(percent, true);
        delayed = false;
    }

    public void DecaySpeed(float multiplier) {
        levelConfig.levelSpeed /= multiplier;
    }

    public void PlayAnimation(string animationName) {
        isPlayingAnimation = true;
        anims[animationName].Play();
        if (currentlyPlayingAnimation != animationName) {
            currentlyPlayingAnimation = animationName;
        }
    }

    public void PlayAnimationGroup(string groupName)  {
        isPlayingAnimationGroup = true;
        if (animGroups.ContainsKey(groupName) == false) {
            return;
        }
        foreach (FrameAnim anim in animGroups[groupName]) {
            anim.Play();
        }
        if (currentlyPlayingAnimationGroup != groupName) {
            currentlyPlayingAnimationGroup = groupName;
        }
    }

    public void PlayObjectAnimation(string objectName) {
        BaseObject[] baseObjects = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
        foreach (BaseObject baseObject in baseObjects) {
            if (baseObject.name == objectName) {
                isPlayingObjectAnimation = true;
                foreach (FrameAnim anim in baseObject.animators) {
                    anim.Play();
                }
                if (currentlyPlayingObjectAnimation != baseObject) {
                    currentlyPlayingObjectAnimation = baseObject;
                }
                break;
            }
        }
    }

    public void ResetBaseObjectAnimation(string objectName, Vector3 position) {
        BaseObject[] baseObjects = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
        foreach (BaseObject baseObject in baseObjects) {
            if (baseObject.name == objectName) {
                baseObject.ResetAnimation(position, false);
                break;
            }
        }
    }

    public string GetPercentage() {
        float balusPercent = (balus.transform.position.z / (float)gre.positionsCount) * 100f;
        balusPercent = Mathf.Clamp(balusPercent, 0f, 100f);
        realPercent = Math.Round(balusPercent).ToString() + "%";
        percentTextMesh.SetText(realPercent);
        return realPercent;
    }

    public void GameOver(string percent, bool stopMusic)
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
        GFreeze.PauseGame(stopMusic);
        isGamePaused = GFreeze.gamePaused;
        //GFreeze.enabled = false;
        if (stopMusic) {
            audioPlayer.PauseAudio();
        }
        //rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void RestartGame()
    {
        sphm.isJumping = false;
        hasFallen = false;
        hasHitObstacle = false;
        if (!balus.activeSelf) {
            balus.SetActive(true);
        }
        objectPool.ClearAllPools();
        num_collectedGems = 0;
        string jsonString = File.ReadAllText(Path.Combine(Application.persistentDataPath, geoBufferJsonFilePath));
        geoBufferJson = JsonConvert.DeserializeObject<GeoBufferJson>(jsonString);
        objectPool.InitializePools(gre.prefabs, ere.prefabs, geoBufferJson);
        BaseObject[] baseObjects = FindObjectsByType<BaseObject>(FindObjectsSortMode.None);
        foreach (BaseObject baseObject in baseObjects) {
            baseObject.ResetAnimation(baseObject.transform.position);
        }
        isPlayingAnimation = false;
        isPlayingAnimationGroup = false;
        isPlayingObjectAnimation = false;
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
        totalGemCount = ere.CountEnemies(18);
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
        levelConfig.LoadLevelConfig();
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
        TextAsset ground1 = Resources.Load<TextAsset>("LevelData/Ground_valea");
        TextAsset enemies1 = Resources.Load<TextAsset>("LevelData/Enemies_valea");
        TextAsset theme1 = Resources.Load<TextAsset>("LevelData/Themes_valea");
        TextAsset theme2 = Resources.Load<TextAsset>("ThemeData/ThemeData");
        TextAsset config1 = Resources.Load<TextAsset>("LevelData/Config_valea");
        TextAsset geoBuffer1 = Resources.Load<TextAsset>("LevelData/GeoBuffer_valea");
        TextAsset ground2 = Resources.Load<TextAsset>("LevelData/Ground_aperta");
        TextAsset enemies2 = Resources.Load<TextAsset>("LevelData/Enemies_aperta");
        TextAsset theme3 = Resources.Load<TextAsset>("LevelData/Themes_aperta");
        TextAsset config2 = Resources.Load<TextAsset>("LevelData/Config_aperta");
        TextAsset geoBuffer2 = Resources.Load<TextAsset>("LevelData/GeoBuffer_aperta");
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "LevelData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "ThemeData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Backgrounds"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Music"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "WorldShow"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "MenuData"));
        StreamWriter groundWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Ground_valea.json"));
        groundWriter.Write(ground1.text);
        groundWriter.Close();
        StreamWriter enemiesWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_valea.json"));
        enemiesWriter.Write(enemies1.text);
        enemiesWriter.Close();
        StreamWriter themeWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Themes_valea.json"));
        themeWriter.Write(theme1.text);
        themeWriter.Close();
        StreamWriter theme2Writer = new StreamWriter(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json"));
        theme2Writer.Write(theme2.text);
        theme2Writer.Close();
        StreamWriter configWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Config_valea.json"));
        configWriter.Write(config1.text);
        configWriter.Close();
        StreamWriter geoBufferWriter = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_valea.json"));
        geoBufferWriter.Write(geoBuffer1.text);
        geoBufferWriter.Close();
        StreamWriter groundWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Ground_aperta.json"));
        groundWriter2.Write(ground2.text);
        groundWriter2.Close();
        StreamWriter enemiesWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Enemies_aperta.json"));
        enemiesWriter2.Write(enemies2.text);
        enemiesWriter2.Close();
        StreamWriter themeWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Themes_aperta.json"));
        themeWriter2.Write(theme3.text);
        themeWriter2.Close();
        StreamWriter configWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/Config_aperta.json"));
        configWriter2.Write(config2.text);
        configWriter2.Close();
        StreamWriter geoBufferWriter2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "LevelData/GeoBuffer_aperta.json"));
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
        Texture2D background4 = Resources.Load<Texture2D>("Backgrounds/Background4");
        byte[] data4 = background4.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background4.png"), data4);
        Texture2D enemy4 = Resources.Load<Texture2D>("Enemy4");
        byte[] enemyData4 = enemy4.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy4.png"), enemyData4);
        Texture2D general4 = Resources.Load<Texture2D>("General4");
        byte[] generalData4 = general4.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General4.png"), generalData4);
        Texture2D background5 = Resources.Load<Texture2D>("Backgrounds/Background5");
        byte[] data5 = background5.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background5.png"), data5);
        Texture2D enemy5 = Resources.Load<Texture2D>("Enemy5");
        byte[] enemyData5 = enemy5.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Enemy5.png"), enemyData5);
        Texture2D general5 = Resources.Load<Texture2D>("General5");
        byte[] generalData5 = general5.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "General5.png"), generalData5);
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
        Texture2D worldShow1 = Resources.Load<Texture2D>("WorldShow/World_valea");
        byte[] worldShowData = worldShow1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "WorldShow/World_valea.png"), worldShowData);
        Texture2D worldShow2 = Resources.Load<Texture2D>("WorldShow/World_aperta");
        byte[] worldShowData2 = worldShow2.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "WorldShow/World_aperta.png"), worldShowData2);
        string menuDataJson = Resources.Load<TextAsset>("MenuData/MenuData").text;
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "MenuData/MenuData.json"), menuDataJson);
        isDataDownloaded = true;
    }
}
