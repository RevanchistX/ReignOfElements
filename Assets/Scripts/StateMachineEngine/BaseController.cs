using System.Collections.Generic;
using UnityEngine;

namespace StateMachineEngine
{
    public class BaseController : MonoBehaviour, IController
    {
        protected StateMachine StateMachine;
        protected List<Timer.Timer> Timers;

        private void Awake()
        {
            SetupReferences();
            SetupStateMachine();
            SetupTimers();
        }

        public virtual void SetupStateMachine()
        {
            //noop
        }

        public virtual void SetupReferences()
        {
            //noop
        }

        public virtual void SetupTimers()
        {
            //noop
        }


        private void Update()
        {
            StateMachine.Update();
            HandleTimers();
        }

        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }

        private void HandleTimers()
        {
            foreach (var timer in Timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
    }
}