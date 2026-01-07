using UnityEngine;
using Werewolf.Core;

namespace Werewolf.Input
{
    public class InputManager : MonoBehaviour, IInputService
    {
        public Vector2 Move { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool DashPressed { get; private set; }
        public bool AttackPressed { get; private set; }

        private WerewolfInputActions inputActions;

        private void Awake()
        {
            inputActions = new WerewolfInputActions();
            ServiceLocator.Current.Register<IInputService>(this);
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            Move = inputActions.Gameplay.Move.ReadValue<Vector2>();
            JumpPressed = inputActions.Gameplay.Jump.WasPressedThisFrame();
            DashPressed = inputActions.Gameplay.Dash.WasPressedThisFrame();
            AttackPressed = inputActions.Gameplay.Attack.WasPressedThisFrame();
        }
    }
}
    