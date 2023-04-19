using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaolin : KungFu
{
    [SerializeField] private float dashDist;

    public GameObject attackAbility;
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.ShaolinKungFu;
        autoDashDistance = dashDist;
    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
    }
}
