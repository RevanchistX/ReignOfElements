using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace MovementManagement
{
    public class MovementController : MonoBehaviour
    {
        private Rigidbody playerRigidBody;
        private PlayerInputActions playerInputActions;
        public float movementSpeed = 10f;
        public float jumpForce = 5f;
        private Stopwatch jumpTimer;

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody>();
            SetupControls();
        }

        private void SetupControls()
        {
            playerInputActions ??= new PlayerInputActions();
            playerInputActions.PlayerMovement.Move.performed += MovePlayer;
            playerInputActions.PlayerMovement.Jump.performed += JumpPerformed;
            playerInputActions.Enable();
        }

        private void JumpPerformed(InputAction.CallbackContext context)
        {
            var duration = Math.Max(Math.Min((float)context.duration, 3), 1);
            playerRigidBody.AddForce(Vector3.up * jumpForce * duration, ForceMode.Impulse);
        }

        private void MovePlayer(InputAction.CallbackContext context)
        {
            var inputDirection = context.ReadValue<Vector2>();
            playerRigidBody.velocity = new Vector3(inputDirection.x, 0, inputDirection.y) * movementSpeed
                                       + new Vector3(0, playerRigidBody.velocity.y, 0);
        }
    }
}