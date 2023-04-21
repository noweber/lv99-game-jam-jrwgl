using Assets.Scripts.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaolin : KungFu
{
    public GameObject attackAbility;
    protected void Awake()
    {
        _stance = Stance.stance.ShaolinKungFu;
    }
    
    public override void Attack()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
    }

    protected override void Dash()
    {
        PlayerFaceDirection dashDirection = Player.Instance.GetComponent<TopDownPlayerController>()._currFaceDir;
        switch (dashDirection)
        {
            case PlayerFaceDirection.right:
                //transform.position += Vector3.right * autoDashDistance;
                Player.Instance.playerController.MoveToPosition(Vector3.right * dashDistance);
                break;
            case PlayerFaceDirection.left:
                Player.Instance.playerController.MoveToPosition(Vector3.left * dashDistance);
                break;
            case PlayerFaceDirection.up:
                Player.Instance.playerController.MoveToPosition(Vector3.up * dashDistance);
                break;
            case PlayerFaceDirection.down:
                Player.Instance.playerController.MoveToPosition(Vector3.down * dashDistance);
                break;
        }
    }
}
