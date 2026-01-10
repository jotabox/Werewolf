using UnityEngine;
using Werewolf.Core;
using Werewolf.Input;
using Werewolf.Player.States;
using Werewolf.Player.States.Air;
using Werewolf.Player.States.Grounded;



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

        [Header("Coyote Time")]
        [SerializeField] private float coyoteTime = 0.1f;


        private InputBuffer inputBuffer;
        private Rigidbody2D rb;
        private IInputService input;
        private bool isGrounded;
        private float coyoteTimeCounter;
        public PlayerStateMachine StateMachine { get; private set; }
        public bool IsGrounded => isGrounded;
        public Vector2 MoveInput => input.Move;
        public IdleState IdleState { get; private set; }
        public RunState RunState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallState FallState { get; private set; }



        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            StateMachine = new PlayerStateMachine();
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

            IdleState = new IdleState(this, StateMachine);
            RunState = new RunState(this, StateMachine);
            JumpState = new JumpState(this, StateMachine);
            FallState = new FallState(this, StateMachine);

            StateMachine.Initialize(IdleState);


        }

        private void Update()
        {
            CheckGround();

            if (!isGrounded && StateMachine.CurrentState is GroundedState)
            {
                StateMachine.ChangeState(FallState);
            }

            StateMachine.CurrentState?.HandleInput();
            StateMachine.CurrentState?.LogicUpdate();

            // Registra intenção de pulo (buffer)
            if (input.JumpPressed)
            {
                inputBuffer.Register("Jump");
            }

            // Reseta estado ao tocar o chão
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
                hasJumped = false;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            // Executa pulo se permitido (ground + coyote time)
            if (!hasJumped && coyoteTimeCounter > 0f && inputBuffer.Consume("Jump"))
            {
                Jump();
                hasJumped = true;
                coyoteTimeCounter = 0f;
            }

        }

        private void FixedUpdate()
        {
            Debug.Log("FixedUpdate State = " + StateMachine.CurrentState.GetType().Name);
            StateMachine.CurrentState?.PhysicsUpdate();
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

        public void SetMovement(float direction)
        {
            Debug.Log("Applying velocity X = " + direction * moveSpeed);
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }

        public bool ConsumeJumpInput()
        {
            return inputBuffer.Consume("Jump");
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
