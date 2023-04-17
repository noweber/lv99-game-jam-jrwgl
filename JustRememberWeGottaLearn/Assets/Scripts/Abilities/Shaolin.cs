using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaolin : KungFu
{
    public GameObject attackAbility;
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.ShaolinKungFu;

    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
    }
}
