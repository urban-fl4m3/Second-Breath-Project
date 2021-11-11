using System;

namespace Common.Actors
{
    public interface IComponentContainer : IReadOnlyComponentContainer
    {
        T Create<T>(params Type[] storeAsTypes) where T : IActorComponent;
        void Add<T>(T component) where T : IActorComponent;
    }
}