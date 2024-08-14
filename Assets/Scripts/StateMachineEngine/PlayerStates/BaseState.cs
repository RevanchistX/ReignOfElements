using System;
using UnityEngine;

namespace StateMachineEngine.PlayerStates
{
    public abstract class BaseState : IState
    {
        protected readonly Animator Animator;
        protected readonly Action Callback;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");

        protected const float CrossFadeDuration = 0.1f;


        protected BaseState(Animator animator, Action callback)
        {
            Animator = animator;
            Callback = callback;
        }

        public virtual void OnEnter()
        {
            // noop
        }

        public virtual void Update()
        {
            // noop
        }

        public virtual void FixedUpdate()
        {
            // noop
        }

        public virtual void OnExit()
        {
            // noop
        }
    }
}