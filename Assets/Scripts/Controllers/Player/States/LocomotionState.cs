using UnityEngine;

namespace Controllers.Player.States
{
    public class LocomotionState : BaseState
    {
        public LocomotionState(Controller player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(LocomotionHash, CrossFadeDuration);
        }

        public override void FixedUpdate()
        {
            Player.HandleMovement();
        }
    }
}