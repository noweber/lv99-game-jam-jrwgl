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

    [SerializeField]
    private bool isUpgrade;
    private bool isUpgradeAttacking;
    [SerializeField]
    private float upgradeAttackMoveTime = 0;

    [SerializeField]
    private GameObject VFX;

    private void Awake()
    {
        _stance = Stance.stance.BruceLee;
    }

    public void DoUpgrade()
    {
        isUpgrade = true;
        isUpgradeAttacking = true;
    }
    
    public override void Attack()
    {
        //throw new System.NotImplementedException();
        if(!isUpgrade)
            AttackOrigin();
        else if(isUpgrade && !isUpgradeAttacking)
        {
            AttackUpgrade();
        }
    }

    private void AttackUpgrade()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20f, LayerMask.GetMask("Enemy"));
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

            Vector3 dir2Enemy = minTransform.position - transform.position;

            float angle = Mathf.Atan2(dir2Enemy.y, dir2Enemy.x) * Mathf.Rad2Deg;
            if(dir2Enemy.magnitude >= 8)
            {
                isUpgradeAttacking = true;
                m_dashOrigin = transform.position;
                m_dashTarget = transform.position + dir2Enemy.normalized * (dir2Enemy.magnitude - 8);
                upgradeAttackMoveTime = DashTime;
            }
            else
            {
                isUpgradeAttacking = false;
                Instantiate(attackAbility, transform.position + dir2Enemy.normalized * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                Instantiate(VFX, transform.position + dir2Enemy.normalized * 2.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                PushBox pushbox = Instantiate(pushBox, transform.position + dir2Enemy.normalized * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                pushbox.SetDir(dir2Enemy);
            }
        }
        else
        {
            isUpgradeAttacking = false;
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
                    offsetPosition = transform.position + new Vector3(0, 1 * forwardOffset, 0);
                    kickDirection = Vector3.up;
                    break;
                case PlayerFaceDirection.down:
                    offsetPosition = transform.position + new Vector3(0, -1 * forwardOffset, 0);
                    kickDirection = Vector3.down;
                    break;

            }

            Instantiate(attackAbility, offsetPosition, transform.rotation);
            Instantiate(VFX, offsetPosition, transform.rotation);
            PushBox pushbox = Instantiate(pushBox, offsetPosition, transform.rotation);
            pushbox.SetDir(kickDirection);
        }
    }

    private void AttackOrigin()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 8.0f, LayerMask.GetMask("Enemy"));
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
            float angle = Mathf.Atan2(dir2Enemy.y, dir2Enemy.x) * Mathf.Rad2Deg;
            Instantiate(attackAbility, transform.position + dir2Enemy * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
            Instantiate(VFX, transform.position + dir2Enemy * 2.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
            PushBox pushbox = Instantiate(pushBox, transform.position + dir2Enemy * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
            pushbox.SetDir(dir2Enemy);
        }
        else
        {
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
                    offsetPosition = transform.position + new Vector3(0, 1 * forwardOffset, 0);
                    kickDirection = Vector3.up;
                    break;
                case PlayerFaceDirection.down:
                    offsetPosition = transform.position + new Vector3(0, -1 * forwardOffset, 0);
                    kickDirection = Vector3.down;
                    break;

            }

            Instantiate(attackAbility, offsetPosition, transform.rotation);
            Instantiate(VFX, offsetPosition, transform.rotation);
            PushBox pushbox = Instantiate(pushBox, offsetPosition, transform.rotation);
            pushbox.SetDir(kickDirection);
        }
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
      
        if (m_remainDashTime > 0 && !isUpgradeAttacking)
        {
           
            Vector3 nextPosition = Vector3.Lerp(m_dashOrigin, m_dashTarget, (1 - m_remainDashTime / DashTime));
            m_remainDashTime -= Time.deltaTime;
            Player.Instance.playerController.MoveToPosition(nextPosition);
            //transform.position = nextPosition;
            PlayerAfterImagePool.Instance.GetFromPool();
        }
        
        if(isUpgradeAttacking)
        {
            Vector3 nextPosition = Vector3.Lerp(m_dashOrigin, m_dashTarget, (1 - upgradeAttackMoveTime / DashTime));
            upgradeAttackMoveTime -= Time.deltaTime;
            Player.Instance.playerController.MoveToPosition(nextPosition);
            PlayerAfterImagePool.Instance.GetFromPool();
            if(upgradeAttackMoveTime <= 0)
            {
                Debug.Log("awd");
                Vector3 dir2Enemy = (m_dashTarget - m_dashOrigin).normalized;
                float angle = Mathf.Atan2(dir2Enemy.y, dir2Enemy.x) * Mathf.Rad2Deg;
                Instantiate(attackAbility, transform.position + dir2Enemy * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                Instantiate(VFX, transform.position + dir2Enemy * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                PushBox pushbox = Instantiate(pushBox, transform.position + dir2Enemy * 4.0f, Quaternion.Euler(new Vector3(0, 0, angle)));
                pushbox.SetDir(dir2Enemy);
                isUpgradeAttacking = false;
                upgradeAttackMoveTime = 0;
            }
        }


    }
    
}
