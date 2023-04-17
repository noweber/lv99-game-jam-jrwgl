using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float speed = 5f;

    [Min(0)]
    [SerializeField] private float randomSpeedMagnitude = 2f;

    private GameObject target;

    private Rigidbody2D rb;

    private void Awake()
    {
        speed = Random.Range(speed - randomSpeedMagnitude, speed + randomSpeedMagnitude);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var player = FindObjectOfType<SideScrollerPlayerController>();
        if (player != null)
        {
            target = player.gameObject;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            rb.velocity = direction.normalized * Random.Range(0.8f * speed, speed);
        }
    }
}
