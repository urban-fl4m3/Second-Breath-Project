using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using UniRx;
using UnityEngine;

namespace SecondBreath.Game.Battle.Searchers
{
    public class ActorSearcherUpdate : ITickUpdate, IDisposable
    {
        private readonly ITeamObjectRegisterer<IActor> _actorsRegisterer;
        private readonly IReactiveProperty<IActor> _target;
        private readonly ITranslatable _ownerTranslatable;
        private readonly Team _team;

        private readonly HashSet<IActor> _enemies;

        public ActorSearcherUpdate(ITeamObjectRegisterer<IActor> actorsRegisterer,
            Team team, IReactiveProperty<IActor> target, ITranslatable ownerTranslatable)
        {
            _actorsRegisterer = actorsRegisterer;
            _target = target;
            _ownerTranslatable = ownerTranslatable;
            _team = team;

            _enemies = new HashSet<IActor>(_actorsRegisterer.GetOppositeTeamObjects(_team));

            _actorsRegisterer.ObjectRegistered += HandleActorRegistered;
            _actorsRegisterer.ObjectUnregistered += HandleActorUnregistered;
        }

        public void Update()
        {
            _target.Value ??= GetNearestEnemyActor();
             
            Debug.Log("A");
        }

        public void Dispose()
        {
            _actorsRegisterer.ObjectRegistered -= HandleActorRegistered;
            _actorsRegisterer.ObjectUnregistered -= HandleActorUnregistered;
        }

        private IActor GetNearestEnemyActor()
        {
            IActor nearestActor = null;
            var lowestDistance = float.MaxValue;

            foreach (var enemy in _enemies)
            {
                var enemyTranslatable = enemy.Components.Get<ITranslatable>();

                var dist = Vector3.SqrMagnitude(enemyTranslatable.Position - _ownerTranslatable.Position);
                
                if (dist < lowestDistance)
                {
                    lowestDistance = dist;
                    nearestActor = enemy;
                }
            }

            return nearestActor;
        }
        
        private void HandleActorRegistered(object sender, RegistrationTeamObjectArgs<IActor> e)
        {
            if (e.Team != _team)
            {
                _enemies.Add(e.Obj);
            }
        }

        private void HandleActorUnregistered(object sender, RegistrationTeamObjectArgs<IActor> e)
        {
            if (e.Team != _team)
            {
                _enemies.Remove(e.Obj);
            }
        }
    }
}