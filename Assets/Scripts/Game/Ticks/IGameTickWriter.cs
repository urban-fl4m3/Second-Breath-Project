using System;

namespace SecondBreath.Game.Ticks
{
    public interface IGameTickWriter
    {
        void AddTick(Action tick);
        void RemoveTick(Action tick);
    }
}