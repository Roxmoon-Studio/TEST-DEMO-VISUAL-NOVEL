using Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ValidateAttribute))]
    public class ValidatePropertyDrawer : PropertyDrawer
    {
        private const string ERROR_MESSAGE = "Wrong method's signature! Method must return bool and has not parameters";
        private const float MARGING = 2f;

        private ValidateAttribute Attribute => attribute as ValidateAttribute;
        private bool HasMessage => _helpBoxType != HelpBoxType.None;
        
        private string Message => _helpBoxType switch
        {
            HelpBoxType.Error => ERROR_MESSAGE,
            HelpBoxType.Validation => Attribute.Message,
            _ => string.Empty
        };

        private float _baseHeight;
        private HelpBoxType _helpBoxType;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            _baseHeight = base.GetPropertyHeight(property, label);
            _helpBoxType = GetHelpBoxType(property);

            if (_helpBoxType == HelpBoxType.None)
                return _baseHeight;

            var style = GUI.skin.GetStyle("helpbox");
            var content = new GUIContent(Message);
            var height = style.CalcHeight(content, EditorGUIUtility.currentViewWidth);
            height += MARGING * 2;

            return _baseHeight + height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (HasMessage)
            {
                var helpPosition = position;
                helpPosition.height -= _baseHeight + MARGING;
                EditorGUI.HelpBox(helpPosition, Message, MessageType.Error);

                position.y += helpPosition.height + MARGING;
                position.height = _baseHeight;
            }

            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndProperty();
        }

        private HelpBoxType GetHelpBoxType(SerializedProperty property)
        {
            if (!AttributeUtility.TryGetMethodResultAs<bool>(property, Attribute.Condition, out var result))
                return HelpBoxType.Error;

            if (result)
                return HelpBoxType.None;

            return HelpBoxType.Validation;
        }

        private enum HelpBoxType
        {
            None,
            Error,
            Validation
        }
    }
}
