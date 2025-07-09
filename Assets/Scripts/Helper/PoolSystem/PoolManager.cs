using System;
using System.Collections.Generic;
using Helper.Shared;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Helper.PoolSystem
{
    public class PoolManager : Singleton<PoolManager>
    {
        public enum PoolType
        {
            Stack,
            LinkedList
        }

        [Serializable]
        public struct PoolData
        {
            public PoolObject prefab;
            public IObjectPool<GameObject> pool;
            public PoolType poolType;
        }
        
        private List<PoolData> _pools;
        
        public bool collectionChecks = true;
        public int maxPoolSize = 10;

        private void Start()
        {
            for (var i = 0; i < _pools.Count; i++)
            {
                var currentPool = _pools[i];
                currentPool.pool = _GeneratePool(_pools[i].prefab, _pools[i].poolType);
                _pools[i] = currentPool; 
            }
        }

        private IObjectPool<GameObject> _GeneratePool(PoolObject prefab, PoolType poolType = PoolType.Stack)
        {
            if (poolType == PoolType.Stack)
                return new ObjectPool<GameObject>(prefab.CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            
            return new LinkedPool<GameObject>(prefab.CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
        }

        public IObjectPool<GameObject> GetPool(PoolObject prefab)
        {
            foreach (var pool in _pools)
            {
                if (pool.prefab.name == prefab.name)
                {
                    return pool.pool;
                }
            }

            return null;
        }
        
        private void OnReturnedToPool(GameObject system)
        {
            system.gameObject.SetActive(false);
        }
        
        private void OnTakeFromPool(GameObject system)
        {
            system.gameObject.SetActive(true);
        }
        
        private void OnDestroyPoolObject(GameObject system)
        {
            Destroy(system.gameObject);
        }
    }
}