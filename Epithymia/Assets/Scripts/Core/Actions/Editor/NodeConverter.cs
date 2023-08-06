using Roxmoon.Epithymia;
using UnityEditor;

namespace Core.Actions.Editor
{
    public static class NodeConverter
    {
        // [MenuItem("Tools/Convert Nodes")]
        // public static void Convert()
        // {
        //     string[] guids = AssetDatabase.FindAssets("t:NovelChapter");
        //
        //     foreach (var guid in guids)
        //     {
        //         var path = AssetDatabase.GUIDToAssetPath(guid);
        //         var chapter = AssetDatabase.LoadAssetAtPath<NovelChapter>(path);
        //         ConvertChapter(chapter);
        //     }
        // }
        //
        // private static void ConvertChapter(NovelChapter chapter)
        // {
        //     foreach (var node in chapter.nodes)
        //     {
        //         var ban = node as BaseActionNode;
        //         var nextOutput = ban.GetOutputPort("Output");
        //
        //         if (nextOutput == null)
        //             continue;
        //
        //         if (nextOutput.Connection == null)
        //             continue;
        //         
        //         var connectionNode = nextOutput.Connection.node;
        //         var temp = ban.GetOutputPort("OutputNode");
        //         temp.Connect(connectionNode.GetInputPort("InputNode"));
        //         nextOutput.ClearConnections();
        //         EditorUtility.SetDirty(node);
        //     }
        // }
    }
}
