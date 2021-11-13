using SecondBreath.Common.Logger;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Actors
{
    public abstract class ActorComponent : SerializedMonoBehaviour, IActorComponent
    {
        protected IDebugLogger _logger;
        
        protected void Init(IDebugLogger logger)
        {
            _logger = logger;
        }
        
        public virtual void Dispose()
        {
            
        }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
        
        private void OnValidate()
        {
            if (!this.HasComponentsOfType<IActor>())
            {
                Debug.LogError($"GameObject {gameObject.name} doesn't has an Actor component! Destroying {GetType()}");
                StartCoroutine(this.DestroyComponentInEditor());
            }
        }
    }
}