using TMPro;
using UnityEngine;

public class SideScrollerPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private float jumpForce = 10f; // Jump force
    [SerializeField] private float maxSprintMultiplier = 2f; // Maximum speed multiplier when sprinting
    [SerializeField] private float sprintStaminaCost = 1f; // Stamina cost per second while sprinting
    [SerializeField] private float sprintStaminaRecoveryRate = 0.5f; // Stamina recovery rate per second when not sprinting
    [SerializeField] private float dashDistance; // Dash distance
    [SerializeField] private float dashSpeed; // Dash distance
    [SerializeField] private float dashTime; // Dash time
    [SerializeField] private float dashAfterImageDistance; // image between after images
    [SerializeField] private float dashCooldown; // Dash cooldown
    [SerializeField] private TextMeshProUGUI staminaText;
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isSprinting;
    private bool isDashing;
    private float sprintMultiplier = 1f;
    private float sprintStamina = 100f;
    private float sprintMaxStamina = 100f;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle input
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }


        if(Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash += dashCooldown))
                Attemp2Dash();

        }

        checkDash();

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
        UpdateStaminaText();
    }

    private void FixedUpdate()
    {
        // Move the character
        if(!isDashing)
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

    private void Attemp2Dash()
    {

        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
    }

    private void checkDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;
            }

            if(Mathf.Abs(transform.position.x - lastImageXpos) > dashAfterImageDistance)
            {
                PlayerAfterImagePool.Instance.GetFromPool();
                lastImageXpos = transform.position.x;
            }

            if (dashTimeLeft < 0)
            {
                isDashing = false;
            }
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

    private void UpdateStaminaText()
    {
        if (staminaText != null)
        {
            staminaText.text = Mathf.RoundToInt((sprintStamina / sprintMaxStamina) * 100f).ToString();
        }
    }
}
