using System;
using Cysharp.Threading.Tasks;
using Roxmoon.Epithymia;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Actions
{
    [CreateNodeMenu(MENU_NAME + "Transitions/Switch Graph")]
    public class SwitchGraphActionNode : BaseActionNode
    {
        private class SwitchGraphActionTask : BaseActionTask
        {
            private readonly AssetReferenceT<NovelChapter> _reference;
            private readonly Action<NovelChapter> _callback;

            public SwitchGraphActionTask(AssetReferenceT<NovelChapter> reference, Action<NovelChapter> callback)
            {
                _reference = reference;
                _callback = callback;
            }
        
            protected override async UniTask RunInternal()
            {
                var handler = Addressables.LoadAssetAsync<NovelChapter>(_reference);
                await handler.Task;
                _callback(handler.Result);
            }
        }

        [SerializeField] private AssetReferenceT<NovelChapter> _chapterReference;

        private NovelChapter _chapter;

        public override BaseActionNode GetOptionAction(int index) => 
            _chapter.StartNode;

        public override BaseActionTask CreateActionTask() =>
            new SwitchGraphActionTask(_chapterReference, chapter => _chapter = chapter);
    }
}
