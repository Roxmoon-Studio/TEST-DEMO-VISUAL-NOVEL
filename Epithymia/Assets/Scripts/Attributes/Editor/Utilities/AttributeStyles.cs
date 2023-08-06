using UnityEditor;
using UnityEngine;

namespace Editor.Utilities
{
    public static class AttributeStyles
    {
        public static GUIStyle HelpBox { get; }

        static AttributeStyles()
        {
            HelpBox = new(EditorStyles.helpBox);
        }
    }
}
