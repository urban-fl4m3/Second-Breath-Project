using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Animations
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private readonly Dictionary<string, EventHandler> _eventsDictionary
            = new Dictionary<string, EventHandler>();
        
        /// <summary>
        /// Executed from unity animator with given key!
        /// </summary>
        /// <param name="key"></param>
        public void RaiseEvent(string key)
        {
            var isExists = _eventsDictionary.TryGetValue(key, out var handler);

            if (isExists)
            {
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Subscribe(string key, EventHandler handler)
        {
            if (!_eventsDictionary.ContainsKey(key))
            {
                _eventsDictionary.Add(key, null);
            }

            _eventsDictionary[key] += handler;
        }

        public void Unsubscribe(string key)
        {
            if (_eventsDictionary.ContainsKey(key))
            {
                _eventsDictionary.Remove(key);
            }
        }
    }
}