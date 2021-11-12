using SecondBreath.Common.Logger;
using SecondBreath.Game.Players;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public abstract class Actor : SerializedMonoBehaviour, IActor
    {
        public IPlayer Owner => _owner;
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