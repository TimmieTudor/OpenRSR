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
    public GameObject prefab;
    public GameObject prefab2;
    public string jsonFilePath;
    public float prefabSpacing = 1f;
    public float destroyDistance = 10f; // maximum distance between Balus and prefab to destroy it
    public int positionsCount;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private GameObject balus;
    private Dictionary<Vector3, GameObject> prefabPositions = new Dictionary<Vector3, GameObject>();

    private void Start()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        string jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
        PositionsData data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        List<List<int>> positions = data.positions;
        positionsCount = positions.Count;
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                if (hasPrefab == 1)
                {
                    float x = j - 2;
                    float z = i * prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 20 && !prefabPositions.ContainsKey(spawnPosition))
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
                    if (spawnPosition.z - balus.transform.position.z < 20 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab2, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                }
            }
        }
    }

    private void Update()
    {
        string jsonString = Resources.Load<TextAsset>(jsonFilePath).text;
        PositionsData data = JsonConvert.DeserializeObject<PositionsData>(jsonString);
        List<List<int>> positions = data.positions;
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                if (hasPrefab == 1)
                {
                    float x = j - 2;
                    float z = i * prefabSpacing;
                    Vector3 spawnPosition = new Vector3(x, 0f, z);
                    if (spawnPosition.z - balus.transform.position.z < 20 && !prefabPositions.ContainsKey(spawnPosition))
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
                    if (spawnPosition.z - balus.transform.position.z < 20 && !prefabPositions.ContainsKey(spawnPosition))
                    {
                        GameObject spawnedPrefab = Instantiate(prefab2, spawnPosition, Quaternion.identity);
                        spawnedPrefabs.Add(spawnedPrefab);
                        prefabPositions.Add(spawnPosition, spawnedPrefab);
                    }
                }
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
}
