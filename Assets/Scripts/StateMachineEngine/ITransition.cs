namespace StateMachineEngine
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}