using System;
using Attributes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Characters
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public class CharacterSlot : MonoBehaviour
    {
        private const int OVERLAY_ORDER = -1;
        private const int DEFAULT_ORDER = -3;
        
        private static readonly int s_appearance = Animator.StringToHash("Appearance");
        private static readonly int s_disappearance = Animator.StringToHash("Disappearance");

        [SerializeField] private float _duration;
        [SerializeField, AutoInit] private Image _image;
        [SerializeField, AutoInit] private Animator _animator;
        [SerializeField, AutoInit] private Canvas _canvas;
        
        public async UniTask SetCharacter(Sprite sprite)
        {
            if (gameObject.activeSelf)
            {
                SetSprite(sprite);
            }
            else
            {
                SetSprite(sprite);
                await PlayAppearanceAnimationAsync();
            }
        }

        public async UniTask Clear()
        {
            if (!gameObject.activeSelf)
                return;

            _animator.SetTrigger(s_disappearance);
            await WaitClipDuration();
            gameObject.SetActive(false);
            _canvas.sortingOrder = DEFAULT_ORDER;
        }

        private async UniTask PlayAppearanceAnimationAsync()
        {
            _animator.SetTrigger(s_appearance);
            await WaitClipDuration();
        }

        private void SetSprite(Sprite sprite)
        {
            gameObject.SetActive(true);
            _image.sprite = sprite;

            if (CharacterUtility.IsKat(sprite))
                _canvas.sortingOrder = OVERLAY_ORDER;
            else
                _canvas.sortingOrder = DEFAULT_ORDER;
        }

        private async UniTask WaitClipDuration()
        {
            var delaySpan = TimeSpan.FromSeconds(_duration);

            await UniTask.Delay(delaySpan);
        }

        public async UniTask MoveTo(CharacterSlot slot)
        {
            Sprite sprite = _image.sprite;
            await Clear();
            await slot.SetCharacter(sprite);
        }
    }
}
