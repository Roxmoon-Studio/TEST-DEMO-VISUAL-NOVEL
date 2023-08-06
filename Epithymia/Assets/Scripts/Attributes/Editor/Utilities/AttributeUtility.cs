using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Editor.Utilities
{
    internal static class AttributeUtility
    {
        internal static bool TryGetMethodResultAs<T>(SerializedProperty property, string methodName, out T result)
        {
            result = default;

            var obj = ExtractObjectFromProperty(property);

            if (!TryGetMethod(obj, methodName, out var methodInfo))
                return false;
            
            var objResult = methodInfo.Invoke(obj, new object[] { });
            
            if (objResult is not T tResult) 
                return false;
            
            result = tResult;
            
            return true;
        }

        private static object ExtractObjectFromProperty(SerializedProperty property)
        {
            var obj = (object) property.serializedObject.targetObject;
            var type = obj.GetType();
            var nestedParents = GetNestedFieldParents(property.propertyPath);

            foreach (var field in nestedParents)
            {
                var fieldInfo = type.GetField(field, (BindingFlags)~0);
                type = fieldInfo.FieldType;
                obj = fieldInfo.GetValue(obj);
            }

            return obj;
        }

        internal static bool TryGetMethod(object obj, string methodName, out MethodInfo methodInfo, params Type[] types)
        {
            Type type = obj.GetType();
            
            if (types == null) 
                types = new Type[] { };
            
            methodInfo = type.GetMethod(methodName, (BindingFlags)(-1), null, types, null);

            return methodInfo != null;
        }

        private static IEnumerable<string> GetNestedFieldParents(string path)
        {
            string[] fieldNames = path.Split('.');

            return fieldNames[..^1];
        }
    }
}
