using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private int numObjects;
    [SerializeField] private T prefab;

    private HashSet<T> _pool;

    public event Action<T> OnObjectFound;

    private IEnumerator Start()
    {
        _pool = new HashSet<T>();
        for (int i = 0; i < numObjects; ++i)
        {
            var instance = Instantiate(prefab, transform);
            instance.gameObject.SetActive(false);
            _pool.Add(instance);

            if (i % 5 == 0)
                yield return null;
        }
    }

    private void OnDestroy()
    {
        _pool.Clear();
    }

    public T GetObject()
    {
        T foundObject = null;

        if (_pool.Any(obj => !obj.gameObject.activeSelf))
            foundObject = _pool.First(obj => !obj.gameObject.activeSelf);

        if (foundObject == null)
            Debug.LogWarning("No more objects in pool!");
        else
            OnObjectFound?.Invoke(foundObject);

        return foundObject;
    }
}