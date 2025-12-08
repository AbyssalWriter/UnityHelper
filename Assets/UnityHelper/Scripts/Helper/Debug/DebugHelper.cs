using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helper.Debug
{
    public class DebugHelper : Shared.Singleton<DebugHelper>
    {
        [SerializeField] private Rect rectBox = new(10, 0, 750, 20);
        [SerializeField] private float logOffset = 10;
        [SerializeField] private float clearLogsTimerInSeconds = 5f;
        [SerializeField] private int fontSize = 20;

        private GUIStyle labelStyle;

        private Dictionary<string, object> _logs = new();
        private float _nextClease;



        public void Log(string key, object value)
        {
            if (_logs.TryAdd(key, value)) return;

            _logs[key] = value;
        }

        private void LateUpdate()
        {
            _nextClease -= Time.deltaTime;

            if (_nextClease > 0) return;
            _nextClease = clearLogsTimerInSeconds;
            _logs.Clear();
        }

        private void OnGUI()
        {
            labelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = fontSize
            };

            var clone = _logs.ToDictionary(entry => entry.Key,
                entry => entry.Value);

            var drawingRect = new Rect(rectBox);
            GUILayout.BeginArea(new Rect(0, 0, rectBox.width, Screen.height));

            foreach (var keyValuePair in clone)
            {
                drawingRect.y += drawingRect.height + logOffset;
                GUI.Label(drawingRect, keyValuePair.Key + " " + keyValuePair.Value, labelStyle);

            }
            GUILayout.EndArea();
        }
    }
}