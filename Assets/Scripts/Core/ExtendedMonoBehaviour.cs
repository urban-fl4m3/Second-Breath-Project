using UnityEngine;
using Zenject;

namespace Core
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [Inject] protected DiContainer _diContainer;

        protected T InstantiatePrefab<T>(T original, Vector3 position, Quaternion rotation) where T : Component
        {
            var prefab = _diContainer.InstantiatePrefab(original);
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;

            return prefab.GetComponent<T>();
        }
        
        protected GameObject InstantiatePrefab(GameObject original, Vector3 position, Quaternion rotation)
        {
            var prefab = _diContainer.InstantiatePrefab(original);
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;

            return prefab;
        }
    }
}