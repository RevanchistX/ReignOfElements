using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachineEngine
{
    public class StateMachine
    {
        public StateNode CurrentNode { get; private set; }
        private readonly Dictionary<Type, StateNode> nodes = new();
        private readonly HashSet<ITransition> anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            CurrentNode.State?.Update();
        }

        public void FixedUpdate()
        {
            CurrentNode.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            CurrentNode = nodes[state.GetType()];
            CurrentNode.State?.OnEnter();
        }

        public void ChangeState(IState state)
        {
            if (state == CurrentNode.State) return;

            var previousState = CurrentNode.State;
            var nextState = nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            CurrentNode = nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in anyTransitions.Where(transition => transition.Condition.Evaluate()))
                return transition;

            return CurrentNode.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        public StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if (node != null) return node;
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
            return node;
        }
    }
}