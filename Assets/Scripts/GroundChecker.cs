using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private Rigidbody playerRigidBody;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsGrounded = playerRigidBody.velocity.y == 0;
    }
}