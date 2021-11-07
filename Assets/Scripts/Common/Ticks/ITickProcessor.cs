namespace SecondBreath.Common.Ticks
{
    public interface ITickProcessor
    {
        void Start();
        void Stop();
        
        void AddTick(ITickUpdate tick);
        void RemoveTick(ITickUpdate tick);
    }
}