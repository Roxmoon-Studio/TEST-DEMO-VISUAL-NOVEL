using Core.Actions;
using Cysharp.Threading.Tasks;
using Roxmoon.Epithymia;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Scenes {
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AssetReferenceT<NovelChapter> _novelChapter;
        
        [Inject] private IActionService _actionService;

        private void Start()
        {
            LoadChapter().Forget();
        }

        private async UniTaskVoid LoadChapter()
        {
            var handler = Addressables.LoadAssetAsync<NovelChapter>(_novelChapter);
            await handler.Task;
            var chapter = handler.Result;
            _actionService.StartAction(chapter.StartNode);
        }
    }
}
