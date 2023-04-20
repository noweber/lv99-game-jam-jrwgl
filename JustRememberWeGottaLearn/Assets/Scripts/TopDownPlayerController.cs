using TMPro;
using UnityEngine;


public enum PlayerFaceDirection
{
    left,
    right,
    up,
    down
}

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private float speedWhenTakingBreath = 1.0f;
   
    private bool isTakingBreath;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    
    public PlayerFaceDirection _currFaceDir;
    
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _currFaceDir = PlayerFaceDirection.right;
    }

    private void Start()
    {
        Player.Instance.OnTakeDeepBreath += HandleTakeBreath;
        Player.Instance.OnStopTakeBreath += HandleStopTakeBreath;
    }
    public void SetMoveDirection(float moveInputHorizontal, float moveInputVertical)
    {
        moveDirection = new Vector2(moveInputHorizontal, moveInputVertical).normalized;

        if (moveInputHorizontal < 0)
            _currFaceDir = PlayerFaceDirection.left;

        if (moveInputHorizontal > 0)
            _currFaceDir = PlayerFaceDirection.right;

        if (moveInputVertical < 0)
            _currFaceDir = PlayerFaceDirection.down;

        if (moveInputVertical > 0)
            _currFaceDir = PlayerFaceDirection.up;
    }

    private void FixedUpdate()
    {
        // Move the character
        if (isTakingBreath)
        {
            rb.velocity = moveDirection * speedWhenTakingBreath;
        }
        else
        {
            rb.velocity = moveDirection * speed;

        }
    }

    private void HandleTakeBreath()
    {
        isTakingBreath = true;
    }
    private void HandleStopTakeBreath()
    {
        isTakingBreath = false;
    }
}
