using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject balus;
    public GameObject gameOverPanel;
    public GameObject gamePlayCanvas;
    public GroundRenderer gre;
    public GameFreeze GFreeze;
    public CameraFollow CFollow;
    private GameObject percentTextLabel;
    private TextMeshProUGUI percentTextMesh;
    private string realPercent;
    public EnemyRenderer ere;
    private LevelThemeChanger themeChanger;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private ThemeChanger themeChanger2;
    private GameObject levelRenderer;
    private LevelConfigurator levelConfig;
    private bool isGameOver = false;
    public bool isDataDownloaded = false;
    private Rigidbody rb;
    private AudioPlayer audioPlayer;
    void Start()
    {
        percentTextLabel = GameObject.Find("Percent");
        percentTextMesh = percentTextLabel.GetComponent<TextMeshProUGUI>();
        balus = GameObject.FindGameObjectWithTag("Balus");
        levelRenderer = GameObject.Find("LevelRenderer");
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

        Application.targetFrameRate = 60;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground1.json")) && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies1.json")) && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes1.json")) && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json")))
        {
            isDataDownloaded = true;
        }
        else
        {
            isDataDownloaded = false;
        }

        if (isDataDownloaded)
        {
            // rb.useGravity = true;
            GFreeze.enabled = true;
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            sphm.enabled = false;
            sphd.enabled = true;
            themeChanger2.enabled = true;
        }
        else 
        {
            rb.useGravity = false;
            gre.enabled = false;
            ere.enabled = false;
            themeChanger.enabled = false;
            sphm.enabled = false;
            sphd.enabled = false;
            themeChanger2.enabled = false;
            LoadData();
            isDataDownloaded = true;
        }
    }

    void Update()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Ground1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Enemies1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "LevelData/Themes1.json")) 
        && File.Exists(Path.Combine(Application.persistentDataPath, "ThemeData/ThemeData.json")))
        {
            isDataDownloaded = true;
        }
        else
        {
            isDataDownloaded = false;
        }
        
        if (isDataDownloaded && !isGameOver)
        {
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            sphm.enabled = false;
            GFreeze.enabled = true;
            sphd.enabled = true;
            themeChanger2.enabled = true;
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

        sphm.speed = levelConfig.levelSpeed;

        float balusPercent = (balus.transform.position.z / (float)gre.positionsCount) * 100f;
        balusPercent = Mathf.Clamp(balusPercent, 0f, 100f);
        realPercent = Math.Round(balusPercent).ToString() + "%";
        percentTextMesh.SetText(realPercent);

        // Check if Balus falls under Y position 0
        if (balus.transform.position.y < 0 && !isGameOver)
        {
            GameOver(realPercent);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isGameOver)
        {
            GameOver(realPercent);
        }
    }

    void GameOver(string percent)
    {
        isGameOver = true;
        //Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        GameObject percentTextLabel2 = GameObject.Find("Percent2");
        TextMeshProUGUI percentTextMesh2 = percentTextLabel2.GetComponent<TextMeshProUGUI>();
        percentTextMesh2.SetText(percent);
        GameObject levelNameTextLabel = GameObject.Find("LevelName");
        TextMeshProUGUI levelNameTextMesh = levelNameTextLabel.GetComponent<TextMeshProUGUI>();
        levelNameTextMesh.SetText(levelConfig.levelName);
        gamePlayCanvas.SetActive(false);
        sphd.enabled = false;
        sphm.enabled = false;
        CFollow.enabled = false;
        GFreeze.enabled = false;
        audioPlayer.PauseAudio();
    }

    public void RestartGame()
    {
        sphm.enabled = false;
        sphd.enabled = false;
        CFollow.enabled = false;
        gre.enabled = false;
        ere.enabled = false;
        gamePlayCanvas.SetActive(true);
        gameOverPanel.SetActive(false);
        Vector3 balusPos = new Vector3(0f, 0.5f, 0f);
        balus.transform.position = balusPos;
        gre.enabled = true;
        ere.enabled = true;
        GameObject[] normalTiles = GameObject.FindGameObjectsWithTag("NormalTile");
        GameObject[] jumpTiles = GameObject.FindGameObjectsWithTag("JumpTile");
        GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
        Vector3 jumpTilePos = new Vector3(-32f, 0.2f, -2f);
        Vector3 normalTilePos = new Vector3(-35f, 0.2f, 0.08f);
        Vector3 riserPos = new Vector3(-33f, 0f, 0f);
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
        gre.clearPrefabPositions();
        foreach (GameObject tile in risers) {
            if (tile.transform.position == riserPos) {
                continue;
            }
            Destroy(tile);
        }
        ere.clearPrefabPositions();
        GFreeze.enabled = true;
        Time.timeScale = 1f;
        GFreeze.PauseGame();
        sphm.enabled = true;
        sphd.enabled = true;
        CFollow.enabled = true;
        audioPlayer.SeekToZero();
        themeChanger.themeID = 0;
        
        isGameOver = false;
    }

    private void LoadData()
    {
        TextAsset ground1 = Resources.Load<TextAsset>("LevelData/Ground1");
        TextAsset enemies1 = Resources.Load<TextAsset>("LevelData/Enemies1");
        TextAsset theme1 = Resources.Load<TextAsset>("LevelData/Themes1");
        TextAsset theme2 = Resources.Load<TextAsset>("ThemeData/ThemeData");
        TextAsset config1 = Resources.Load<TextAsset>("LevelData/Config1");
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "LevelData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "ThemeData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Backgrounds"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Music"));
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
        AudioClip music1 = Resources.Load<AudioClip>("Music/Music1");
        EncodeMP3.convert(music1, Path.Combine(Application.persistentDataPath, "Music/Music1.mp3"), 32);
        isDataDownloaded = true;
    }
}
