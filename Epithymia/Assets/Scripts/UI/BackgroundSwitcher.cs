using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackgroundSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _transitionTime;

        private static readonly int s_progress = Shader.PropertyToID("_Progress");
        private static readonly int s_mainTexture = Shader.PropertyToID("_MainTexture");
        private static readonly int s_transitionTexture = Shader.PropertyToID("_TransitionTexture");

        public async UniTask SetBackground(Sprite background, BackgroundTransition transition = BackgroundTransition.None)
        {
            switch (transition)
            {
                case BackgroundTransition.None:
                    _backgroundImage.sprite = background;
                    _backgroundImage.color = Color.white;
                    break;
                case BackgroundTransition.Fade:
                    await SwitchFadeAsync(background);
                    break;
                case BackgroundTransition.SliderDissolve:
                    await SwitchSliderDissolveAsync(background);
                    break;
            }
        }

        private async UniTask SwitchFadeAsync(Sprite background)
        {
            float period = _transitionTime / 2;
            float fadeInTime = 0f;

            while (fadeInTime < period && _backgroundImage.sprite != null)
            {
                await UniTask.Yield();
                fadeInTime += Time.deltaTime;
                float progress = fadeInTime / period;
                SetColor(1f - progress);
            }

            _backgroundImage.sprite = background;

            float fadeOutTime = 0f;

            while (fadeOutTime < period)
            {
                await UniTask.Yield();
                fadeOutTime += Time.deltaTime;
                float progress = fadeOutTime / period;
                SetColor(progress);
            }
        }

        private void SetColor(float color) =>
            _backgroundImage.color = new Color(color, color, color);

        private async UniTask SwitchSliderDissolveAsync(Sprite background)
        {
            float time = 0f;
            _backgroundImage.material = Resources.Load<Material>("Shaders/Left Slider Transition Material");
            _backgroundImage.material.SetTexture(s_mainTexture, _backgroundImage.sprite.texture);
            _backgroundImage.material.SetTexture(s_transitionTexture, background.texture);
            _backgroundImage.material.SetFloat(s_progress, 0f);

            while (time < _transitionTime)
            {
                await UniTask.Yield();
                time += Time.deltaTime;
                float progress = time / _transitionTime;
                _backgroundImage.material.SetFloat(s_progress, progress);
            }

            _backgroundImage.sprite = background;
            _backgroundImage.material = null;
        }
    }
}
