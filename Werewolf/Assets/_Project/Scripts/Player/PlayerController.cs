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
        private bool hasJumped;

        [Header("Input Buffer")]
        [SerializeField] private float jumpBufferTime = 0.15f;

        private InputBuffer inputBuffer;
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
            inputBuffer = new InputBuffer(jumpBufferTime);


            if (input == null)
            {
                Debug.LogError("IInputService not found. Ensure InputManager is initialized before Player.");
                enabled = false;
            }

        }

        private void Update()
        {
            CheckGround();

            // Registra intenção de pulo (buffer)
            if (input.JumpPressed)
            {
                inputBuffer.Register("Jump");
            }

            // Reseta estado ao tocar o chão
            if (isGrounded)
            {
                hasJumped = false;
            }

            // Executa pulo apenas se permitido
            if (isGrounded && !hasJumped && inputBuffer.Consume("Jump"))
            {
                Jump();
                hasJumped = true;
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
