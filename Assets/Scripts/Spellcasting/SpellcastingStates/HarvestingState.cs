using System;
using StateMachineEngine.PlayerStates;
using UnityEngine;

namespace Spellcasting.SpellcastingStates
{
    public class HarvestingState : BaseState
    {
        public HarvestingState(Animator animator, Action callback) : base(animator, callback)
        {
        }
    }
}