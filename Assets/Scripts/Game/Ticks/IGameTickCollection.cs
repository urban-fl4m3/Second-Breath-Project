using SecondBreath.Common.Ticks;

namespace SecondBreath.Game.Ticks
{
    public interface IGameTickCollection
    {
        void AddTick(ITickUpdate tick);
        void RemoveTick(ITickUpdate tick);
    }
}