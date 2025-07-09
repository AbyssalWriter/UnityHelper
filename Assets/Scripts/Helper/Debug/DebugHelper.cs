using System.Collections.Generic;
using Helper.Shared;
using UnityEngine;

namespace Helper.Debug
{
    public class DebugHelper: Singleton<DebugHelper>
    {
        private readonly Dictionary<string, object> _logs = new();

        public void Log(string key, object value)
        {
            if (_logs.TryAdd(key, value)) return;
            _logs[key] = value;
        }

        private void OnGUI()
        {
            var rect = new Rect(10, 0, 400, 20);
            GUILayout.BeginArea(new Rect(0, 0, 400, Screen.height));
        
            foreach (var keyValuePair in _logs)
            {
                rect.y += rect.height + 10;
                GUI.Label(rect, keyValuePair.Key +" "+ keyValuePair.Value);
            }
            GUILayout.EndArea();
        }
    }
}