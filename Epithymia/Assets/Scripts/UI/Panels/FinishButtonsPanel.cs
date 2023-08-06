using Cysharp.Threading.Tasks;
using UI.Buttons;
using UnityEngine;

namespace UI.Panels
{
    public class FinishButtonsPanel : MonoBehaviour
    {
        [Header("Google")]
        [SerializeField] private string _googleFormUrl;
        [SerializeField] private ExtendedButton _googleFormButton;
        [Space]
        
        [Header("VK")]
        [SerializeField] private string _vkUrl;
        [SerializeField] private ExtendedButton _vkButton;
        [Space]
        
        [Header("Telegram")]
        [SerializeField] private string _telegramUrl;
        [SerializeField] private ExtendedButton _telegramButton;
        [Space]

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration;

        private void Awake()
        {
            _googleFormButton.Clicked.AddListener(() => Application.OpenURL(_googleFormUrl));
            _vkButton.Clicked.AddListener(() => Application.OpenURL(_vkUrl));
            _telegramButton.Clicked.AddListener(() => Application.OpenURL(_telegramUrl));
        }

        public void Show() => 
            AppearThroughAlphaAsync().Forget();

        private async UniTask AppearThroughAlphaAsync()
        {
            float time = 0f;
            float step = 1f / _duration;

            while (time < _duration)
            {
                await UniTask.Yield();
                _canvasGroup.alpha += step * Time.deltaTime;
                time += Time.deltaTime;
            }
        }
    }
}
