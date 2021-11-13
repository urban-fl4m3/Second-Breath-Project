using System;
using System.Collections.Generic;
using System.Linq;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Registration
{
    public class TeamObjectRegisterer<T> : ITeamObjectRegisterer<T>
    {
        public event EventHandler<RegistrationTeamObjectArgs<T>> ObjectRegistered;
        public event EventHandler<RegistrationTeamObjectArgs<T>> ObjectUnregistered;
        
        private readonly Dictionary<Team, List<T>> _registeredObjects = new Dictionary<Team, List<T>>();
        
        public void Register(Team team, T obj)
        {
            if (!_registeredObjects.ContainsKey(team))
            {
                _registeredObjects.Add(team, new List<T>());
            }

            var teamObjects = _registeredObjects[team];
            
            if (teamObjects.Contains(obj))
            {
                return;
            }
            
            teamObjects.Add(obj);
            ObjectRegistered?.Invoke(this, new RegistrationTeamObjectArgs<T>(obj, team));
        }

        public void Unregister(T obj)
        {
            foreach (var teamObjects in _registeredObjects)
            {
                teamObjects.Value.Remove(obj);
                ObjectUnregistered?.Invoke(this, new RegistrationTeamObjectArgs<T>(obj, teamObjects.Key));
            }
        }

        public IEnumerable<T> GetRegisteredObjects()
        {
            var registeredObjects = new List<T>();

            foreach (var o in _registeredObjects)
            {
                registeredObjects.AddRange(o.Value);
            }

            return registeredObjects;
        }
        
        public IEnumerable<T> GetTeamObjects(Team team)
        {
            return GetTeamObjects(t => t.Equals(team));
        }

        public IEnumerable<T> GetOppositeTeamObjects(Team team)
        {
            return GetTeamObjects(t => !t.Equals(team));
        }
        
        private IEnumerable<T> GetTeamObjects(Func<Team, bool> teamEqualityCheck)
        {
            return _registeredObjects
                .Where(kv => teamEqualityCheck(kv.Key) && kv.Value != null)
                .SelectMany(kv => kv.Value);
        }
    }
}