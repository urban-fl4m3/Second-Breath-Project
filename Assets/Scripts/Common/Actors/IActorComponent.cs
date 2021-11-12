using System;

namespace Common.Actors
{
    public interface IActorComponent : IDisposable
    {
        void Enable();
        void Disable();
    }
}