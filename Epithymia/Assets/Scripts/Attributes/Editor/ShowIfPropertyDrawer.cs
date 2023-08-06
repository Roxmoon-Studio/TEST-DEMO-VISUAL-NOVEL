using Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        private ShowIfAttribute Attribute => attribute as ShowIfAttribute;

        private bool _result;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _result = GetResult(property);
            
            if (_result)
                return base.GetPropertyHeight(property, label);

            return 0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_result)
                EditorGUI.PropertyField(position, property, label);
        }

        private bool GetResult(SerializedProperty property)
        {
            if (!AttributeUtility.TryGetMethodResultAs<bool>(property, Attribute.Condition, out var result))
                return false;

            return result;
        }
    }
}
