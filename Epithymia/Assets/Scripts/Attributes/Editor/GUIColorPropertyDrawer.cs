using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(GUIColorAttribute))]
    public class GUIColorPropertyDrawer : PropertyDrawer
    {
        private GUIColorAttribute Attribute => (GUIColorAttribute)attribute;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Color prevColor = GUI.backgroundColor;
            GUI.backgroundColor = Attribute.Color;
            EditorGUI.LabelField(position, label, EditorStyles.objectFieldThumb);
            position.x += EditorGUIUtility.labelWidth;
            position.width -= EditorGUIUtility.labelWidth;
            
            EditorGUI.PropertyField(position, property, GUIContent.none);
            GUI.backgroundColor = prevColor;
        }
    }
}
