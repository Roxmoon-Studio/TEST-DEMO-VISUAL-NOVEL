using System;
using System.Reflection;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(AutoInitAttribute))]
    public class AutoInitPropertyDrawer : PropertyDrawer
    {
        private AutoInitAttribute Attribute => attribute as AutoInitAttribute;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var serializedObject = property.serializedObject;
            serializedObject.Update();
            var target = (Component) serializedObject.targetObject;
            var targetType = target.GetType();
            var fieldInfo = targetType.GetField(property.propertyPath, (BindingFlags) ~0);

            if (IsAssignableFrom<Component>(fieldInfo.FieldType))
            {
                if (Attribute.IsChildrenIncluded)
                    property.objectReferenceValue = target.GetComponentInChildren(fieldInfo.FieldType);
                else
                    property.objectReferenceValue = target.GetComponent(fieldInfo.FieldType);
                    
            }
            else
                Debug.LogError($"{property.displayName} is not derived from Component.");
            
            serializedObject.ApplyModifiedProperties();
            
            EditorGUI.PropertyField(position, property, label);
        }

        private bool IsAssignableFrom<T>(Type type)
        {
            var componentType = typeof(T);

            return componentType.IsAssignableFrom(type);
        }
    }
}
