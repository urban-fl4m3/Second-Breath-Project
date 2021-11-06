using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Actors
{
    public class ActorComponent : SerializedMonoBehaviour, IActorComponent 
    {
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