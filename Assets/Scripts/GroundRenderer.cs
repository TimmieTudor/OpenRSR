using System.Collections.Generic;
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
    public float destroyDistance = 10f; // maximum distance between Balus and prefab to destroy it
    public int positionsCount;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private GameObject balus;
    private Dictionary<Vector3, GameObject> prefabPositions = new Dictionary<Vector3, GameObject>();
    private GameManager gameManager;
    private string jsonString;
    private GameObject edgePrefab;

    private void HandleAllCases(GameObject spawnedPrefab, Vector3 spawnPosition, List<List<int>> positions, int checknum, int i, int j, float x, float z) {
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
        balus = GameObject.FindGameObjectWithTag("Balus");
        edgePrefab = GameObject.Find("DeceBalus_Grouped_Glass_Edge");
        gameManager = balus.GetComponent<GameManager>();
        if (gameManager.isDataDownloaded) {
            jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        } else {
            jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
        }
        PositionsData data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        List<List<int>> positions = data.positions;
        positionsCount = positions.Count;
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                float x = j - 2;
                float z = i * prefabSpacing;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition)) {
                    GameObject spawnedPrefab = Instantiate(prefabs[hasPrefab], spawnPosition, Quaternion.identity);
                    spawnedPrefabs.Add(spawnedPrefab);
                    prefabPositions.Add(spawnPosition, spawnedPrefab);
                    if (hasPrefab == 4) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 4, i, j, x, z);
                    } else if (hasPrefab == 5) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 5, i, j, x, z);
                    } else if (hasPrefab == 6) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 6, i, j, x, z);
                    }
                }

                /* if (hasPrefab == 1)
                {
                    float x = j - 2;
                    float z = i * prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                }
                else if (hasPrefab == 2)
                {
                    float x = j - 2;
                    float z = i * prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab2, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                } else if (hasPrefab == 3) {
                    float x = j - 2;
                    float z = i * prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab3, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                } */
            }
        }
    }

    private void Update()
    {
        PositionsData data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        List<List<int>> positions = data.positions;
        positionsCount = positions.Count;
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            float z = i * prefabSpacing;
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                float x = j - 2;
                Vector3 spawnPosition = new Vector3(x, 0f, z);
                if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition)) {
                    GameObject spawnedPrefab = Instantiate(prefabs[hasPrefab], spawnPosition, Quaternion.identity);
                    spawnedPrefabs.Add(spawnedPrefab);
                    prefabPositions.Add(spawnPosition, spawnedPrefab);
                    if (hasPrefab == 4) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 4, i, j, x, z);
                    } else if (hasPrefab == 5) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 5, i, j, x, z);
                    } else if (hasPrefab == 6) {
                        HandleAllCases(spawnedPrefab, spawnPosition, positions, 6, i, j, x, z);
                    }
                }
                /* if (hasPrefab == 1)
                {
                    float x = j - 2;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                }
                else if (hasPrefab == 2)
                {
                    float x = j - 2;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab2, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                } else if (hasPrefab == 3) {
                    float x = j - 2;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab3, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                } */
            }
        }

        for (int i = spawnedPrefabs.Count - 1; i >= 0; i--)
        {
            GameObject spawnedPrefab = spawnedPrefabs[i];
            if (spawnedPrefab == null)
            {
                spawnedPrefabs.RemoveAt(i);
                continue;
            }

            if (balus.transform.position.z - spawnedPrefab.transform.position.z >= destroyDistance)
            {
                Destroy(spawnedPrefab);
                spawnedPrefabs.RemoveAt(i);
            }
        }
    }

    public void UpdateData() {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
    }

    public void clearPrefabPositions() {
        prefabPositions.Clear();
        spawnedPrefabs.Clear();
    }
}
