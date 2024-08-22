namespace Utility.Timer
{
    public class CountdownTimer : Timer
    {
        public CountdownTimer(float value) : base(value)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (!IsRunning) return;

            if (Time > 0) Time -= deltaTime;
            if (Time <= 0) Stop();
        }

        public bool IsFinished => Time <= 0;

        public void Reset(float newTime)
        {
            InitialTime = newTime;
            Reset();
        }
    }
}