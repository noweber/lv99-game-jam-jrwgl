//using Assets.Scripts.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaolin : KungFu
{
    [SerializeField] private float DashTime = 0.1f;

    private float m_remainDashTime = 0;
    private Vector3 m_dashOrigin;
    private Vector3 m_dashTarget;

    public GameObject attackAbility;

    [SerializeField]
    private bool isUpgrade;

    protected void Awake()
    {
        _stance = Stance.stance.ShaolinKungFu;
    }

    public override void Attack()
    {
        //throw new System.NotImplementedException();
        //Instantiate(attackAbility, transform.position, transform.rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 6.0f, LayerMask.GetMask("Enemy"));
        if (colliders.Length > 0)
        {
            Transform minTransform = colliders[0].transform;
            float distance = (transform.position - minTransform.position).magnitude;
            foreach (var c in colliders)
            {
                float tmp = (transform.position - c.transform.position).magnitude;
                if (tmp < distance)
                {
                    minTransform = c.transform;
                    distance = tmp;
                }
            }

            Vector3 dir2Enemy = (minTransform.position - transform.position).normalized;
            foreach (var c in colliders)
            {
                Vector2 dir = (c.transform.position - transform.position).normalized;
                if (!isUpgrade)
                {
                    float angleBetween = Vector2.Angle(dir, dir2Enemy);
                    if (angleBetween <= 60 / 2f)
                    {
                        Instantiate(attackAbility, c.transform.position, c.transform.rotation);
                    }
                }
                else
                {
                    float angleBetweenInverse = Vector2.Angle(dir, -dir2Enemy);
                    float angleBetween = Vector2.Angle(dir, dir2Enemy);
                    Debug.Log(angleBetweenInverse);
                    if (angleBetween <= 60 / 2f || angleBetweenInverse <= 60 / 2f)
                    {
                        Instantiate(attackAbility, c.transform.position, c.transform.rotation);
                    }
                }

            }
        }

    }



    protected override void Dash()
    {
        m_dashOrigin = transform.position;
        m_remainDashTime = DashTime;
        m_dashTarget = m_dashOrigin + Player.Instance.playerController.GetPlayerDirection() * dashDistance;
    }

    private void FixedUpdate()
    {

        if (m_remainDashTime > 0)
        {

            Vector3 nextPosition = Vector3.Lerp(m_dashOrigin, m_dashTarget, (1 - m_remainDashTime / DashTime));
            m_remainDashTime -= Time.deltaTime;
            Player.Instance.playerController.MoveToPosition(nextPosition);
            //transform.position = nextPosition;
            //PlayerAfterImagePool.Instance.GetFromPool();
        }

    }
}
