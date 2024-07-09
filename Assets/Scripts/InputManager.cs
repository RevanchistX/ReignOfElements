using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsSprinting { get; private set; }
    public float SideMovement { get; private set; }
    public float ForwardMovement { get; private set; }


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
    }

    private void Update()
    {
        SideMovement = Input.GetAxis("Horizontal");
        ForwardMovement = Input.GetAxis("Vertical");
        IsJumping = Input.GetKeyDown(KeyCode.Space);
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
    }
}