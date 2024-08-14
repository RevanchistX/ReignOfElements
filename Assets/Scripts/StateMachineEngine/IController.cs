namespace StateMachineEngine
{
    public interface IController
    {
        void SetupStateMachine();
        void SetupReferences();
        void SetupTimers();
    }
}