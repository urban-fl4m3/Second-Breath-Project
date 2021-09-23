using System;
using System.Collections.Generic;
using UniRx;

namespace Components.Data
{
    public class DataModel
    {
        private readonly Dictionary<Type, Dictionary<Attributes, object>> _dataContainers 
            = new Dictionary<Type, Dictionary<Attributes, object>>();
        
        public void AddProperty<T>(Attributes key, T value)
        {
            _dataContainers.GetOrCreate(typeof(T)).Add(key, new ReactiveProperty<T>(value));
        }

        public ReactiveProperty<T> GetProperty<T>(Attributes key)
        {
            return (ReactiveProperty<T>)_dataContainers[typeof(T)][key];
        }
    }
}