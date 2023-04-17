using UnityEngine;

public sealed class AbilitySpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The game object to spawn
    public float spawnInterval = 2f; // The interval between each spawn
    private float timeUntilNextSpawn;

    private void Start()
    {
        timeUntilNextSpawn = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilNextSpawn > 0)
        {
            timeUntilNextSpawn -= Time.deltaTime;
        }
        if (timeUntilNextSpawn <= 0)
        {
            // Reset the timer
            timeUntilNextSpawn = spawnInterval;

            // Spawn the game object
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}
