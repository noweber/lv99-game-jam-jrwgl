using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingchun : KungFu
{
    public float dashDist;
    public GameObject attackAbility;
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.WingChun;
        autoDashDistance = dashDist;
    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
    }
}
