using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance { get; private set; }
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float sprintModifier;
    [SerializeField] private float jumpMultiplier;
    private Rigidbody rigidbody;
    public bool IsGrounded;
    public bool IsMoving;
    public bool IsSprinting;
    private InputManager inputs;
    private Collider collider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        print(collider);
    }

    private void Start()
    {
        inputs = InputManager.Instance;
    }

    private void Update()
    {
        var totalSpeed = inputs.IsSprinting ? groundSpeed * sprintModifier : groundSpeed;
        // rigidbody.velocity = new Vector3(
        // inputs.SideMovement * totalSpeed,
        // rigidbody.velocity.y,
        // inputs.ForwardMovement * totalSpeed
        // );
        rigidbody.AddForce(new Vector3(
            inputs.SideMovement * totalSpeed,
            rigidbody.velocity.y,
            inputs.ForwardMovement * totalSpeed
        ));
        IsMoving = MovementCheck();
        IsGrounded = GroundCheck();
        IsSprinting = inputs.IsSprinting;
        if (inputs.IsJumping && IsGrounded)
        {
            rigidbody.AddForce(Physics.gravity * (jumpMultiplier * -1), ForceMode.Force);
        }

        Camera.main.transform.LookAt(transform.position);
    }

    private bool MovementCheck()
    {
        var velocity = rigidbody.velocity;
        velocity.y = 0;
        return Vector3.Magnitude(velocity) != 0;
    }

    private bool GroundCheck()
    {
        // var distance = collider.bounds.size.y / 2 + 0.1f;
        var distance = 0.1f;
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, distance, groundMask);
    }
}