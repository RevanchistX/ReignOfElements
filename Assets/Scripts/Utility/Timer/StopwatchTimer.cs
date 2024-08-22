namespace Utility.Timer
{
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer() : base(0)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (!IsRunning) return;
            Time += deltaTime;
        }
    }
}