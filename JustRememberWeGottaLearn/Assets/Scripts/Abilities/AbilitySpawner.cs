using UnityEngine;

public sealed class AbilitySpawner : MonoBehaviour
{

    public GameObject[] objectToSpawnList; // The game object to spawn
    public float spawnInterval = 1f; // The interval between each spawn
    private float timeUntilNextSpawn;

    public LayerMask enemeyDetectLayer;

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
            if(this.GetComponent<SideScrollerPlayerController>().getQi() >= 50)
            {
                Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 5.0f, enemeyDetectLayer);
                if(collider != null)
                {
                    Debug.Log("hello");
                    Vector3 offset = Vector3.zero;
                    if (collider.transform.position.x > this.transform.position.x)
                        offset = Vector3.right * 2.0f;
                    Instantiate(objectToSpawnList[1], transform.position + offset, transform.rotation);
                }

            }
            else
            {
                Collider2D collider = Physics2D.OverlapCircle(this.transform.position, 3.0f, enemeyDetectLayer);
                if (collider != null)
                {
                    Instantiate(objectToSpawnList[0], transform.position, transform.rotation);
                }
            }
        }
    }
}
