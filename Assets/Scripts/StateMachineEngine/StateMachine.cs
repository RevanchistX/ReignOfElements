using System;
using System.Collections.Generic;
using System.Linq;

namespace StateMachineEngine
{
    public class StateMachine
    {
        private StateNode currentNode;
        private readonly Dictionary<Type, StateNode> nodes = new();
        private readonly HashSet<ITransition> anyTransitions = new();
        public IState CurrentState => currentNode.State;

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            currentNode.State?.Update();
        }

        public void FixedUpdate()
        {
            currentNode.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            currentNode = nodes[state.GetType()];
            currentNode.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (state == currentNode.State) return;

            var previousState = currentNode.State;
            var nextState = nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            currentNode = nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in anyTransitions.Where(transition => transition.Condition.Evaluate()))
                return transition;

            return currentNode.Transitions.FirstOrDefault(transition => transition.Condition.Evaluate());
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());
            if (node != null) return node;
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
            return node;
        }
    }
}