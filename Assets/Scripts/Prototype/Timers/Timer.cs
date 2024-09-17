using System;

namespace Prototype.Timers
{
    public abstract class Timer
    {
        protected float InitialTime;
        protected float Time { get; set; }
        public bool IsRunning { get; private set; }

        public float Progress => Time / InitialTime;

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float initialTime)
        {
            InitialTime = initialTime;
            IsRunning = false;
        }

        public void Start()
        {
            Time = InitialTime;
            if (IsRunning) return;
            IsRunning = true;
            OnTimerStart.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;
            OnTimerStop.Invoke();
        }

        public float GetTime() => Time;
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        public void Reset() => Time = InitialTime;
        
        public abstract void Tick(float deltaTime);
    }
}