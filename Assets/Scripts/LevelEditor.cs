using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class GroundPositionsData
{
    public List<List<int>> positions;

    public GroundPositionsData(List<List<int>> positions) {
        this.positions = positions;
    }
}

[System.Serializable]
public class EnemyPositionsData2
{
    public List<List<int>> positions;

    public EnemyPositionsData2(List<List<int>> positions) {
        this.positions = positions;
    }
}

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
    private GameObject percentTextLabel;
    private TextMeshProUGUI percentTextMesh;
    private string realPercent;
    public EnemyRenderer ere;
    public int gridSize = 10;
    private bool isInEditor = false;
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
    private GroundPositionsData gdata;
    private EnemyPositionsData2 edata;
    private LevelThemeData ltdata;
    public int objectLayer = 0;
    public int objectID = 0;
    public GameObject tilesPanel;
    public GameObject obstaclesPanel;
    public GameObject themesPanel;
    // Start is called before the first frame update
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
        manager = balus.GetComponent<GameManager>();
        groundJsonString = Resources.Load<TextAsset>("LevelData/Ground1").text;
        enemyJsonString = Resources.Load<TextAsset>("LevelData/Enemies1").text;
        themeJsonString = Resources.Load<TextAsset>("LevelData/Themes1").text;
        gdata = JsonConvert.DeserializeObject<GroundPositionsData>(groundJsonString);
        edata = JsonConvert.DeserializeObject<EnemyPositionsData2>(enemyJsonString);
        ltdata = JsonConvert.DeserializeObject<LevelThemeData>(themeJsonString);
        themeText = GameObject.Find("DeceBalus_Theme_Text");
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
        balus.transform.position = new Vector3(0f, 0.5f, 0f);
        Vector3 camPos = new Vector3(0f, 10f, 0f);
        Quaternion q = Quaternion.Euler(90f, 0f, 0f);
        m_camera.transform.position = camPos;
        m_camera.transform.rotation = q;
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
        foreach (GameObject tile in risers) {
            if (tile.transform.position == riserPos) {
                continue;
            }
            Destroy(tile);
        }
        List<List<int>> gpositions = gdata.positions;
        List<List<int>> epositions = edata.positions;
        for (int i = 0; i < gpositions.Count; i++) {
            for (int j = 0; j < gpositions[i].Count; j++) {
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
                }
            }
        }
        for (int i = 0; i < epositions.Count; i++) {
            for (int j = 0; j < epositions[i].Count; j++) {
                if (epositions[i][j] == 1) {
                    float x = j - 2;
                    float z = i * ere.prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0.55f, z);
                    GameObject spawnedPrefab = Instantiate(ere.prefab, spawnPosition, Quaternion.identity);
                }
            }
        }
        List<float> themeZPositions = ltdata.themeZPositions;
        Quaternion themetextRotation = Quaternion.Euler(90f, 0f, 0f);
        for (int i = 0; i < themeZPositions.Count; i++) {
            float z = themeZPositions[i];
            Vector3 spawnPosition = new Vector3(-1.4f, 0.1f, z - 0.4f);
            GameObject spawnedPrefab = Instantiate(themeText, spawnPosition, themetextRotation);
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

    private void saveLevelData() {
        string groundJsonString = JsonConvert.SerializeObject(gdata);
        string enemyJsonString = JsonConvert.SerializeObject(edata);
        // TODO: where the heck is Mihnea???
    }

    // scrolls sus
    public void ScrollUp() {
        float camPosZ = m_camera.transform.position.z;
        camPosZ += 0.15f;
        Vector3 camPos = new Vector3(m_camera.transform.position.x, m_camera.transform.position.y, camPosZ);
        m_camera.transform.position = camPos;
    }

    // scrolls jos
    public void ScrollDown() {
        float camPosZ = m_camera.transform.position.z;
        camPosZ -= 0.15f;
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
        GameObject[] risers = GameObject.FindGameObjectsWithTag("Riser");
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
            if (gdata.positions[i][j] == 1) {
                Instantiate(gre.prefab, new Vector3(x, 0f, z), Quaternion.identity);
            } else if (gdata.positions[i][j] == 2) {
                Instantiate(gre.prefab2, new Vector3(x, 0f, z), Quaternion.identity);
            }
        } else if (objectLayer == 1) {
            foreach (GameObject tile in risers) {
                if (tile.transform.position == new Vector3(x, 0.55f, z)) {
                    Destroy(tile);
                    break;
                }
            }
            if (edata.positions[i][j] == 1) {
                Instantiate(ere.prefab, new Vector3(x, 0.55f, z), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_camera.transform.position.z + 6f >= gridSize && isInEditor) {
            for (int i = 0; i < 5; i++) {
                float x = i - 2;
                float z = gridSize * gre.prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                GameObject spawnedPrefab = Instantiate(grid, spawnPosition, Quaternion.identity);
            }
            gridSize++;
        } else if (Input.GetMouseButtonDown(0) && isInEditor) {
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
                    gdata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                    edata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                }
                if (i >= 0 && i < gdata.positions.Count && j >= 0 && j < 5) {
                    gdata.positions[i][j] = objectID;
                    UpdateLevelData(i, j);
                }
            } else if (objectLayer == 1) {
                if (i >= edata.positions.Count) {
                    edata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
                    gdata.positions.Add(new List<int>(){ 0, 0, 0, 0, 0 });
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
    }
}
