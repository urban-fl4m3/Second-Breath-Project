using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.UI
{
    public class HealthBarViewContainer : MonoBehaviour, IView
    {
        [Inject] private ITeamObjectRegisterer<IActor> _actorRegisterer;

        [SerializeField] private HealthBarView _healthBarPrefab;
        
        private readonly Dictionary<IActor, HealthBarView> _healthBars = new Dictionary<IActor, HealthBarView>();
        
        private HashSet<IActor> _actors;

        private void Start()
        {
            _actors = new HashSet<IActor>(_actorRegisterer.GetRegisteredObjects());
            
            _actorRegisterer.ObjectRegistered += HandleActorRegistered;
            _actorRegisterer.ObjectUnregistered += HandleActorUnregistered;
        }

        private void HandleActorRegistered(object sender, RegistrationTeamObjectArgs e)
        {
            var actor = e.Obj;
            
            if (!_actors.Contains(actor))
            {
                _actors.Add(actor);

                var healthBar = Instantiate(_healthBarPrefab, transform);
                healthBar.TrackActor(actor);
                
                _healthBars.Add(actor, healthBar);
            }
        }
        
        private void HandleActorUnregistered(object sender, RegistrationTeamObjectArgs e)
        {
            var actor = e.Obj;
            
            if (_actors.Contains(actor))
            {
                _actors.Remove(actor);

                var actorHealthBar = _healthBars[actor];
                actorHealthBar.Dispose();

                _healthBars.Remove(actor);
                
                Destroy(actorHealthBar.gameObject);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}