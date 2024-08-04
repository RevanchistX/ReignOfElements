using System;

namespace StateMachineEngine
{
    public class FunctionPredicate : IPredicate
    {
        private readonly Func<bool> function;

        public FunctionPredicate(Func<bool> function)
        {
            this.function = function;
        }

        public bool Evaluate() => function.Invoke();
    }
}