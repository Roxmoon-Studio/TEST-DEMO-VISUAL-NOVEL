using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace UI
{
    [CreateNodeMenu(MENU_NAME + "UI/Change Background")]
    public class ChangeBackgroundActionNode : BaseActionNode
    {
        private class ChangeBackgroundActionTask : BaseActionTask {
            [Inject] private readonly BackgroundSwitcher _backgroundSwitcher;
            private readonly BackgroundTransition _transition;
            private readonly AssetReference _spriteReference;

            public ChangeBackgroundActionTask(BackgroundTransition transition, AssetReference spriteReference)
            {
                _transition = transition;
                _spriteReference = spriteReference;
            }

            protected override async UniTask RunInternal()
            {
                var handler = Addressables.LoadAssetAsync<Sprite>(_spriteReference);
                await handler.Task;
                var sprite = handler.Result;
                await _backgroundSwitcher.SetBackground(sprite, _transition);
            }
        }

        [SerializeField] private BackgroundTransition _transition;
        [SerializeField] private AssetReferenceSprite _backgroundSprite;

        public override BaseActionTask CreateActionTask() =>
            new ChangeBackgroundActionTask(_transition, _backgroundSprite);
    }
}
