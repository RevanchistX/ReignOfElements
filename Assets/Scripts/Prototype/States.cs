using AdvancedController;
using StateMachineEngine;

namespace Prototype {
    public class GroundedState : IState {
        readonly PlayerController controller;

        public GroundedState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            controller.OnGroundContactRegained();
        }

        public void Update()
        {
            // noop
        }

        public void FixedUpdate()
        {
            // noop
        }

        public void OnExit()
        {
            // noop
        }
    }

    public class FallingState : IState {
        readonly PlayerController controller;

        public FallingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            controller.OnFallStart();
        }

        public void Update()
        {
            // noop
        }

        public void FixedUpdate()
        {
            // noop
        }

        public void OnExit()
        {
            // noop
        }
    }

    public class SlidingState : IState {
        readonly PlayerController controller;

        public SlidingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            controller.OnGroundContactLost();
        }

        public void Update()
        {
            // noop
        }

        public void FixedUpdate()
        {
            // noop
        }

        public void OnExit()
        {
            // noop
        }
    }

    public class RisingState : IState {
        readonly PlayerController controller;

        public RisingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            controller.OnGroundContactLost();
        }

        public void Update()
        {
            // noop
        }

        public void FixedUpdate()
        {
            // noop
        }

        public void OnExit()
        {
            // noop
        }
    }

    public class JumpingState : IState {
        readonly PlayerController controller;

        public JumpingState(PlayerController controller) {
            this.controller = controller;
        }

        public void OnEnter() {
            controller.OnGroundContactLost();
            controller.OnJumpStart();
        }

        public void Update()
        {
            // noop
        }

        public void FixedUpdate()
        {
            // noop
        }

        public void OnExit()
        {
            // noop
        }
    }
}