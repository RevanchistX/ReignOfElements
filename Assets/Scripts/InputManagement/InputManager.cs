using UnityEngine;

namespace InputManagement
{
    public class InputManager : MonoBehaviour
    {
        private MovementControls movementControls;
        public Vector2 movementInput;

        private void OnEnable()
        {
            movementControls ??= new MovementControls();
            movementControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            movementControls.Enable();
        }

        private void OnDisable()
        {
            movementControls.Disable();
        }
    }
}