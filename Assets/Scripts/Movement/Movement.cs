using Movement.State;
using Spellcasting;
using UnityEngine;

namespace Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Camera camera;
        private Rigidbody rigidbody;

        void Start()
        {
            camera = Camera.main;
            rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            var state = MovementManager.Instance.ChangeMovementState(Input.GetMouseButtonDown(2)
                ? new JumpingState()
                : new MovementState());
            if (state.IsJumping())
            {
                Debug.Log(state);
                rigidbody.AddForce(Vector3.up * state.VerticalSpeed);
            }

            var rotateQ = Input.GetKeyDown("q");
            var rotateE = Input.GetKeyDown("e");
            var crossVector = Vector3.Cross(transform.position, Vector3.up);
            var angle = 25;
            if (rotateE)
            {
                // rigidbody.AddForce(crossVector);
                transform.Rotate(crossVector, angle);
            }

            if (rotateQ)
            {
                // rigidbody.AddForce(crossVector);
                transform.Rotate(crossVector, -angle);
            }

            var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            var speedDelta = moveSpeed * Time.deltaTime;
            var spellcastingState = SpellcastingManager.Instance.SpellcastingState;
            var speedDirection = moveDirection * (speedDelta * spellcastingState.GetMovementModifier());
            transform.position += speedDirection;
            camera.transform.position += speedDirection;
            camera.transform.LookAt(transform.position);
        }
    }
}