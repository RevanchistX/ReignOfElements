using StateMachineEngine;
using UnityEngine;

namespace Controllers.Player.States
{
    public abstract class BaseState : IState
    {
        protected readonly Controller Player;
        protected readonly Animator Animator;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected const float CrossFadeDuration = 0.1f;

        protected BaseState(Controller player, Animator animator)
        {
            Player = player;
            Animator = animator;
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