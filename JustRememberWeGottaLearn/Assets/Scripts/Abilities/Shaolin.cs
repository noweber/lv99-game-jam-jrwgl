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

    [SerializeField]
    private GameObject VFX;

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

            if (!isUpgrade)
            {
                foreach (var c in colliders)
                {
                    Vector2 dir = (c.transform.position - transform.position).normalized;
                    float angleBetween = Vector2.Angle(dir, dir2Enemy);
                    if (angleBetween <= 60 / 2f)
                    {
                        float angle = Mathf.Atan2(dir2Enemy.y, dir2Enemy.x) * Mathf.Rad2Deg;
                        Instantiate(attackAbility, c.transform.position, c.transform.rotation);
                        Instantiate(VFX, transform.position + dir2Enemy.normalized * 2.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                    }
                }
            }
            else
            {
                foreach (var c in colliders)
                {
                    Vector2 dir = (c.transform.position - transform.position).normalized;
                    float angleBetweenInverse = Vector2.Angle(dir, -dir2Enemy);
                    float angleBetween = Vector2.Angle(dir, dir2Enemy);
                    if (angleBetween <= 60 / 2f || angleBetweenInverse <= 60 / 2f)
                    {
                        float angle = Mathf.Atan2(dir2Enemy.y, dir2Enemy.x) * Mathf.Rad2Deg;
                        Instantiate(attackAbility, c.transform.position, c.transform.rotation);
                        Instantiate(VFX, transform.position + dir2Enemy.normalized * 2.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                    }
                }
            }
        }
        else
        {
            Vector3 offsetPosition = transform.position;
            Quaternion offsetRotation = transform.rotation;
            switch (Player.Instance.gameObject.GetComponent<TopDownPlayerController>()._currFaceDir)
            {
                case PlayerFaceDirection.right:
                    offsetPosition = transform.position + new Vector3(1 * 3, 0, 0);
                    offsetRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case PlayerFaceDirection.left:
                    offsetPosition = transform.position + new Vector3(-1 * 3, 0, 0);
                    offsetRotation = Quaternion.Euler(0, 0, 180);
                    break;
                case PlayerFaceDirection.up:
                    offsetPosition = transform.position + new Vector3(0, 1 * 3, 0);
                    offsetRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case PlayerFaceDirection.down:
                    offsetPosition = transform.position + new Vector3(0, -1 * 3, 0);
                    offsetRotation = Quaternion.Euler(0, 0, -90);
                    break;
            }

            Instantiate(VFX, offsetPosition, offsetRotation);
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
