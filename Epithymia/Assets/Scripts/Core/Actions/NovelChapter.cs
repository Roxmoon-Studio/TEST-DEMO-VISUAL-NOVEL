using Core.Actions;
using UnityEngine;
using XNode;

namespace Roxmoon.Epithymia
{
    [CreateAssetMenu]
    public class NovelChapter : NodeGraph
    {
        [ReadOnly] public BaseActionNode StartNode;
    }
}
