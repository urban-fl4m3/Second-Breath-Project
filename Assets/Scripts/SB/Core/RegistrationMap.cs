using System.Collections.Generic;
using UnityEngine;

namespace SB.Core
{
    public class RegistrationMap
    {
        public IEnumerable<GameObject> RegisteredObjects => _registeredObjects;
        
        private readonly List<GameObject> _registeredObjects = new List<GameObject>();

        public void RegisterObject(GameObject gameObject)
        {
            _registeredObjects.Add(gameObject);
        }

        public void RegisterObject<T>(T component) where T : Component
        {
            _registeredObjects.Add(component.gameObject);
        }

        public void UnregisterObject(GameObject gameObject)
        {
            _registeredObjects.Remove(gameObject);
        }
        
        public void UnregisterObject<T>(T component) where T : Component
        {
            _registeredObjects.Remove(component.gameObject);
        }
    }
}