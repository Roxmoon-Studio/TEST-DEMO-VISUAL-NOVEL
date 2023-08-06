using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Utilities
{
    public class SearchWindow : EditorWindow
    {
        private IEnumerable<Type> _types;
        private Action<Type> _callback;
        private GUIStyle _style;
        private string _filter;
        private Vector2 _scrollPosition;

        public static SearchWindow Open(Vector2 position, Type type, Action<Type> callback)
        {
            var window = CreateInstance<SearchWindow>();
            window.position = new Rect(position, new Vector2(250f, 300f));
            window._types = GetTypes(type);
            window._callback = callback;
            window._style = new GUIStyle(EditorStyles.toolbarButton) { alignment = TextAnchor.MiddleLeft };
            window.ShowPopup();
            window.Focus();

            return window;
        }

        private static IEnumerable<Type> GetTypes(Type type)
        {
            List<Type> result = new();
            var domain = AppDomain.CurrentDomain;
            var assemblies = domain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var t in types)
                    if (type.IsAssignableFrom(t) && !t.IsAbstract)
                        result.Add(t);
            }

            return result;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            _filter = EditorGUILayout.TextField(_filter, EditorStyles.toolbarSearchField);
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true));
            DrawButtons();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void OnLostFocus() => 
            Close();

        private void DrawButtons()
        {
            foreach (var type in _types)
            {
                if (!string.IsNullOrEmpty(_filter) && !type.Name.ToLower().Contains(_filter))
                    continue;

                if (GUILayout.Button(type.Name, _style, GUILayout.ExpandWidth(true)))
                {
                    _callback?.Invoke(type);
                    Close();
                }
            }
        }
    }
}
