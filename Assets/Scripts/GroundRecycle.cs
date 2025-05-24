using System.Collections.Generic;
using UnityEngine;

public class GroundRecycle : MonoBehaviour
{
    public Transform player;
    public GameObject groundPrefab;
    public List<GameObject> groundTiles;
    public float tileLength = 74.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, i * tileLength);
            GameObject tile = Instantiate(groundPrefab, spawnPos, Quaternion.identity);
            groundTiles.Add(tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject firstTile = groundTiles[0];

        // Check if the player has passed the first tile enough to recycle it
        if (player.position.z - firstTile.transform.position.z > tileLength)
        {
            // Move the first tile to the end
            GameObject lastTile = groundTiles[groundTiles.Count - 1];
            firstTile.transform.position = lastTile.transform.position + Vector3.forward * tileLength;

            // Update the list order
            groundTiles.RemoveAt(0);
            groundTiles.Add(firstTile);
        }
    }

}
