using System;
using System.Collections.Generic;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Registration
{
    public interface ITeamObjectRegisterer<T>
    {
        event EventHandler<RegistrationTeamObjectArgs<T>> ObjectRegistered;
        event EventHandler<RegistrationTeamObjectArgs<T>> ObjectUnregistered;
        
        void Register(Team team, T obj);
        void Unregister(T obj);
        IEnumerable<T> GetRegisteredObjects();
        IEnumerable<T> GetTeamObjects(Team team);
        IEnumerable<T> GetOppositeTeamObjects(Team team);
    }
}