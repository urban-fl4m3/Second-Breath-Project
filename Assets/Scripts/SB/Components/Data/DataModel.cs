﻿using System;
using System.Collections.Generic;
using SB.Helpers;
using UniRx;

namespace SB.Components.Data
{
    public class DataModel
    {
        private readonly Dictionary<Type, Dictionary<Attributes, object>> _dataContainers 
            = new Dictionary<Type, Dictionary<Attributes, object>>();
        
        public void AddProperty<T>(Attributes key, T value)
        {
            _dataContainers.GetOrCreate(typeof(T)).GetOrCreate(key, new ReactiveProperty<T>(value));
        }

        public ReactiveProperty<T> GetOrCreateProperty<T>(Attributes key)
        {
            return (ReactiveProperty<T>)_dataContainers.GetOrCreate(typeof(T)).GetOrCreate(key);
        }
        
        public ReactiveProperty<T> GetProperty<T>(Attributes key)
        {
            return (ReactiveProperty<T>)_dataContainers[typeof(T)][key];
        }

        public bool ContainsValue<T>(Attributes key)
        {
            return _dataContainers.ContainsKey(typeof(T)) && _dataContainers[typeof(T)].ContainsKey(key);
        }
    }
}