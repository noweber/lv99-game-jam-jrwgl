using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingchun : KungFu
{
    public GameObject attackAbility;

    [SerializeField]
    private GameObject VFX;
    [SerializeField]
    private Vector3 VFXOffset;
    
    private void Awake()
    {
        _stance = Stance.stance.WingChun;
    }

    public override void Attack()
    {
        //throw new System.NotImplementedException();
        Instantiate(attackAbility, transform.position, transform.rotation);
        Instantiate(VFX, transform.position +VFXOffset , transform.rotation);
    }

    protected override void Dash()
    {
        PlayerFaceDirection dashDirection = Player.Instance.GetComponent<TopDownPlayerController>()._currFaceDir;
        switch (dashDirection)
        {
            case PlayerFaceDirection.right:
                //transform.position += Vector3.right * autoDashDistance;
                Player.Instance.playerController.MoveToPosition(transform.position + Vector3.right * dashDistance);
                break;
            case PlayerFaceDirection.left:
                Player.Instance.playerController.MoveToPosition(transform.position + Vector3.left * dashDistance);
                break;
            case PlayerFaceDirection.up:
                Player.Instance.playerController.MoveToPosition(transform.position + Vector3.up * dashDistance);
                break;
            case PlayerFaceDirection.down:
                Player.Instance.playerController.MoveToPosition(transform.position + Vector3.down * dashDistance);
                break;
        }
    }
}
