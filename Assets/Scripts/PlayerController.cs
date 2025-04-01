using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    private float currentSpeed = 0f;

    [Header("Components")]
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        // Get input (using Unity's input system)
        movementInput.x = Input.GetAxisRaw("Horizontal");

        movementInput.y = Input.GetAxisRaw("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Normalize diagonal movement
        if(movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }

        HandleFlip();
    }

    private void FixedUpdate()
    {
        HandleMovement();

        UpdateAnimations();
    }

    private void HandleMovement()
    {
        float baseSpeed = isSprinting ? sprintSpeed : moveSpeed;

        // Calculate target speed
        float targetSpeed = movementInput.magnitude * baseSpeed;

        // Accelerate or decelerate
        if(movementInput.magnitude > 0.1f)
        {
            float currentAcceleration = isSprinting ? acceleration * 1.5f : acceleration;

            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, currentAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }

        // Apply movement
        if(currentSpeed > 0.1f)
        {
            Vector2 moveForce = movementInput * currentSpeed;

            rb.linearVelocity = moveForce;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void UpdateAnimations()
    {
        if(animator != null)
        {
            bool isMoving = rb.linearVelocity.magnitude > 0f;

            animator.SetBool("isMoving", isMoving);
        }
    }

    private void HandleFlip()
    {
        // Only flip if there's significant horizontal movement
        if(Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }
    }
}
