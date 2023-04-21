using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BruceLeeKick : KungFu
{
    [SerializeField] private float DashTime = 0.1f;
    public GameObject attackAbility;
    public PushBox pushBox;
    public float forwardOffset;


    private float m_remainDashTime = 0;
    private Vector3 m_dashOrigin;
    private Vector3 m_dashTarget;


    private void Awake()
    {
        _stance = Stance.stance.BruceLee;
    }
    
    public override void Attack()
    {
        //throw new System.NotImplementedException();
        Vector3 offsetPosition = transform.position;
        Vector3 kickDirection = Vector3.zero;
        switch (Player.Instance.gameObject.GetComponent<TopDownPlayerController>()._currFaceDir)
        {
            case PlayerFaceDirection.right:
                offsetPosition = transform.position + new Vector3(1 * forwardOffset, 0, 0);
                kickDirection = Vector3.right;
                break;
            case PlayerFaceDirection.left:
                offsetPosition = transform.position + new Vector3(-1 * forwardOffset, 0, 0);
                kickDirection = Vector3.left;
                break;
            case PlayerFaceDirection.up:
                offsetPosition = transform.position + new Vector3(0, 1* forwardOffset, 0);
                kickDirection = Vector3.up;
                break;
            case PlayerFaceDirection.down:
                offsetPosition = transform.position + new Vector3(0, -1 * forwardOffset, 0);
                kickDirection = Vector3.down;
                break;

        }



        Instantiate(attackAbility, offsetPosition, transform.rotation);
        PushBox pushbox = Instantiate(pushBox, offsetPosition, transform.rotation);
        pushbox.SetDir(kickDirection);

    }

    protected override void Dash()
    {
        if (m_remainDashTime <= 0)
        {
            m_dashOrigin = transform.position;
            m_remainDashTime = DashTime;
            
            PlayerFaceDirection dashDirection = Player.Instance.GetComponent<TopDownPlayerController>()._currFaceDir;
            
            switch (dashDirection)
            {
                case PlayerFaceDirection.right:
                    m_dashTarget = m_dashOrigin + Vector3.right * dashDistance;
                    break;
                case PlayerFaceDirection.left:
                    m_dashTarget = m_dashOrigin + Vector3.left * dashDistance;
                    break;
                case PlayerFaceDirection.up:
                    m_dashTarget = m_dashOrigin + Vector3.up * dashDistance;
                    break;
                case PlayerFaceDirection.down:
                    m_dashTarget = m_dashOrigin + Vector3.down * dashDistance;
                    break;
            }
        }
     
    }

    private void FixedUpdate()
    {
      
        if (m_remainDashTime > 0)
        {
           
            Vector3 nextPosition = Vector3.Lerp(m_dashOrigin, m_dashTarget, (1 - m_remainDashTime / DashTime));
            m_remainDashTime -= Time.deltaTime;
            Player.Instance.playerController.MoveToPosition(nextPosition);
            //transform.position = nextPosition;
            PlayerAfterImagePool.Instance.GetFromPool();
        }
        
    }
    
}
