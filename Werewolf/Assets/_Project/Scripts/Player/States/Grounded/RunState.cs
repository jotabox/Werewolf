using UnityEngine;
using Werewolf.Player;
using Werewolf.Player.States;
using Werewolf.Player.States.Grounded;

public class RunState : GroundedState
{
    public RunState(PlayerController player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }
}