using UnityEngine;

namespace Werewolf.Core
{
    public enum GameState
    {
        Boot,
        MainMenu,
        Gameplay,
        Paused
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentState { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            ServiceLocator.Current.Clear();
            EventManager.Clear();
        }

        private void Start()
        {
            SetState(GameState.Boot);
            Debug.Log($"Game State initialized as: {CurrentState}");
        }

        public void SetState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            CurrentState = newState;

            Debug.Log($"Game State changed to: {CurrentState}");
        }
    }
}
