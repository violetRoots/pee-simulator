using NaughtyAttributes;
using System;
using System.Linq;
using UnityEngine;

public abstract class ScriptableDatabase<T> : ScriptableObject
{
    [Serializable]
    public class KeyValueDatabase
    {
        public T Value;
        [ReadOnly]
        public string Key;
    }

    public KeyValueDatabase[] objects;

    private void OnValidate()
    {
        for (int i = objects.Length - 1; i >= 0; i--)
        {
            KeyValueDatabase obj = objects[i];
            if (obj.Key == string.Empty || objects.Any(o => o != obj && o.Key == obj.Key))
                obj.Key = Guid.NewGuid().ToString();
        }
    }
}
