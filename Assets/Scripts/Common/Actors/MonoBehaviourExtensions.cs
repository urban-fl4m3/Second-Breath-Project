using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Actors
{
    public static class MonoBehaviourExtensions
    {
        public static IEnumerator DestroyComponentInEditor(this MonoBehaviour obj)
        {
            yield return null;
            Object.DestroyImmediate(obj);
        }

        public static bool HasComponentsOfType<T>(this MonoBehaviour obj, T exclude = default)
        {
            var components = new List<T>();
            obj.GetComponents(components);

            if (exclude != null && components.Contains(exclude))
            {
                components.Remove(exclude);
            }
            
            return components.Count > 0;
        }
    }
}