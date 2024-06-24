namespace Movement.State
{
    public class JumpingState : MovementState
    {
        public override float HorizontalSpeedModifier => 0.5f;

        public override bool IsJumping()
        {
            return true;
        }
    }
}