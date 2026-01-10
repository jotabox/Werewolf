using Werewolf.Player.States;

namespace Werewolf.Player.States.Grounded
{
    public abstract class GroundedState : PlayerState
    {
        protected GroundedState(
            PlayerController player,
            PlayerStateMachine stateMachine)
            : base(player, stateMachine)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

        }
    }
}
