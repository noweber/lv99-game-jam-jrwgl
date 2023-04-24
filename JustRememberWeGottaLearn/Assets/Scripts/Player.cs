using Assets.Scripts.HitHurt;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private int dmgWhenOverBF = 1;

    public Action OnPlayerAttack;
    public Action OnPlayerDash;
    //public Action OnTakeDeepBreath;
    //public Action OnStopTakeBreath;
    
    public HurtBox playerHurtBox;
    public Rigidbody2D playerRB;
    public TopDownPlayerController playerController;
    public bool stopTakingInput = false;

    [SerializeField] private KeyCode attackKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode takeDeepBreathKey = KeyCode.B;

    private bool isTakingBreath;
    private List<Card> m_collection = new List<Card>();
    public List<Card> CardCollection
    {
        get { return m_collection; }
    }
    public override void Awake()
    {
        base.Awake();
        isTakingBreath = false;
        playerHurtBox = GetComponent<HurtBox>();
        playerRB = GetComponent<Rigidbody2D>();
        playerController = GetComponent<TopDownPlayerController>();

    }
    private void Start()
    {
        Experience.Instance.OnLevelUp += HandleLevelUp;
    }
    public void AddCard(Card card)
    {
        m_collection.Add(card);
    }

    private void HandleLevelUp()
    {
        stopTakingInput = true;

    }
    private void Update()
    {
        if (stopTakingInput)
        {
            return;
        }
        playerController.SetMoveDirection(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Time.timeScale != 0f)
        {
            
            if (Input.GetKeyDown(attackKey))
            {
                if (!isTakingBreath)
                {
                    AudioManager.instance.RequestSFX(SFXTYPE.yell);
                    OnPlayerAttack.Invoke();
                }
            }
            else if (Input.GetKeyDown(dashKey))
            {
                if (!isTakingBreath)
                {
                    AudioManager.instance.RequestSFX(SFXTYPE.dash);
                    OnPlayerDash.Invoke();
                }
            }
        }

    }


}
