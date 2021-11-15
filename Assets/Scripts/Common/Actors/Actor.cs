using System;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public abstract class Actor : SerializedMonoBehaviour, IActor
    {
        [Inject] private ITeamObjectRegisterer<IActor> _actorRegisterer;
        
        public IPlayer Owner => _owner;
        public event EventHandler Killed;
        
        public IReadOnlyComponentContainer Components => _components;
        
        protected ActorComponentContainer _components;
        protected IDebugLogger _logger;
        
        private IPlayer _owner;

        private void Awake()
        {
            _components = GetComponent<ActorComponentContainer>();
        }

        protected void Init(IPlayer owner, IDebugLogger logger)
        {
            _actorRegisterer.Register(owner.Team, this); 
            
            _owner = owner;
            _logger = logger;
            
            _components.Init(this, logger);
            _components.StoreSerializableComponents();
        }
        
        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }

        protected virtual void Kill()
        {
            _actorRegisterer.Unregister(this); 
            Killed?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnValidate()
        {
            if (this.HasComponentsOfType(this))
            {
                Debug.LogError($"GameObject {gameObject.name} already has an Actor component! Destroying {GetType()}");
                StartCoroutine(this.DestroyComponentInEditor());
            }
        }
    }
}