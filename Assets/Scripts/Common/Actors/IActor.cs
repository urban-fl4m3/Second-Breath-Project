using System;

namespace Common.Actors
{
    public interface IActor
    {
        event EventHandler Killed;
        
        IReadOnlyComponentContainer Components { get; }
        
        void Enable();
        void Disable();
    }
}