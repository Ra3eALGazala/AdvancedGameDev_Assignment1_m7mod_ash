using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("إعدادات التوليد")]
    public GameObject prefabToSpawn; 
    public float spawnRate = 2f;    
    private float nextSpawnTime = 0f;

    void Update()
    {
      
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab();
           
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnPrefab()
    {
       
        Instantiate(prefabToSpawn, transform.position, transform.rotation);
    }
}