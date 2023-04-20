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
   
    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    public PlayerFaceDirection _currFaceDir;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _currFaceDir = PlayerFaceDirection.right;
    }

    private void Update()
    {
        // Handle input
        float moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        float moveInputVertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveInputHorizontal, moveInputVertical).normalized;

        if(moveInputHorizontal < 0)
            _currFaceDir = PlayerFaceDirection.left;
        
        if(moveInputHorizontal > 0)
            _currFaceDir = PlayerFaceDirection.right;

        if(moveInputVertical < 0)
            _currFaceDir = PlayerFaceDirection.down;

        if (moveInputVertical > 0)
            _currFaceDir = PlayerFaceDirection.up;


    }

    private void FixedUpdate()
    {
        // Move the character
        //rb.velocity = new Vector2(moveInput * speed * sprintMultiplier, rb.velocity.y);
        rb.velocity = moveInput * speed;


    }

}
