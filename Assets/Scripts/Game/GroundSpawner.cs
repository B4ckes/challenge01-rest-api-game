using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    const int INITIAL_TILES_AMOUNT = 15;
    const int NO_OBSTACLES_TILES_AMOUNT = 3;
    
    public GameObject GroundTile;
    Vector3 NextSpawnPoint;

    public void SpawnTile(bool shouldSpawnItems)
    {
        GameObject temp = Instantiate(GroundTile, NextSpawnPoint, Quaternion.identity);

        NextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (shouldSpawnItems) {
            temp.GetComponent<GroundTile>().SpawnObstacle();
        }
    }

    void Start()
    {
        for (int i = 0; i < INITIAL_TILES_AMOUNT; i++)
        {
            bool shouldSpawnItems = i >= NO_OBSTACLES_TILES_AMOUNT;

            SpawnTile(shouldSpawnItems);
        }
    }
}
