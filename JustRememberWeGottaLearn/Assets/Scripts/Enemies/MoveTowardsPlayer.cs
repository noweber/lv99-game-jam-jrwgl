using Assets.Scripts.HitHurt;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float speed = 5f;

    [Min(0)]
    [SerializeField] private float randomSpeedMagnitude = 2f;

    private float collisionDamage;
    private float remainOnAirTime;

    private GameObject target;

    private Rigidbody2D rb;

    private void Awake()
    {
        speed = Random.Range(speed - randomSpeedMagnitude, speed + randomSpeedMagnitude);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var player = FindObjectOfType<TopDownPlayerController>();
        if (player != null)
        {
            target = player.gameObject;
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(remainOnAirTime);
        if(remainOnAirTime > 0)
        {
            remainOnAirTime -= Time.deltaTime;
            //Debug.Log("OnAir");
            return;

        }

        if (rb != null && target != null)
        {
           
            Vector2 direction = target.transform.position - transform.position;
            rb.velocity = direction.normalized * Random.Range(0.8f * speed, speed);
        }
    }

    public void KickOnAir(float onAirTime, int damage)
    {
        if (remainOnAirTime <= 0)
        {
            Debug.Log("OnAir");
            remainOnAirTime = onAirTime;
            collisionDamage = damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (remainOnAirTime > 0)
        {
            //Hit other enemies and cause damage
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<HurtBox>()?.TakeDamage(collisionDamage);
                collision.gameObject.GetComponent<Rigidbody2D>()?.AddForce(Vector3.up * 500);
                collision.gameObject.GetComponent<MoveTowardsPlayer>()?.KickOnAir(remainOnAirTime / 2, (int)collisionDamage);

                remainOnAirTime = 0;
            }

        }
    }

}
