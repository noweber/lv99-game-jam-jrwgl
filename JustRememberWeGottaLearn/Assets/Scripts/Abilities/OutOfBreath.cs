using Assets.Scripts.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBreath : KungFu
{
    [SerializeField] private int OOBDamage = 1;
    

    public GameObject attackAbility;
    private void Awake()
    {
        _stance = Stance.stance.OutOfBreath;
    }
    
    public override void Attack()
    {
        //throw new System.NotImplementedException();
        if(attackAbility)
            Instantiate(attackAbility, transform.position, transform.rotation);


        GetComponent<HurtBox>().TakeDamage(OOBDamage);

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

        GetComponent<HurtBox>().TakeDamage(OOBDamage);
    }
}
