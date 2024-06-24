namespace Movement.State
{
    public class MovementState
    {
        public float HorizontalSpeed => 10;
        public float VerticalSpeed => 250;
        public virtual float HorizontalSpeedModifier => 1;

        public virtual bool IsJumping()
        {
            return false;
        }
    }
}