using UnityEngine;

namespace Werewolf.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float jumpForce = 14f;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody2D rb;
        private float moveInput;
        private bool isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ReadInput();
            CheckGround();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void ReadInput()
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }

        private void ApplyMovement()
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        private void Jump()
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
    }
}
