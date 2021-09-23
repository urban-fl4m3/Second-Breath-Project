using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) 
        where TValue : new()
    {
        if (!dict.TryGetValue(key, out var val))
        {
            val = new TValue();
            dict.Add(key, val);
        }

        return val;
    }
    
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) 
        where TValue : new()
    {
        if (!dict.TryGetValue(key, out var val))
        {
            val = value;
            dict.Add(key, val);
        }

        return val;
    }
}
