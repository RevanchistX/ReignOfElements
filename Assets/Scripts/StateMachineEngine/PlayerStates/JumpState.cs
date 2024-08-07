using UnityEngine;

namespace StateMachineEngine.PlayerStates
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(JumpHash, CrossFadeDuration);
            Player.HandleJump();
        }
    }
}