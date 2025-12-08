using UnityEngine;

namespace Helper.Shared
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T _instance;
        public static T Instance {
            get { return _instance; }
        }

        protected virtual void Awake() {
            if (_instance != null) {
                Destroy(gameObject);
            }
            else {
                _instance = GetComponent<T>();
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}