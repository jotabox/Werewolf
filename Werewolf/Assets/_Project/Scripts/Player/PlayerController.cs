using UnityEngine;
using Werewolf.Input;
using Werewolf.Core;

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
        private IInputService input;
        private bool isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            input = ServiceLocator.Current.Get<IInputService>();

            if (input == null)
            {
                Debug.LogError("IInputService not found. Ensure InputManager is initialized before Player.");
                enabled = false;
            }

        }

        private void Update()
        {
            CheckGround();

            if (input.JumpPressed && isGrounded)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            rb.linearVelocity = new Vector2(input.Move.x * moveSpeed, rb.linearVelocity.y);
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
