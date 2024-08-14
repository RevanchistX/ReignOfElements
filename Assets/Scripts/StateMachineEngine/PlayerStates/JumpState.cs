using System;
using UnityEngine;

namespace StateMachineEngine.PlayerStates
{
    public class JumpState : BaseState
    {
        public JumpState(Animator animator, Action callback) : base(animator, callback)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(JumpHash, CrossFadeDuration);
            Callback();
        }
    }
}