using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class EnemyPositionsData
{
    public List<List<int>> positions;

    public EnemyPositionsData(List<List<int>> positions) {
        this.positions = positions;
    }
}

public class EnemyRenderer : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public string jsonFilePath;
    public float prefabSpacing = 1f;
    public float destroyDistance = 12f;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();
    private List<int> spawnedPrefabIDs = new List<int>();
    private GameObject balus;
    private Dictionary<Vector3, GameObject> prefabPositions = new Dictionary<Vector3, GameObject>();
    private GameManager gameManager;
    private string jsonString;
    private EnemyPositionsData data;
    public GameObject groundRotor;
    public GameObject glassRotor;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    private bool shouldSpawnPrefabCache = true;
    private ObjectPool objectPool;
    private SphereMovement sphm;

    IEnumerator UpdateCache() {
        while (true)
        {
            // Update the cached value
            shouldSpawnPrefabCache = !shouldSpawnPrefabCache;

            // Wait for a few seconds before updating again
            yield return new WaitForSeconds(0.4f);
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
            UpdateEnemies();
        }
    }

    public void Initialize()
    {
        balus = GameObject.FindGameObjectWithTag("Balus");
        sphm = balus.GetComponent<SphereMovement>();
        GameObject objPoolObject = GameObject.Find("ObjectPool");
        objectPool = objPoolObject.GetComponent<ObjectPool>();
        gameManager = balus.GetComponent<GameManager>();
        jsonString = gameManager.isDataDownloaded ? File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json") : Resources.Load<TextAsset>(jsonFilePath).text;
        data = JsonConvert.DeserializeObject<EnemyPositionsData>(jsonString);
        ProcessEnemyPositions(data.positions);
    }

    private void UpdateEnemies()
    {
        ProcessEnemyPositions(data.positions);
        CleanupSpawnedPrefabs();
    }

    public int CountEnemies(int id) {
        int count = 0;
        if (data != null) {
            for (int i = 0; i < data.positions.Count; i++) {
                for (int j = 0; j < data.positions[i].Count; j++) {
                    if (data.positions[i][j] == id) {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    private void ProcessEnemyPositions(List<List<int>> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            List<int> row = positions[i];
            float z = i * prefabSpacing;
            for (int j = 0; j < row.Count; j++)
            {
                int hasPrefab = row[j];
                float x = j - 2;
                Vector3 spawnPosition = CalculateSpawnPosition(hasPrefab, x, z);
                if (ShouldSpawnPrefab(spawnPosition))
                {
                    SpawnPrefab(hasPrefab, spawnPosition, i, j, x, z);
                }
            }
        }
    }

    private Vector3 CalculateSpawnPosition(int hasPrefab, float x, float z)
    {
        Vector3 spawnPosition = new Vector3(x, 0.55f, z);
        if (hasPrefab == 4 || hasPrefab == 5 || (hasPrefab >= 10 && hasPrefab <= 15))
        {
            spawnPosition = new Vector3(x, 0f, z);
        }
        else if (hasPrefab == 6 || hasPrefab == 7)
        {
            spawnPosition = new Vector3(x, 0.2f, z);
        }
        else if (hasPrefab == 8)
        {
            spawnPosition = new Vector3(x + 1f, 0.2f, z);
        }
        else if (hasPrefab == 9)
        {
            spawnPosition = new Vector3(x - 1f, 0.2f, z);
        }
        return spawnPosition;
    }

    private bool ShouldSpawnPrefab(Vector3 spawnPosition)
    {
        return spawnPosition.z - balus.transform.position.z < 25 && !prefabPositions.ContainsKey(spawnPosition);
    }

    private void SpawnPrefab(int hasPrefab, Vector3 spawnPosition, int i, int j, float x, float z)
    {
        if (hasPrefab != 0) {
            GameObject spawnedPrefab = objectPool.GetPrefab(prefabs[hasPrefab], spawnPosition);
            if (sphm.fallingObstaclesGroup1.Contains(spawnedPrefab)) {
                sphm.fallingObstaclesGroup1.Remove(spawnedPrefab);
                sphm.fallingObstaclesGroup1.Clear();
            } else if (sphm.fallingObstaclesGroup2.Contains(spawnedPrefab)) {
                sphm.fallingObstaclesGroup2.Remove(spawnedPrefab);
                sphm.fallingObstaclesGroup2.Clear();
            } else if (sphm.fallingObstaclesGroup3.Contains(spawnedPrefab)) {
                sphm.fallingObstaclesGroup3.Remove(spawnedPrefab);
                sphm.fallingObstaclesGroup3.Clear();
            }
            if (sphm.fallingObstacles.Contains(spawnedPrefab)) {
                sphm.fallingObstacles.Remove(spawnedPrefab);
                while (sphm.fallingObstacles.Contains(spawnedPrefab)) {
                    sphm.fallingObstacles.Remove(spawnedPrefab);
                }
            }
            if (spawnedPrefab.TryGetComponent<RiserAnim>(out RiserAnim riserAnim)) {
                if (riserAnim.animator == null || riserAnim.animator2 == null) {
                    goto SkipRiserAnim;
                }
                riserAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<CrusherAnim>(out CrusherAnim crusherAnim)) {
                if (crusherAnim.animator == null || crusherAnim.animator2 == null) {
                    goto SkipRiserAnim;
                }
                crusherAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<LeftHammerAnim>(out LeftHammerAnim hammerAnim)) {
                if (hammerAnim.animator == null || hammerAnim.animator2 == null) {
                    goto SkipRiserAnim;
                }
                hammerAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<RightHammerAnim>(out RightHammerAnim hammerAnim2)) {
                if (hammerAnim2.animator == null || hammerAnim2.animator2 == null) {
                    goto SkipRiserAnim;
                }
                hammerAnim2.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<LeftHammerLargeAnim>(out LeftHammerLargeAnim hammerLargeAnim)) {
                if (hammerLargeAnim.animator == null) {
                    goto SkipRiserAnim;
                }
                hammerLargeAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<RightHammerLargeAnim>(out RightHammerLargeAnim hammerLargeAnim2)) {
                if (hammerLargeAnim2.animator == null) {
                    goto SkipRiserAnim;
                }
                hammerLargeAnim2.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<LargeTreeAnim>(out LargeTreeAnim largeTreeAnim)) {
                if (largeTreeAnim.animator == null || largeTreeAnim.animator2 == null || largeTreeAnim.animator3 == null || largeTreeAnim.animator4 == null || largeTreeAnim.animator5 == null || largeTreeAnim.animator6 == null || largeTreeAnim.animator7 == null || largeTreeAnim.animator8 == null) {
                    goto SkipRiserAnim;
                }
                largeTreeAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<MediumTreeAnim>(out MediumTreeAnim mediumTreeAnim)) {
                if (mediumTreeAnim.animator == null || mediumTreeAnim.animator2 == null || mediumTreeAnim.animator3 == null || mediumTreeAnim.animator4 == null || mediumTreeAnim.animator5 == null) {
                    goto SkipRiserAnim;
                }
                mediumTreeAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<SmallTreeAnim>(out SmallTreeAnim smallTreeAnim)) {
                if (smallTreeAnim.animator == null || smallTreeAnim.animator2 == null || smallTreeAnim.animator3 == null || smallTreeAnim.animator4 == null || smallTreeAnim.animator5 == null || smallTreeAnim.animator6 == null || smallTreeAnim.animator7 == null) {
                    goto SkipRiserAnim;
                }
                smallTreeAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<LaserAnim>(out LaserAnim laserAnim)) {
                if (laserAnim.animator == null || laserAnim.animator2 == null || laserAnim.animator3 == null) {
                    goto SkipRiserAnim;
                }
                laserAnim.ResetAnimation(spawnPosition);
                GameObject laserBaseObject = spawnedPrefab.transform.Find("DeceBalus_Laser_Base").gameObject;
                laserBaseObject.transform.position = new Vector3(spawnPosition.x, laserBaseObject.transform.position.y, spawnPosition.z);
            } else if (spawnedPrefab.TryGetComponent<FloaterAnim>(out FloaterAnim floaterAnim)) {
                if (floaterAnim.animator == null || floaterAnim.animator2 == null) {
                    goto SkipRiserAnim;
                }
                floaterAnim.ResetAnimation(spawnPosition);
                
            } else if (spawnedPrefab.TryGetComponent<SpotlightAnim>(out SpotlightAnim spotlightAnim)) {
                if (spotlightAnim.animator == null) {
                    goto SkipRiserAnim;
                }
                spotlightAnim.ResetAnimation(spawnPosition);
            } else if (spawnedPrefab.TryGetComponent<SoundPlayer>(out SoundPlayer soundPlayer)) {
                GameObject gemBaseObject = spawnedPrefab.transform.GetChild(0).gameObject;
                gemBaseObject.SetActive(true);
                if (gemBaseObject.transform.position.x - spawnedPrefab.transform.position.x > 0) {
                    gemBaseObject.transform.position = new Vector3(spawnedPrefab.transform.position.x, gemBaseObject.transform.position.y, gemBaseObject.transform.position.z);
                }
                soundPlayer.SeekToZero();
            }
            SkipRiserAnim:
            spawnedPrefab.transform.position = spawnPosition;
            spawnedPrefabs.Add(spawnedPrefab);
            spawnedPrefabIDs.Add(hasPrefab);
            prefabPositions.Add(spawnPosition, spawnedPrefab);
            if (hasPrefab == 6 || hasPrefab == 7 || hasPrefab == 15) {
                GroundRenderer gre = GetComponent<GroundRenderer>();
                PositionsData gdata = gre.GetData();
                if (gdata.positions[i][j] == 3 
                || gdata.positions[i][j] == 4 
                || gdata.positions[i][j] == 5
                || gdata.positions[i][j] == 6) {
                    GameObject rotor = Instantiate(glassRotor, new Vector3(x, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(groundRotor, new Vector3(x, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 8) {
                GroundRenderer gre = GetComponent<GroundRenderer>();
                PositionsData gdata = gre.GetData();
                if (gdata.positions[i][j] == 3 
                || gdata.positions[i][j] == 4 
                || gdata.positions[i][j] == 5
                || gdata.positions[i][j] == 6) {
                    GameObject rotor = Instantiate(glassRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(groundRotor, new Vector3(x + 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 9) {
                GroundRenderer gre = GetComponent<GroundRenderer>();
                PositionsData gdata = gre.GetData();
                if (gdata.positions[i][j] == 3 
                || gdata.positions[i][j] == 4 
                || gdata.positions[i][j] == 5
                || gdata.positions[i][j] == 6) {
                    GameObject rotor = Instantiate(glassRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                } else {
                    GameObject rotor = Instantiate(groundRotor, new Vector3(x - 1f, 0f, z), Quaternion.identity);
                    rotor.transform.SetParent(spawnedPrefab.transform);
                }
            } else if (hasPrefab == 16) {
                if (spawnedPrefab.TryGetComponent<LeftRollerAnim>(out LeftRollerAnim lr)) {
                    lr.xOffset = x;
                }
            } else if (hasPrefab == 17) {
                if (spawnedPrefab.TryGetComponent<RightRollerAnim>(out RightRollerAnim rr)) {
                    rr.xOffset = x;
                }
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

    public void UpdateData()
    {
        jsonString = File.ReadAllText(Application.persistentDataPath + "/" + jsonFilePath + ".json");
        data = JsonConvert.DeserializeObject<EnemyPositionsData>(jsonString);
    }

    public EnemyPositionsData GetData()
    {
        return JsonConvert.DeserializeObject<EnemyPositionsData>(jsonString);
    }

    public void ClearPrefabPositions()
    {
        prefabPositions.Clear();
        spawnedPrefabs.Clear();
        spawnedPrefabIDs.Clear();
        lastPosition = new Vector3(0f, 0f, 0f);
    }
}