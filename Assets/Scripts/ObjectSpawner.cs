using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameManager gameManager;
    [SerializeField] public float spawnInterval = 5f;
    private float lastSpawnTime;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameActive)
        {
            if (Time.time >= lastSpawnTime + spawnInterval)
            {
                SpawnObject();
                lastSpawnTime = Time.time;
            }
        }

    }

    void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            gameManager.enemies.Add(Instantiate(objectToSpawn, transform.position, transform.rotation));
        }
        else
        {
            Debug.LogWarning("Object to spawn is not set.");
        }
    }
}
