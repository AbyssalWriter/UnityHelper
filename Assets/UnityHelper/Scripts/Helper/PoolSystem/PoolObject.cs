using UnityEngine;
using UnityEngine.Pool;

namespace Helper.PoolSystem
{
    public class PoolObject : MonoBehaviour
    {
        private IObjectPool<GameObject> _pool; 
    
        public GameObject CreatePooledItem()
        {
            var poolManager = PoolManager.Instance;
            var generated = Instantiate(gameObject, poolManager.gameObject.transform);
            generated.GetComponent<PoolObject>()._pool = poolManager.GetPool(this);
        
            return generated;
        }

        public IObjectPool<GameObject> GetPool()
        {
            return _pool;
        }
    }
}