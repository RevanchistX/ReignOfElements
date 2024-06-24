namespace Movement.State
{
    public class RunningState : MovementState
    {
        public override float HorizontalSpeedModifier => 2;
    }
}