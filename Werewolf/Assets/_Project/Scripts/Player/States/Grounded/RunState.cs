using System.Diagnostics;
using Werewolf.Player.States.Grounded;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace Werewolf.Player.States.Grounded
{
    public class RunState : GroundedState
    {
        public RunState(PlayerController player, PlayerStateMachine stateMachine)
            : base(player, stateMachine)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // Parou de se mover -> Idle
            if (player.MoveInput.x == 0)
            {
                stateMachine.ChangeState(player.IdleState);
                return;
            }

            // Jump
            if (player.ConsumeJumpInput())
            {
                stateMachine.ChangeState(player.JumpState);
            }
        }

        public override void PhysicsUpdate()
        {
            //Debug.Log("RUN STATE PHYSICS");

            base.PhysicsUpdate();

            player.SetMovement(player.MoveInput.x);
        }
    }
}
