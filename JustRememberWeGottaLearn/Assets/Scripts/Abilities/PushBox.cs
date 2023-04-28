using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    [SerializeField] private float pushForce = 1000f;
    [SerializeField] private Vector3 pushDir;
    [SerializeField] private float onAirDuration = 0.5f;
    [SerializeField] private int onAirDmg = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushDir * pushForce);
            //Debug.Log(collision.gameObject);
            //Debug.Log(collision.gameObject.GetComponent<MoveTowardsPlayer>());
            collision.gameObject.GetComponent<MoveTowardsPlayer>().KickOnAir(onAirDuration, onAirDmg);
        }
    }

    public void SetDir(Vector3 dir)
    {
        pushDir = dir;
    }
}
