using System;

namespace Common.Actors
{
    public interface IComponentContainer : IReadOnlyComponentContainer
    {
        T Create<T>(params Type[] storeAsTypes);
        void Add<T>(T component);
    }
}