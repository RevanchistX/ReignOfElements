using System.Collections.Generic;
using System.Linq;
using InputEngine;
using StateMachineEngine;
using StateMachineEngine.PlayerStates;
using Timer;
using UnityEngine;

public class PlayerController : BaseController
{
    private Rigidbody playerRigidBody;
    private GroundChecker groundChecker;
    private Animator animator;
    private InputReader inputReader;

    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private float smoothTime = 0.2f;

    [Header("Jump Settings")]
    [SerializeField]
    private float jumpForce = 3f;

    [SerializeField]
    private float jumpDuration = 1f;


    private CountdownTimer jumpTimer;
    private static readonly int Forward = Animator.StringToHash("forward");
    private static readonly int Lateral = Animator.StringToHash("lateral");

    public override void SetupReferences()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
        inputReader = ScriptableObject.CreateInstance<InputReader>();
    }

    public override void SetupStateMachine()
    {
        StateMachine = new StateMachine();

        var locomotionState = new LocomotionState(animator, HandleMovement);
        var jumpState = new JumpState(animator, HandleJump);

        StateMachine.AddTransition(locomotionState, jumpState, new FunctionPredicate(() => jumpTimer.IsRunning));
        StateMachine.AddAnyTransition(locomotionState, new FunctionPredicate(ReturnToLocomotionState));

        StateMachine.SetState(locomotionState);
    }

    private bool ReturnToLocomotionState()
    {
        return groundChecker.IsGrounded &&
               Timers.All(timer => !timer.IsRunning);
    }

    public override void SetupTimers()
    {
        jumpTimer = new CountdownTimer(jumpDuration);

        Timers = new List<Timer.Timer>(1) { jumpTimer };
    }

    private void Start() => inputReader.EnablePlayerActions();

    private void OnEnable()
    {
        inputReader.Jump += OnJump;
    }

    private void OnDisable()
    {
        inputReader.Jump -= OnJump;
    }

    private void OnJump(bool performed)
    {
        switch (performed)
        {
            case true when !jumpTimer.IsRunning && groundChecker.IsGrounded:
                jumpTimer.Start();
                break;
            case false when jumpTimer.IsRunning:
                jumpTimer.Stop();
                break;
        }
    }

    public void HandleJump()
    {
        playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void HandleMovement()
    {
        var lateralDirection = new Vector3(inputReader.Direction.x, 0, 0);
        var forwardDirection = new Vector3(0, 0, inputReader.Direction.y);
        var gravity = new Vector3(0, playerRigidBody.velocity.y, 0);
        var movementDirection = (lateralDirection + forwardDirection) * moveSpeed;
        var animatorForwardValue = animator.GetFloat(Forward);
        var animatorLateralValue = animator.GetFloat(Lateral);

        playerRigidBody.velocity = gravity;
        animator.SetFloat(Forward, Mathf.Lerp(animatorForwardValue, 0, smoothTime));
        animator.SetFloat(Lateral, Mathf.Lerp(animatorLateralValue, 0, smoothTime));

        if (!(movementDirection.magnitude > 0)) return;
        playerRigidBody.velocity = movementDirection + gravity;
        animator.SetFloat(Forward, Mathf.Lerp(animatorForwardValue, forwardDirection.z, smoothTime));
        animator.SetFloat(Lateral, Mathf.Lerp(animatorLateralValue, lateralDirection.x, smoothTime));
    }
}