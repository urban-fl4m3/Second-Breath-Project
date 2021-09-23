using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [Inject] protected DiContainer _diContainer;
        
        protected GameObject InstantiatePrefab(GameObject original)
        {
            var prefab = _diContainer.InstantiatePrefab(original);
            prefab.transform.position = Vector3.zero;
            prefab.transform.rotation = quaternion.identity;

            return prefab;
        }
        
        protected T InstantiatePrefab<T>(T original) where T : Component
        {
            var prefab = _diContainer.InstantiatePrefab(original);
            prefab.transform.position = Vector3.zero;
            prefab.transform.rotation = quaternion.identity;

            return prefab.GetComponent<T>();
        }
        
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