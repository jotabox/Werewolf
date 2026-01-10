using Werewolf.Player.States;

namespace Werewolf.Player.States.Air
{
    public class FallState : PlayerState
    {
        public FallState(PlayerController player, PlayerStateMachine stateMachine)
            : base(player, stateMachine) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.IsGrounded)
            {
                if (player.MoveInput.x != 0)
                    stateMachine.ChangeState(player.RunState);
                else
                    stateMachine.ChangeState(player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            // Movimento no ar (controle aéreo)
            player.SetMovement(player.MoveInput.x);
        }
    }
}
