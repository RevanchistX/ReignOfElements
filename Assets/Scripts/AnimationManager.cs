using System;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private MovementManager movementManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        movementManager = MovementManager.Instance;
    }

    private void Update()
    {
        animator.SetBool(Animator.StringToHash("IsWalking"), movementManager.IsMoving);
        animator.SetBool(Animator.StringToHash("IsRunning"), movementManager.IsSprinting);
        animator.SetBool(Animator.StringToHash("IsDodging"), true);
    }
}