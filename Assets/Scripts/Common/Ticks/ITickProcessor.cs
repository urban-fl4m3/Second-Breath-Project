using System;

namespace SecondBreath.Common.Ticks
{
    public interface ITickProcessor
    {
        void Start();
        void Stop();
        
        void AddTick(Action tick);
        void RemoveTick(Action tick);
    }
}