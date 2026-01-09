using Werewolf.Player.States.Grounded;

namespace Werewolf.Player.States.Grounded
{
    public class IdleState : GroundedState
    {
        public IdleState(
            PlayerController player,
            PlayerStateMachine stateMachine)
            : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetHorizontalVelocity(0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.MoveInput.x != 0)
            {
                stateMachine.ChangeState(player.RunState);
            }

            if (player.ConsumeJumpInput())
            {
                stateMachine.ChangeState(player.JumpState);
            }
        }
    }
}
