using Werewolf.Player.States;

namespace Werewolf.Player.States.Air
{
    public class FallState : PlayerState
    {
        public FallState(PlayerController player, PlayerStateMachine stateMachine)
            : base(player, stateMachine) { }
    }
}
