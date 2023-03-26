using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject balus;
    public GameObject gameOverPanel;
    public GameObject gamePlayCanvas;
    public GroundRenderer gre;
    public GameFreeze GFreeze;
    private GameObject percentTextLabel;
    private TextMeshProUGUI percentTextMesh;
    private string realPercent;
    public EnemyRenderer ere;
    private LevelThemeChanger themeChanger;
    private SphereMovement sphm;
    private SphereDragger sphd;
    private ThemeChanger themeChanger2;
    private GameObject levelRenderer;

    private bool isGameOver = false;
    public bool isDataDownloaded = false;
    private Rigidbody rb;

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
            rb.useGravity = true;
            GFreeze.enabled = true;
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            sphm.enabled = true;
            sphd.enabled = true;
            themeChanger2.enabled = true;
        }
        else 
        {
            rb.useGravity = false;
            GFreeze.enabled = false;
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
            gre.enabled = true;
            ere.enabled = true;
            themeChanger.enabled = true;
            sphm.enabled = true;
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver(realPercent);
        }
    }

    void GameOver(string percent)
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        GameObject percentTextLabel2 = GameObject.Find("Percent2");
        TextMeshProUGUI percentTextMesh2 = percentTextLabel2.GetComponent<TextMeshProUGUI>();
        percentTextMesh2.SetText(percent);
        gamePlayCanvas.SetActive(false);
        GFreeze.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void LoadData()
    {
        TextAsset ground1 = Resources.Load<TextAsset>("LevelData/Ground1");
        TextAsset enemies1 = Resources.Load<TextAsset>("LevelData/Enemies1");
        TextAsset theme1 = Resources.Load<TextAsset>("LevelData/Themes1");
        TextAsset theme2 = Resources.Load<TextAsset>("ThemeData/ThemeData");
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "LevelData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "ThemeData"));
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Backgrounds"));
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
        Texture2D background1 = Resources.Load<Texture2D>("Backgrounds/Background1");
        byte[] data = background1.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background1.png"), data);
        Texture2D background2 = Resources.Load<Texture2D>("Backgrounds/Background2");
        byte[] data2 = background2.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Backgrounds/Background2.png"), data2);
        isDataDownloaded = true;
    }
}
