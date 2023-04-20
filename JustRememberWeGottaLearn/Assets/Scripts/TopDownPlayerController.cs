using System;
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

    public Action<Vector3> OnPlayerMove;
    private Vector3 playerLastPosition;
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _currFaceDir = PlayerFaceDirection.right;
        playerLastPosition = transform.position;
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
            OnPlayerMove?.Invoke(transform.position - playerLastPosition);
            playerLastPosition = transform.position;
        }
        else
        {
            rb.velocity = moveDirection * speed;
            OnPlayerMove?.Invoke(transform.position - playerLastPosition);
            playerLastPosition = transform.position;
        }
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        rb.MovePosition(rb.position + (Vector2)newPosition);
        OnPlayerMove?.Invoke(transform.position - playerLastPosition);
        playerLastPosition = transform.position;
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
