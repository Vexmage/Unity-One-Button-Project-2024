using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPrefab; // Assign the Bug GameObject in the inspector
    public float spawnInterval = 2f; // Time interval between bug spawns
    public int maxBugs = 10; // Maximum number of bugs to spawn per wave

    private int currentBugCount = 0; // Track the number of spawned bugs

    void Start()
    {
        StartCoroutine(SpawnBugs());
    }

    System.Collections.IEnumerator SpawnBugs()
    {
        while (currentBugCount < maxBugs)
        {
            SpawnBug();
            currentBugCount++;

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBug()
    {
        // Access the xLimit from CrosshairController
        float xLimit = CrosshairController.xLimit;

        // Randomize bug spawn position within range
        float randomX = Random.Range(-xLimit, xLimit);

        // Spawn the bug at the randomized position
        Vector3 spawnPosition = new Vector3(randomX, 5f, 0f); // Start at the top of the screen
        Instantiate(bugPrefab, spawnPosition, Quaternion.identity);
    }
}
