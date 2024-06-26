using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class PositionsData
{
    public List<List<int>> positions;

    public PositionsData(List<List<int>> positions) {
        this.positions = positions;
    }
}

public class GroundRenderer : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public string jsonFilePath;
    public float prefabSpacing = 1f;
    public float destroyDistance = 12f; // maximum distance between Balus and prefab to destroy it
    public int positionsCount;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private List<int> spawnedPrefabIDs = new List<int>();
    private GameObject balus;
    private Dictionary<Vector3, GameObject> prefabPositions = new Dictionary<Vector3, GameObject>();
    private GameManager gameManager;
    private string jsonString;
    private PositionsData data;
    private GameObject glassEdgePrefab;
    private GameObject moverEdgePrefab;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    private bool shouldSpawnPrefabCache = true;
    private ObjectPool objectPool;
    private SphereMovement sphm;
    private GameObject endObject;

    IEnumerator UpdateCache() {
        while (true)
        {
            // Update the cached value
            shouldSpawnPrefabCache = !shouldSpawnPrefabCache;

            // Wait for a few seconds before updating again
            yield return new WaitForSeconds(0.4f);
        }
    }
    private void HandleAllCases(GameObject spawnedPrefab, GameObject edgePrefab, Vector3 spawnPosition, List<List<int>> positions, int checknum, int i, int j, float x, float z) {
        if (i > 0 && i < positions.Count - 1 && j > 0 && j < positions[i].Count - 1) {
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge1.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge2 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge2.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == 0 && j > 0 && j < positions[i].Count - 1) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge2 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge2.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == positions.Count - 1 && j > 0 && j < positions[i].Count - 1) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge2 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge2.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i > 0 && i < positions.Count - 1 && j == 0) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge2 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge2.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i > 0 && i < positions.Count - 1 && j == positions[i].Count - 1) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge2 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge2.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == 0 && j == 0) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            GameObject edge2 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge2.transform.parent = spawnedPrefab.transform;
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == positions.Count - 1 && j == positions[i].Count - 1) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            GameObject edge2 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge2.transform.parent = spawnedPrefab.transform;
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == 0 && j == positions[i].Count - 1) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            GameObject edge2 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge2.transform.parent = spawnedPrefab.transform;
            if (!(positions[i+1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j-1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        } else if (i == positions.Count - 1 && j == 0) {
            GameObject edge1 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z + 0.45f), Quaternion.Euler(0f, 0f, 0f));
            edge1.transform.parent = spawnedPrefab.transform;
            GameObject edge2 = Instantiate(edgePrefab, new Vector3(x - 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
            edge2.transform.parent = spawnedPrefab.transform;
            if (!(positions[i-1][j] == checknum)) {
                GameObject edge3 = Instantiate(edgePrefab, new Vector3(x, 0.1f, z - 0.45f), Quaternion.Euler(0f, 0f, 0f));
                edge3.transform.parent = spawnedPrefab.transform;
            }
            if (!(positions[i][j+1] == checknum)) {
                GameObject edge4 = Instantiate(edgePrefab, new Vector3(x + 0.45f, 0.1f, z), Quaternion.Euler(0f, 90f, 0f));
                edge4.transform.parent = spawnedPrefab.transform;
            }
        }
    }

    private void Start()
    {
        Initialize();
        //StartCoroutine(UpdateCache());
    }

    private void Update()
    {
        if (lastPosition.z < balus.transform.position.z) {
            lastPosition = balus.transform.position;
        }
        if (ShouldSpawnPrefab(lastPosition)) {
            UpdateGround();
        } 
        if (endObject != null && endObject.transform.position != new Vector3(0f, 0f, positionsCount)) {
            endObject.transform.position = new Vector3(0f, 0f, positionsCount);
        }
    }

    public void Initialize()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        sphm = balus.GetComponent<SphereMovement>();
        glassEdgePrefab = GameObject.Find("DeceBalus_Grouped_Glass_Edge");
        moverEdgePrefab = GameObject.Find("DeceBalus_Mover_Edge");
        GameObject objPoolObject = GameObject.Find("ObjectPool");
        objectPool = objPoolObject.GetComponent<ObjectPool>();
        gameManager = balus.GetComponent<GameManager>();
        jsonString = gameManager.isDataDownloaded ? File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json") : Resources.Load<TextAsset>(jsonFilePath).text;
        data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        positionsCount = data.positions.Count;
        ProcessGroundPositions(data.positions);
        GameObject end = GameObject.Find("End");
        endObject = end;
    }

    private void UpdateGround()
    {
        ProcessGroundPositions(data.positions);
        CleanupSpawnedPrefabs();
    }

    public int CountEnemies(int id) {
        int count = 0;
        for (int i = 0; i < data.positions.Count; i++) {
            for (int j = 0; j < data.positions[i].Count; j++) {
                if (data.positions[i][j] == id) {
                    count++;
                }
            }
        }
        return count;
    }

    private void ProcessGroundPositions(List<List<int>> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            float z = i * prefabSpacing;
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                float x = j - 2;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                if (ShouldSpawnPrefab(spawnPosition))
                {
                    SpawnPrefab(hasPrefab, spawnPosition, i, j, x, z);
                    if (i + 1 >= positions.Count && endObject != null && endObject.transform.position != new Vector3(0f, 0f, positions.Count)) {
                        endObject.transform.position = new Vector3(0f, 0f, positions.Count);
                    }
                }
            }
        }
    }

    private bool ShouldSpawnPrefab(Vector3 spawnPosition)
    {
        return spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition);
    }

    private void SpawnPrefab(int hasPrefab, Vector3 spawnPosition, int i, int j, float x, float z)
    {
        if (hasPrefab != 0) {
            GameObject spawnedPrefab = objectPool.GetPrefab(prefabs[hasPrefab], spawnPosition);
            if (hasPrefab == 2) {
                GameObject jumpTileNormal = spawnedPrefab.transform.GetChild(0).gameObject;
                GameObject jumpTileActive = spawnedPrefab.transform.GetChild(1).gameObject;
                jumpTileActive.SetActive(false);
                jumpTileNormal.SetActive(true);
            } else if (hasPrefab == 3 || hasPrefab == 4 || hasPrefab == 5 || hasPrefab == 6) {
                GameObject glassTileCollision = spawnedPrefab.transform.GetChild(0).gameObject;
                GameObject glassTileActive = spawnedPrefab.transform.GetChild(2).gameObject;
                GameObject glassTileNormal = spawnedPrefab.transform.GetChild(1).gameObject;
                glassTileActive.SetActive(false);
                glassTileNormal.SetActive(true);
                if (sphm.glassGroup1.Contains(glassTileCollision)) {
                    sphm.glassGroup1.Remove(glassTileCollision);
                    while (sphm.glassGroup1.Contains(glassTileCollision)) {
                        sphm.glassGroup1.Remove(glassTileCollision);
                    }
                } else if (sphm.glassGroup2.Contains(glassTileCollision)) {
                    sphm.glassGroup2.Remove(glassTileCollision);
                    while (sphm.glassGroup2.Contains(glassTileCollision)) {
                        sphm.glassGroup2.Remove(glassTileCollision);
                    }
                } else if (sphm.glassGroup3.Contains(glassTileCollision)) {
                    sphm.glassGroup3.Remove(glassTileCollision);
                    while (sphm.glassGroup3.Contains(glassTileCollision)) {
                        sphm.glassGroup3.Remove(glassTileCollision);
                    }
                }
                if (sphm.glassTiles.Contains(glassTileCollision)) {
                    sphm.glassTiles.Remove(glassTileCollision);
                    while (sphm.glassTiles.Contains(glassTileCollision)) {
                        sphm.glassTiles.Remove(glassTileCollision);
                    }
                }
                foreach (Transform transform in spawnedPrefab.transform) {
                    if (transform.tag == "Edge") {
                        Destroy(transform.gameObject);
                    }
                }
                GlassObject glassObject = spawnedPrefab.GetComponent<GlassObject>();
                glassObject.fallCoefficient = 0.125f;
                //sphm.glassTiles.Clear();
            }
            spawnedPrefab.transform.position = spawnPosition;
            spawnedPrefabs.Add(spawnedPrefab);
            spawnedPrefabIDs.Add(hasPrefab);
            prefabPositions.Add(spawnPosition, spawnedPrefab);
            if (hasPrefab == 4) {
                HandleAllCases(spawnedPrefab, glassEdgePrefab, spawnPosition, data.positions, 4, i, j, x, z);
            } else if (hasPrefab == 5) {
                HandleAllCases(spawnedPrefab, glassEdgePrefab, spawnPosition, data.positions, 5, i, j, x, z);
            } else if (hasPrefab == 6) {
                HandleAllCases(spawnedPrefab, glassEdgePrefab, spawnPosition, data.positions, 6, i, j, x, z);
            } else if (hasPrefab == 7) {
                if (spawnedPrefab.TryGetComponent<LeftMovingTileAnim>(out LeftMovingTileAnim leftMovingTileAnim)) {
                    leftMovingTileAnim.xOffset = x;
                }
            } else if (hasPrefab == 8) {
                if (spawnedPrefab.TryGetComponent<RightMovingTileAnim>(out RightMovingTileAnim rightMovingTileAnim)) {
                    rightMovingTileAnim.xOffset = x;
                }
            } else if (hasPrefab == 9) {
                HandleAllCases(spawnedPrefab, moverEdgePrefab, spawnPosition, data.positions, 9, i, j, x, z);
            } else if (hasPrefab == 10) {
                HandleAllCases(spawnedPrefab, moverEdgePrefab, spawnPosition, data.positions, 10, i, j, x, z);
            } else if (hasPrefab == 11) {
                HandleAllCases(spawnedPrefab, moverEdgePrefab, spawnPosition, data.positions, 11, i, j, x, z);
            }
            lastPosition = balus.transform.position;
        }
    }

    private void CleanupSpawnedPrefabs()
    {
        for (int i = 0; i < spawnedPrefabs.Count; i++)
        {
            GameObject spawnedPrefab = spawnedPrefabs[i];
            if (spawnedPrefab == null || balus.transform.position.z - spawnedPrefab.transform.position.z >= destroyDistance)
            {
                if (spawnedPrefab != null)
                {
                    objectPool.ReturnPrefab(prefabs[spawnedPrefabIDs[i]], spawnedPrefab);
                }
                spawnedPrefabs.RemoveAt(i);
                spawnedPrefabIDs.RemoveAt(i);
            }
        }
    }

    public void UpdateData() {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        positionsCount = data.positions.Count;
    }

    public PositionsData GetData() {
        return JsonConvert.DeserializeObject<PositionsData>(jsonString);
    }

    public void ClearPrefabPositions() {
        prefabPositions.Clear();
        spawnedPrefabs.Clear();
        spawnedPrefabIDs.Clear();
        lastPosition = new Vector3(0f, 0f, 0f);
        endObject.transform.position = new Vector3(-90f, 0f, 0f);
    }
}
