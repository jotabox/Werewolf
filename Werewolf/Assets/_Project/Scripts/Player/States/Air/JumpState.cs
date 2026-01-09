using Werewolf.Player.States;

namespace Werewolf.Player.States.Air
{
    public class JumpState : PlayerState
    {
        public JumpState(PlayerController player, PlayerStateMachine stateMachine)
            : base(player, stateMachine) { }
    }
}
