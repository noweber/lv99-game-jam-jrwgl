using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private int enemeyNum;

    public bool isWin;

    public GameObject objectToSpawn; // The game object to spawn
    public float spawnInterval = 1f; // The interval between each spawn
    private float timeUntilNextSpawn;
    private GameObject player;

    private void Start()
    {
        isWin = false;
        var playerController = FindObjectOfType<TopDownPlayerController>();
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
            enemeyNum--;
        }

        if(enemeyNum < 0)
        {
            isWin = true;
        }
    }

    private float GetSpawnInterval()
    {
        return Random.Range(spawnInterval / 2f, 1.5f * spawnInterval);
    }
}
