using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class KungFu : MonoBehaviour
{
    
    protected Stance.stance _stance;
    protected float autoDashDistance = 1.0f;

    protected virtual void Start()
    {
        enabled = false;
        Stance.Instance.onSwitchStance += TryEnable;
        
       
        Player.Instance.OnPlayerAttack += DoPlayerAttack;
        Player.Instance.OnPlayerDash += DoPlayerDash;
        TryEnable();
        
    }

    protected void TryEnable()
    {
        if(_stance == Stance.Instance.currentStance)
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void DoPlayerAttack()
    {
        if (enabled)
        {
            Perform();
        }
    }

    protected virtual void DoPlayerDash()
    {
        if (enabled)
        {
            PlayerFaceDirection dashDirection = Player.Instance.GetComponent<TopDownPlayerController>()._currFaceDir;
            switch (dashDirection)
            {
                case PlayerFaceDirection.right:
                    transform.position += Vector3.right * autoDashDistance;
                    break;
                case PlayerFaceDirection.left:
                    transform.position += Vector3.left * autoDashDistance;
                    break;
                case PlayerFaceDirection.up:
                    transform.position += Vector3.up * autoDashDistance;
                    break;
                case PlayerFaceDirection.down:
                    transform.position += Vector3.down * autoDashDistance;
                    break;
            }
        }

    }

    
    public abstract void Perform();


    
}
