using System;

namespace SecondBreath.Game.Players
{
    public interface IPlayer : IEquatable<IPlayer>
    {
        Guid Guid { get; }
        Team Team { get; }
    }
}