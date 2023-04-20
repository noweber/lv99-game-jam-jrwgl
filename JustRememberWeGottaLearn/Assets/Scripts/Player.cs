using Assets.Scripts.Damage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private int dmgWhenOverBF = 1;

    public AbilitySpawner abilitySpawner;
    public Action OnPlayerAttack;
    public Action OnPlayerDash;

    private BPM currentBPM;
    private HurtBox hurtBox;

    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    public override void Awake()
    {
        base.Awake();
        abilitySpawner = GetComponent<AbilitySpawner>();
    }

    public void Start()
    {
        TempoGenerator.Instance.OnBpmChange += SetCurrentBPM;
        hurtBox = GetComponent<HurtBox>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            if(currentBPM == BPM.bpm180plus)
            {
                hurtBox.TakeDamage(dmgWhenOverBF);
            }
            OnPlayerAttack.Invoke();
        }
        else if (Input.GetKeyDown(dashKey))
        {
            if (currentBPM == BPM.bpm180plus)
            {
                hurtBox.TakeDamage(dmgWhenOverBF);
            }

            OnPlayerDash.Invoke();
        }
    }

    private void SetCurrentBPM(BPM bpm)
    {
        currentBPM = bpm;
    }



}
