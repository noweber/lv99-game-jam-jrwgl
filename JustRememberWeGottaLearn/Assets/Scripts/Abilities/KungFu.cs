using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class KungFu : MonoBehaviour
{
    [SerializeField] protected float dashDistance;

    protected Stance.stance _stance;
    
    protected int attackBreathIncrease = 3;
    protected int dashBreathIncrease = 2;

    public Action<int> OnKungFuAttack;
    public Action<int> OnKungFuDash;

    protected virtual void Start()
    {
        //Debug.Log("KungFu start");
        //Debug.Log(gameObject);
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

    protected virtual void DoPlayerAttack()
    {
        if (enabled && Time.timeScale != 0)
        {
            Attack();
            OnKungFuAttack.Invoke(attackBreathIncrease);
        }
    }

    protected virtual void DoPlayerDash()
    {
        if (enabled && Time.timeScale != 0)
        {
            Dash();
            OnKungFuDash.Invoke(dashBreathIncrease);
        }

    }

    public abstract void Attack();
    protected abstract void Dash();

    
}
