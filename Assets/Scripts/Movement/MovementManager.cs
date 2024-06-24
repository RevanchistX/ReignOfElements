using Movement.State;
using UnityEngine;

namespace Movement
{
    public class MovementManager : MonoBehaviour
    {
        private static MovementManager instance;
        private MovementState movementState;
        public static MovementManager Instance => instance;


        void Awake()
        {
            instance ??= this;
            movementState = new JumpingState();
        }

        public MovementState GetMovementState()
        {
            return movementState;
        }

        public MovementState ChangeMovementState(MovementState newMovementState)
        {
            movementState = newMovementState;
            return movementState;
        }
    }
}