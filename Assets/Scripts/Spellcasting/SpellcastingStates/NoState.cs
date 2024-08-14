using System;
using StateMachineEngine.PlayerStates;
using UnityEngine;

namespace Spellcasting.SpellcastingStates
{
    public class NoState : BaseState
    {
        public NoState(Animator animator, Action callback) : base(animator, callback)
        {
        }
    }
}