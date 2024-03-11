using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public interface IPoolable
{
    public void OnSpawned();
    public void OnDespawned();
}

public static class PoolManager
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Application.quitting += () => { OnExiting?.Invoke(); };
        SceneManager.sceneUnloaded += _ => { OnSceneUnloaded?.Invoke(); };
    }

    private static event Action OnExiting;
    private static event Action OnSceneUnloaded;

    /// <summary>
    ///     오브젝트를 풀에서 가져옵니다.
    /// </summary>
    /// <param name="prefab">프리팹</param>
    /// <param name="parent">부모</param>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    /// <returns>풀링된 오브젝트</returns>
    public static T Get<T>(T prefab, Transform parent = null) where T : Object
    {
        var pool = Pool<T>.Instance;
        var obj = pool.Get(prefab);

        switch (obj)
        {
            case Component component:
                component.transform.SetParent(parent, false);
                break;
            case GameObject gameObject:
                gameObject.transform.SetParent(parent, false);
                break;
        }

        return obj;
    }

    /// <summary>
    ///     오브젝트를 풀에서 가져옵니다.
    /// </summary>
    /// <param name="prefab">프리팹</param>
    /// <param name="position">위치</param>
    /// <param name="rotation">회전</param>
    /// <param name="parent">부모</param>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    /// <returns>풀링된 오브젝트</returns>
    public static T Get<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Object
    {
        var pool = Pool<T>.Instance;
        var obj = pool.Get(prefab);

        switch (obj)
        {
            case Component component:
                component.transform.SetParent(parent, false);
                component.transform.position = position;
                component.transform.rotation = rotation;
                break;
            case GameObject gameObject:
                gameObject.transform.SetParent(parent, false);
                gameObject.transform.position = position;
                gameObject.transform.rotation = rotation;
                break;
        }

        return obj;
    }

    /// <summary>
    ///     오브젝트를 풀에 반환합니다.
    /// </summary>
    /// <param name="obj">반환할 오브젝트</param>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    public static void Release<T>(T obj) where T : Object
    {
        var pool = Pool<T>.Instance;
        pool.Release(obj);
    }

    /// <summary>
    ///     오브젝트를 풀에 미리 생성합니다.
    /// </summary>
    /// <param name="prefab">프리팹</param>
    /// <param name="count">생성할 개수</param>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    public static void Preload<T>(T prefab, int count) where T : Object
    {
        var pool = Pool<T>.Instance;
        for (var i = 0; i < count; i++)
        {
            var obj = pool.Get(prefab);
            pool.Release(obj);
        }
    }

    /// <summary>
    ///     풀을 비웁니다.
    /// </summary>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    /// <param name="prefab">프리팹</param>
    public static void Clear<T>(T prefab) where T : Object
    {
        var pool = Pool<T>.Instance;
        pool.Clear();
    }

    private class Pool<T> where T : Object
    {
        private readonly Dictionary<T, T> _prefabs = new();
        private readonly Dictionary<T, Stack<T>> _stacks = new();
        private readonly Dictionary<object, List<IPoolable>> _poolables = new();

        static Pool()
        {
            OnExiting += Instance.Clear;
            OnSceneUnloaded += Instance.Clear;
            Debug.Log("PoolManager Initialized - " + typeof(T));
        }

        public static Pool<T> Instance { get; } = new();

        private GameObject GetGameObject(T prefab)
        {
            return prefab switch
            {
                GameObject @object => @object,
                Component component => component.gameObject,
                _ => null
            };
        }

        public T Get(T prefab)
        {
            if (!_stacks.TryGetValue(prefab, out var stack))
            {
                stack = new Stack<T>();
                _stacks.Add(prefab, stack);
            }

            T obj;

            if (stack.Count > 0)
            {
                obj = stack.Pop();
                GetGameObject(obj).SetActive(true);
            }
            else
            {
                obj = Object.Instantiate(prefab);
                _prefabs.Add(obj, prefab);

                var gameObject = GetGameObject(obj);

                if (gameObject != null && !_poolables.ContainsKey(gameObject))
                {
                    _poolables.Add(obj, new List<IPoolable>());
                }

                if (gameObject == null) return obj;

                foreach (var poolable in gameObject.GetComponents<IPoolable>())
                {
                    _poolables[obj].Add(poolable);
                }
            }

            foreach (var poolable in _poolables[obj])
            {
                poolable.OnSpawned();
            }

            return obj;
        }

        public void Release(T obj)
        {
            var gameObject = GetGameObject(obj);

            if (!_prefabs.TryGetValue(obj, out var prefab))
            {
                Object.Destroy(gameObject);
                return;
            }

            if (_poolables.TryGetValue(obj, out var poolables))
            {
                foreach (var poolable in poolables)
                {
                    poolable.OnDespawned();
                }
            }

            if (gameObject != null) gameObject.SetActive(false);
            _stacks[prefab].Push(obj);
        }

        public void Clear()
        {
            foreach (var obj in _stacks.Values.SelectMany(stack => stack))
            {
                Object.Destroy(GetGameObject(obj));
            }

            _stacks.Clear();
            _prefabs.Clear();
            _poolables.Clear();
        }
    }
}