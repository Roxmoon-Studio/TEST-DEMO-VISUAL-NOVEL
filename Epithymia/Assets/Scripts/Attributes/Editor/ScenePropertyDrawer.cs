using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class ScenePropertyDrawer : PropertyDrawer
    {
        private const string ERROR = "Error";
        private const string ERROR_MESSAGE = "Нельзя установить сцены, которые выключены в настройках";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SceneAsset sceneAsset = null;
            
            if (property.stringValue != "")
            {
                foreach (var scene in EditorBuildSettings.scenes)
                {
                    if (scene.path.Contains(property.stringValue))
                    {
                        if (!scene.enabled)
                        {
                            EditorUtility.DisplayDialog(ERROR, ERROR_MESSAGE, "OK");
                            break;
                        }
                        sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                    }
                }
            }

            sceneAsset = EditorGUI.ObjectField(position, label, sceneAsset, typeof(SceneAsset), false) as SceneAsset;
            property.stringValue = sceneAsset != null ? sceneAsset.name : "";
        }
    }
}