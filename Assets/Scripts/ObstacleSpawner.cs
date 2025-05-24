using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public List<GameObject> obstacleSets;      // Drag your obstacle prefabs here
    public Transform player;                   // Drag your Player here
    public float spawnCheckDistance = 50f;     // Distance ahead of player to check for spawning
    public float obstacleSpacing = 20f;        // Minimum distance between obstacles
    public float destroyBehindDistance = 100f;
    public int maxActiveObstacles = 5;

    private List<GameObject> activeObstacles = new List<GameObject>();
    private float lastSpawnZ = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("SpawnObstacleSet", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z + spawnCheckDistance > lastSpawnZ)
        {
            SpawnObstacleSet();
        }
        CleanupPassedObstacles();
    }

    void SpawnObstacleSet()
    {
        int randomIndex = Random.Range(0, obstacleSets.Count);
        GameObject selectedPrefab = obstacleSets[randomIndex];

        float obstacleLength = selectedPrefab.GetComponent<ObstacleSet>().length;

        Vector3 spawnPos = new Vector3(0, 0, lastSpawnZ + obstacleSpacing);
        GameObject newObstacle = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        activeObstacles.Add(newObstacle);

        lastSpawnZ = spawnPos.z + obstacleLength;
    }

    void CleanupPassedObstacles()
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = activeObstacles[i];
            if (obstacle == null) continue;

            if (player.position.z - obstacle.transform.position.z > destroyBehindDistance)
            {
                Destroy(obstacle);
                activeObstacles.RemoveAt(i);
            }
        }
    }
}
