using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera camera;

    public CharacterController controller;
    public float playerSpeed;

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        var movement = transform.forward * vertical + transform.right * horizontal;
        var mouseX = Input.GetAxis("Mouse X");
        
        var rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rotation.x, rotation.y + mouseX, rotation.z);
        
        camera.transform.LookAt(transform);
        var isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
        if (isCtrlPressed) return;
        controller.Move(movement * (playerSpeed * Time.fixedDeltaTime));
    }
}

// using UnityEngine;
//
// public class Movement : MonoBehaviour
// {
//     public float speed = 5f;
//     public float sensitivity = 2f;
//     public float gravity = -20f;
//     public float verticalSpeed = 3;
//     public float jumpSpeed = 15f;
//     private bool isJumping = false;
//
//     private CharacterController characterController;
//     private Animator animator;
//     public Camera playerFollowCamera;
//     private static readonly int IsWalking = Animator.StringToHash("isWalking");
//     private static readonly int Speed = Animator.StringToHash("Speed");
//     private static readonly int IsJumping = Animator.StringToHash("isJumping");
//
//     private void Start()
//     {
//         characterController = GetComponent<CharacterController>();
//         animator = GetComponentInParent<Animator>();
//
//         // Lock cursor for better camera control
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }
//
//     private void Update()
//     {
//         // Player Movement
//         MovePlayer();
//
//         // Player Look
//         RotatePlayer();
//     }
//
//
//     private void MovePlayer()
//     {
//         var position = characterController.transform.position;
//         playerFollowCamera.transform.position = position + new Vector3(10, 10, 10);
//         playerFollowCamera.transform.LookAt(position);
//         var horizontal = Input.GetAxis("Horizontal");
//         var vertical = Input.GetAxis("Vertical");
//
//         var movement = new Vector3(horizontal, 0f, vertical);
//
//         if (movement == Vector3.zero)
//         {
//             animator.SetFloat(Speed, 0f);
//         }
//         else if (!Input.GetKey(KeyCode.LeftShift))
//         {
//             animator.SetFloat(Speed, 0.5f);
//         }
//         else
//         {
//             animator.SetFloat(Speed, 1f);
//         }
//
//         movement = transform.TransformDirection(movement);
//         characterController.SimpleMove(movement * speed);
//
//         isJumping = Input.GetKey(KeyCode.Space);
//         var moveVelocity = transform.forward * vertical;
//         animator.SetBool(IsJumping, false);
//
//         moveVelocity.y += gravity * Time.deltaTime;
//
//
//         characterController.Move(moveVelocity * Time.deltaTime);
//         if (!isJumping) return;
//         moveVelocity.y = jumpSpeed;
//         moveVelocity.x = -jumpSpeed;
//         animator.SetBool(IsJumping, true);
//         isJumping = false;
//         characterController.Move(moveVelocity * Time.deltaTime);
//     }
//
//     private void RotatePlayer()
//     {
//         var mouseX = Input.GetAxis("Mouse X") * sensitivity;
//         // var mouseY = Input.GetAxis("Mouse Y") * sensitivity;
//         
//         // Rotate the player around the Y-axis
//         transform.Rotate(Vector3.up * mouseX);
//         // //
//         // // // Rotate the camera around the X-axis
//         // // var rotationX = playerFollowCamera.transform.rotation.eulerAngles.x - mouseY;
//         // // rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit the camera's vertical rotation
//         // //
//         // playerFollowCamera.transform.rotation = characterController.transform.rotation;
//     }
// }