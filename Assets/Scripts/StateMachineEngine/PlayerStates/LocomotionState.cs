using System;
using InputEngine;
using UnityEngine;

namespace StateMachineEngine.PlayerStates
{
    public class LocomotionState : BaseState
    {
        private readonly InputReader inputReader;
        private readonly Rigidbody playerRigidBody;

        public LocomotionState(Animator animator, Action callback) : base(animator, callback)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(LocomotionHash, CrossFadeDuration);
        }

        public override void FixedUpdate()
        {
            Callback();
        }
    }
}