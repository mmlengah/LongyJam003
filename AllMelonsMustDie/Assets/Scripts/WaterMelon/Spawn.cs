using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Define prefabs of your GameObjects
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;
    public GameObject prefab5; // Fifth GameObject as the rare spawn

    // Maximum GameObjects on the screen
    private int maxObjects = 35;

    private static int currentObjects = 0;

    public static int CurrentObjects
    {
        get { return currentObjects; }
        set { currentObjects = value; }
    }

    // List to hold the current GameObjects
    private List<GameObject> gameObjects = new List<GameObject>();

    // Flag to control spawning of the rare object
    private bool rareObjectSpawned = false;

    private void Update()
    {
        if (currentObjects < maxObjects)
        {
            SpawnRandomObject();
            currentObjects++;
        }
    }

    private void SpawnRandomObject()
    {
        GameObject prefabToSpawn;

        // If the rare object hasn't been spawned, check if it should be spawned
        if (!rareObjectSpawned && Random.Range(0.0f, 1.0f) <= 0.05f) // 5% chance to spawn the rare object
        {
            prefabToSpawn = prefab5; // Rare GameObject
            rareObjectSpawned = true;
        }
        else
        {
            // If the rare object has already been spawned, randomly choose from the other 4 prefabs
            prefabToSpawn = ChooseRandomPrefab();
        }

        // Randomly choose a position to instantiate the GameObject
        Vector3 randomPosition = new Vector3(Random.Range(-812.4f, -864.0f), 3050.482f, Random.Range(1204.7f, 1342.1f));
        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        gameObjects.Add(spawnedObject);
        spawnedObject.transform.parent = transform;
    }

    private GameObject ChooseRandomPrefab()
    {
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                return prefab1;
            case 2:
                return prefab2;
            case 3:
                return prefab3;
            case 4:
                return prefab4;
            default:
                return null;
        }
    }
}
