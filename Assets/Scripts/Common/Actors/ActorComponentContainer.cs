using System;
using System.Collections.Generic;
using SecondBreath.Common.Logger;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Common.Actors
{
    public class ActorComponentContainer : SerializedMonoBehaviour, IComponentContainer
    {
        public IActor Owner => _owner;
        
        [OdinSerialize] private List<IActorComponent> _serializableComponents;

        private readonly Dictionary<Type, object> _components = new Dictionary<Type, object>();
        private IDebugLogger _logger;
        private IActor _owner;

        public void Init(IActor owner, IDebugLogger logger)
        {
            _owner = owner;
            _logger = logger;
        }

        public void StoreSerializableComponents()
        {
            foreach (var component in _serializableComponents)
            {
                var t = component.GetType();
             
                if (!_components.ContainsKey(t))
                {
                    _components.Add(t, component);
                }
            }
        }

        public T Get<T>()
        {
            var type = typeof(T);
            var hasComponent = _components.TryGetValue(type, out var component);

            if (hasComponent)
            {
                return (T)component;
            }

            return FindComponentOfType<T>();
        }

        public T Create<T>(params Type[] storeAsTypes) where T : IActorComponent
        {
            var type = typeof(T);
            var hasComponent = _components.TryGetValue(type, out var component);

            if (!hasComponent)
            {
                component = GetComponent<T>();

                if (component != null)
                {
                    _components.Add(type, component);
                }
                else
                {
                    _logger.LogError($"The component with type {type} for {gameObject.name} is null!");
                    return default;
                }
            }
            
            var obj = (T)component;
            AddAdditionalComponents(storeAsTypes, obj);
            return obj;
        }

        public void Add<T>(T component) where T : IActorComponent
        {
            var type = typeof(T);

            if (_components.ContainsKey(type))
            {
                return;
            }
            
            _components.Add(type, component);
        }
        
        private void AddAdditionalComponents<T>(IEnumerable<Type> types, T component)
        {
            foreach (var type in types)
            {
                if (!_components.ContainsKey(type))
                {
                    _components.Add(type, component);
                }
            }
        }
        
        private T FindComponentOfType<T>()
        {
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                {
                    return typedComponent;
                }
            }

            _logger.LogError($"Component of type {typeof(T)} wasn't found for {gameObject.name}");
            return default;
        }
    }
}