using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The game object to spawn
    public float spawnInterval = 1f; // The interval between each spawn
    private float timeUntilNextSpawn;
    private GameObject player;

    private void Start()
    {
        var playerController = FindObjectOfType<SideScrollerPlayerController>();
        player = playerController.gameObject;
        timeUntilNextSpawn = GetSpawnInterval();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn <= 0)
        {
            // Reset the timer
            timeUntilNextSpawn = GetSpawnInterval();

            // Spawn the game object
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    private float GetSpawnInterval()
    {
        return Random.Range(spawnInterval / 2f, 1.5f * spawnInterval);
    }
}
