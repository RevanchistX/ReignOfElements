using System.Collections.Generic;
using System.Linq;
using InputEngine;
using StateMachineEngine;
using StateMachineEngine.PlayerStates;
using Timer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private GroundChecker groundChecker;
    private Animator animator;
    private InputReader inputReader;

    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private float rotationSpeed = 15f;

    [SerializeField]
    private float smoothTime = 0.2f;

    [Header("Jump Settings")]
    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float jumpDuration = 0.5f;

    [SerializeField]
    private float jumpCooldown;

    [SerializeField]
    private float gravityMultiplier = 3f;


    [SerializeField]
    private float dodgeDuration = 0.5f;

    private float velocity;
    private float jumpVelocity;


    private List<Timer.Timer> timers;
    private CountdownTimer jumpTimer;
    private CountdownTimer jumpCooldownTimer;
    private CountdownTimer dodgeTimer;

    private StateMachine stateMachine;

    private static readonly int Forward = Animator.StringToHash("forward");
    private static readonly int Lateral = Animator.StringToHash("lateral");

    private void Awake()
    {
        SetupReferences();
        SetupTimers();
        SetupStateMachine();
    }

    private void SetupReferences()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.freezeRotation = true;
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
        inputReader = ScriptableObject.CreateInstance<InputReader>();
    }

    private void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var locomotionState = new LocomotionState(this, animator);
        var jumpState = new JumpState(this, animator);
        var dodgeState = new DodgeState(this, animator);

        stateMachine.AddTransition(locomotionState, jumpState, new FunctionPredicate(() => jumpTimer.IsRunning));
        stateMachine.AddTransition(locomotionState, dodgeState, new FunctionPredicate(() => dodgeTimer.IsRunning));
        stateMachine.AddAnyTransition(locomotionState, new FunctionPredicate(ReturnToLocomotionState));

        stateMachine.SetState(locomotionState);
    }

    private bool ReturnToLocomotionState()
    {
        return groundChecker.IsGrounded &&
               timers.All(timer => !timer.IsRunning);
        // && !jumpTimer.IsRunning;
    }

    private void SetupTimers()
    {
        jumpTimer = new CountdownTimer(jumpDuration);
        jumpCooldownTimer = new CountdownTimer(jumpCooldown);

        jumpTimer.OnTimerStart += () => jumpVelocity = jumpForce;
        jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

        dodgeTimer = new CountdownTimer(dodgeDuration);

        timers = new List<Timer.Timer>(5) { jumpTimer, jumpCooldownTimer, dodgeTimer };
    }

    private void Start() => inputReader.EnablePlayerActions();

    private void OnEnable()
    {
        inputReader.Jump += OnDodge;
        // inputReader.Jump += OnJump;
    }

    private void OnDisable()
    {
        inputReader.Jump -= OnDodge;
        // inputReader.Jump -= OnJump;
    }

    private void OnDodge(bool performed)
    {
        if (performed && !dodgeTimer.IsRunning && groundChecker.IsGrounded)
        {
            dodgeTimer.Start();
        }
        // else
        // {
        //     dodgeTimer.Stop();
        // }
    }

    private void OnJump(bool performed)
    {
        switch (performed)
        {
            case true when !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded:
                jumpTimer.Start();
                break;
            case false when jumpTimer.IsRunning:
                jumpTimer.Stop();
                break;
        }
    }

    private void Update()
    {
        stateMachine.Update();
        HandleTimers();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    public void HandleJump()
    {
        switch (jumpTimer.IsRunning)
        {
            case false when groundChecker.IsGrounded:
                jumpVelocity = 0f;
                return;
            case false:
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
                break;
        }

        playerRigidBody.velocity =
            new Vector3(playerRigidBody.velocity.x, jumpVelocity, playerRigidBody.velocity.z);
    }

    public void HandleDodge()
    {
        // Debug.Log(inputReader.Direction.x);
        // dodgeTimer.Stop();
        // animator.SetFloat("Dodge", 1 );
        animator.SetFloat("Dodge", inputReader.Direction.x < 0 ? 0 : 1);
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