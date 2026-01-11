using UnityEngine;
using Werewolf.Player.States;

namespace Werewolf.Player.States.Air
{
    public class JumpState : PlayerState
    {
        private bool jumpApplied;

        public JumpState(PlayerController player, PlayerStateMachine stateMachine)
            : base(player, stateMachine) { }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Enter JumpState");


            jumpApplied = false;
            ApplyJump();
        }

        private void ApplyJump()
        {
            if (jumpApplied) return;

            player.Jump();
            jumpApplied = true;
        }

        public override void PhysicsUpdate()
        {
            // Controle aéreo
            player.SetMovement(player.MoveInput.x);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // Começou a cair
            if (player.RigidbodyVelocityY <= 0f)
            {
                Debug.Log("Jump -> Fall");
                stateMachine.ChangeState(player.FallState);
            }
        }
    }
}
