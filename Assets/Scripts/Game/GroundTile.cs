using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    GroundSpawner GroundSpawner;

    public GameObject ObstaclePrefab;
    public GameObject CoinPrefab;

    void Start()
    {
        GroundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        GroundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle()
    {
        List<int> remainingSpawnersIndex = new List<int>()
        {
            2, 3, 4
        };

        int obstacleSpawnIndex = Random.Range(2, 5);

        remainingSpawnersIndex.Remove(obstacleSpawnIndex);

        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(ObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);

        remainingSpawnersIndex.ForEach(index => SpawnCoin(index));
    }

    void SpawnCoin(int index)
    {
        int obstacleSpawnIndex = Random.Range(2, 5);

        Transform spawnPoint = transform.GetChild(index).transform;

        GameObject coin = Instantiate(CoinPrefab, transform);

        coin.transform.position = spawnPoint.transform.position;
    }
}
