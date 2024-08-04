using UnityEngine;

namespace StateMachineEngine.PlayerStates
{
    public class DodgeState : BaseState
    {
        public DodgeState(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            Animator.CrossFade(DodgeHash, CrossFadeDuration);
        }

        public override void FixedUpdate()
        {
            Player.HandleDodge();
        }
    }
}