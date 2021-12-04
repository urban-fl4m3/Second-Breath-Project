using System;
using SecondBreath.Game.Players;

namespace Common.Actors
{
    public interface IActor
    {
        event EventHandler Killed;
     
        IPlayer Owner { get; }
        IReadOnlyComponentContainer Components { get; }
        
        void Enable();
        void Disable();
    }
}