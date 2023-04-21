using Assets.Scripts.HitHurt;
using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private int dmgWhenOverBF = 1;

    public Action OnPlayerAttack;
    public Action OnPlayerDash;
    public Action OnTakeDeepBreath;
    public Action OnStopTakeBreath;
    
    public HurtBox playerHurtBox;
    public Rigidbody2D playerRB;
    public TopDownPlayerController playerController;

    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode takeDeepBreathKey = KeyCode.B;

    private bool isTakingBreath;
    public override void Awake()
    {
        base.Awake();
        isTakingBreath = false;
        playerHurtBox = GetComponent<HurtBox>();
        playerRB = GetComponent<Rigidbody2D>();
        playerController = GetComponent<TopDownPlayerController>();

    }

    private void Update()
    {
        playerController.SetMoveDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(takeDeepBreathKey))
        {
            OnTakeDeepBreath.Invoke();
            isTakingBreath = true;
        }
        else if (Input.GetKeyUp(takeDeepBreathKey))
        {
            OnStopTakeBreath.Invoke();
            isTakingBreath = false;
        }
        else if (Input.GetKeyDown(attackKey))
        {
            if (!isTakingBreath)
            {
                OnPlayerAttack.Invoke();
            }
        }
        else if (Input.GetKeyDown(dashKey))
        {
            if (!isTakingBreath)
            {
                OnPlayerDash.Invoke();
            }
        }
        

    }


}
