using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingchun : KungFu
{
    public GameObject attackAbility;
    [SerializeField] private float DashTime = 0.1f;

    private float m_remainDashTime = 0;
    private Vector3 m_dashOrigin;
    private Vector3 m_dashTarget;

    [SerializeField]
    private GameObject VFX;
    [SerializeField]
    private Vector3 VFXOffset;
    
    private void Awake()
    {
        _stance = Stance.stance.WingChun;
    }

    public override void Attack()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
        Instantiate(VFX, transform.position +VFXOffset , transform.rotation);
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
