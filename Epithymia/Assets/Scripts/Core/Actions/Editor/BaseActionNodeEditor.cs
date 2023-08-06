using Roxmoon.Epithymia;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Core.Actions.Editor
{
    [CustomNodeEditor(typeof(BaseActionNode))]
    public class BaseActionNodeEditor : NodeEditor
    {
        private readonly Color _startNodeColor = new Color(0.39f, 0.53f, 0.51f);

        public override Color GetTint()
        {
            if (HasStartNodeStatus())
                return _startNodeColor;

            return base.GetTint();
        }

        public override void AddContextMenuItems(GenericMenu menu)
        {
            menu.AddItem(new GUIContent("Set as Start Node"), false, SetAsStartNode);
            base.AddContextMenuItems(menu);
        }

        private void SetAsStartNode()
        {
            var chapter = target.graph as NovelChapter;
            chapter.StartNode = (BaseActionNode) target;
            EditorUtility.SetDirty(target);
        }

        private bool HasStartNodeStatus()
        {
            BaseActionNode node = target as BaseActionNode;
            NovelChapter chapter = node.graph as NovelChapter;

            return chapter.StartNode == node;
        }
    }
}
