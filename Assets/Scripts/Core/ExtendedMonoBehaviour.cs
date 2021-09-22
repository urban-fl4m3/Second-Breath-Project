using UnityEngine;
using Zenject;

namespace Core
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private RegistrationMap _registrationMap;

        protected T InstantiatePrefab<T>(T original, Vector3 position, Quaternion rotation) where T : Component
        {
            var prefab = _diContainer.InstantiatePrefab(original);
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            
            _registrationMap.RegisterObject(prefab);

            return prefab.GetComponent<T>();
        }
    }
}