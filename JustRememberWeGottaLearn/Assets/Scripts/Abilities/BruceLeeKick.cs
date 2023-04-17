using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruceLeeKick : KungFu
{
    public GameObject attackAbility;
    public float forwardOffset;
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.BruceLee;

    }

    public override void Perform()
    {
        //throw new System.NotImplementedException();
        Vector3 offsetPosition = transform.position;
        switch (Player.Instance.gameObject.GetComponent<TopDownPlayerController>()._currFaceDir)
        {
            case PlayerFaceDirection.right:
                offsetPosition = transform.position + new Vector3(1 * forwardOffset, 0, 0);
                break;
            case PlayerFaceDirection.left:
                offsetPosition = transform.position + new Vector3(-1 * forwardOffset, 0, 0);
                break;
            case PlayerFaceDirection.up:
                offsetPosition = transform.position + new Vector3(0, 1* forwardOffset, 0);
                break;
            case PlayerFaceDirection.down:
                offsetPosition = transform.position + new Vector3(0, -1 * forwardOffset, 0);
                break;

        }



        Instantiate(attackAbility, offsetPosition, transform.rotation);
    }
}
