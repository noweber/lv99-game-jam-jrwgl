using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    [SerializeField] private float maxSprintMultiplier = 2f; // Maximum speed multiplier when sprinting
    [SerializeField] private float sprintStaminaCost = 1f; // Stamina cost per second while sprinting
    [SerializeField] private float sprintStaminaRecoveryRate = 0.5f; // Stamina recovery rate per second when not sprinting
    [SerializeField] private float dashDistance = 5f; // Dash distance
    [SerializeField] private float dashCooldown = 2f; // Dash cooldown

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isSprinting;
    private float sprintMultiplier = 1f;
    [SerializeField] private float sprintStamina = 100f;
    [SerializeField] private float sprintMaxStamina = 100f;
    private float dashTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle input
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Update sprinting and stamina
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartSprint();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprint();
        }
        if (isSprinting)
        {
            sprintStamina = Mathf.Clamp(sprintStamina - sprintStaminaCost * Time.deltaTime, 0f, sprintMaxStamina);
            sprintMultiplier = Mathf.Lerp(1f, maxSprintMultiplier, sprintStamina / sprintMaxStamina);
        }
        else
        {
            sprintStamina = Mathf.Clamp(sprintStamina + sprintStaminaRecoveryRate * Time.deltaTime, 0f, sprintMaxStamina);
            sprintMultiplier = 1f;
        }
    }

    private void FixedUpdate()
    {
        // Move the character
        rb.velocity = new Vector2(moveInput * speed * sprintMultiplier, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the character is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the character is no longer grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void StartSprint()
    {
        if (!isSprinting && sprintStamina > 0f)
        {
            isSprinting = true;
        }
    }

    private void StopSprint()
    {
        isSprinting = false;
    }
}
