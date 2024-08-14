using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace InputEngine
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction<bool> HarvestElement = delegate { };

        private PlayerInputActions inputActions;

        public Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();

        private void OnEnable()
        {
            if (inputActions != null) return;
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }

        public void EnablePlayerActions() => inputActions.Enable();

        public void OnMove(InputAction.CallbackContext context)
        {
            //noop
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }

        public void OnHarvestElement(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    HarvestElement.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    HarvestElement.Invoke(false);
                    break;
            }
        }
    }
}