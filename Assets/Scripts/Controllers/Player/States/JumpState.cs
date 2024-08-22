using UnityEngine;

namespace Controllers.Player.States
{
    public class JumpState : BaseState
    {
        public JumpState(Controller player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(JumpHash, CrossFadeDuration);
        }

        public override void FixedUpdate()
        {
            Player.HandleJump();
            Player.HandleMovement();
        }
    }
}