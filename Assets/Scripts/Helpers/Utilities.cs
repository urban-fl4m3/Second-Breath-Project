using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();

        if (component == null)
            component = gameObject.AddComponent<T>();

        return component;

    }
    
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) 
        where TValue : new()
    {
        if (!dict.TryGetValue(key, out TValue val))
        {
            val = new TValue();
            dict.Add(key, val);
        }

        return val;
    }
}
