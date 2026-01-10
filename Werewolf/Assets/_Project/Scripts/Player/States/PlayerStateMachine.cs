using UnityEngine;

namespace Werewolf.Player.States
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            Debug.Log($"FSM: {CurrentState?.GetType().Name} -> {newState?.GetType().Name}");
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
