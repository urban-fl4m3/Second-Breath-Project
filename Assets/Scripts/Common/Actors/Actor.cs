using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Actors
{
    public class Actor : SerializedMonoBehaviour, IActor
    {
        private void OnValidate()
        {
            if (this.HasComponentsOfType(this))
            {
                Debug.LogError($"GameObject {gameObject.name} already has an Actor component! Destroying {GetType()}");
                StartCoroutine(this.DestroyComponentInEditor());
            }
        }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
    }
}