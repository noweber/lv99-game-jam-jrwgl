using Assets.Scripts.HitHurt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBreath : KungFu
{
    [SerializeField] private int OOBDamage = 1;
    [SerializeField] private float dashDist = 0.5f;

    public GameObject attackAbility;
    private void Awake()
    {

        _stance = Stance.stance.OutOfBreath;
        autoDashDistance = dashDist;
    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        if(attackAbility)
            Instantiate(attackAbility, transform.position, transform.rotation);


        GetComponent<HurtBox>().TakeDamage(OOBDamage);

    }

    protected override void DoPlayerDash()
    {
        if (enabled)
        {
            base.DoPlayerDash();
            GetComponent<HurtBox>().TakeDamage(OOBDamage);
        }
    }
}
