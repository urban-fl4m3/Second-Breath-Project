using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class RegistrationMap
    {
        public IEnumerable<GameObject> RegisteredObjects => _registeredObjects;
        
        private readonly List<GameObject> _registeredObjects = new List<GameObject>();
        
        public RegistrationMap()
        {
            
        }

        public void RegisterObject(GameObject gameObject)
        {
            _registeredObjects.Add(gameObject);
        }
    }
}