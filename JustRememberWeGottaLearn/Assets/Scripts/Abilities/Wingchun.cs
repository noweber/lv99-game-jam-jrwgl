using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingchun : KungFu
{
    public GameObject attackAbility;
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.WingChun;

    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
    }
}