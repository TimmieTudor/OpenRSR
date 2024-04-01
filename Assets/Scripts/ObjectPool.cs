using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<GameObject, Queue<GameObject>> pooledObjects = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, HashSet<GameObject>> activeObjects = new Dictionary<GameObject, HashSet<GameObject>>();
    private int maxPoolSize = 200; // Set a maximum pool size
    private List<Vector3> ObstaclePoses = new List<Vector3>();

    private void Awake()
    {
        if (Instance == null)
        {
            this.ObstaclePoses.Add(new Vector3(-5f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-46f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-49f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-50f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-51f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-35f, 0.2f, 0.08f));
            this.ObstaclePoses.Add(new Vector3(-33f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-40f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-42f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-51f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-54f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-56f, 0.2f, 0f));
            this.ObstaclePoses.Add(new Vector3(-60f, 0.2f, 0f));
            this.ObstaclePoses.Add(new Vector3(-64f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-66f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-68f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-70f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-72f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-74f, 0f, 0f));
            this.ObstaclePoses.Add(new Vector3(-76f, 0f, 0f));
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPrefab(GameObject prefab, Vector3 spawnPosition)
    {
        //Debug.Log(!pooledObjects.ContainsKey(prefab));
        if (!pooledObjects.ContainsKey(prefab))
        {
            pooledObjects.Add(prefab, new Queue<GameObject>());
            activeObjects.Add(prefab, new HashSet<GameObject>());
        }

        Queue<GameObject> pool = pooledObjects[prefab];
        if (pool.Count > 0) {
            GameObject obj = pool.Dequeue();
            if (!ObstaclePoses.Contains(obj.transform.position)) {
                obj.SetActive(true);
                activeObjects[prefab].Add(obj);
                return obj;
            } else {
                return GetPrefab(prefab, spawnPosition);
            }
        } else if (pooledObjects[prefab].Count + activeObjects[prefab].Count < maxPoolSize) {
            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
            if (newObject.name.Contains("(Clone)")) {
                newObject.name = prefab.name;
            }
            newObject.SetActive(true);
            activeObjects[prefab].Add(newObject);
            return newObject;
        }
        else
        {
            Debug.Log(pooledObjects[prefab].Count);
            // Attempt to find an inactive object to recycle
            foreach (GameObject obj in pooledObjects[prefab])
            {
                Debug.Log(!activeObjects[prefab].Contains(obj));
                if (!activeObjects[prefab].Contains(obj))
                {
                    obj.SetActive(true);
                    activeObjects[prefab].Add(obj);
                    return obj;
                }
            }

            // If no inactive objects are found, log a warning and return null
            Debug.LogWarning("Object pool limit reached for " + prefab.name + ", and no inactive objects to recycle.");
            return null;
        }
    }

    public void ReturnPrefab(GameObject prefab, GameObject objectToReturn)
    {
        if (!pooledObjects.ContainsKey(prefab))
        {
            pooledObjects.Add(prefab, new Queue<GameObject>());
            activeObjects.Add(prefab, new HashSet<GameObject>());
        }

        if (!ObstaclePoses.Contains(objectToReturn.transform.position)) {
            objectToReturn.SetActive(false);
            pooledObjects[prefab].Enqueue(objectToReturn);
            activeObjects[prefab].Remove(objectToReturn);

            //Debug.Log($"Returned {objectToReturn.name} to pool. Current pool count: {pooledObjects[prefab].Count}");
        }
    }

    public void ClearPool(GameObject prefab)
    {
        if (pooledObjects.ContainsKey(prefab))
        {
            foreach (GameObject obj in pooledObjects[prefab]) {
                Destroy(obj);
            }
            pooledObjects[prefab].Clear();
            activeObjects[prefab].Clear();
        }
    }
    public void ClearAllPools() {
        foreach (GameObject prefab in pooledObjects.Keys) {
            foreach (GameObject obj in pooledObjects[prefab]) {
                Destroy(obj);
            }
            pooledObjects[prefab].Clear();
            activeObjects[prefab].Clear();
        }
    }
}