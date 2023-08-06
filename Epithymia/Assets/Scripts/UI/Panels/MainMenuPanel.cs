using Cysharp.Threading.Tasks;
using UI.Buttons;
using UnityEngine;
using Utilities;

namespace UI.Panels
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField, Scene] private string _scene;
        [SerializeField] private ExtendedButton _startGameButton;
        [SerializeField] private ExtendedButton _loadGameButton;
        [SerializeField] private ExtendedButton _settingsButton;
        [SerializeField] private ExtendedButton _mediaButton;
        [SerializeField] private ExtendedButton _exitButton;
        [SerializeField] private Canvas _settingsCanvas;
        
        private void Awake()
        {
            SubscribeButtons();
        }

        private void SubscribeButtons()
        {
            _startGameButton.Clicked.AddListener(StartGame);
            _loadGameButton.Clicked.AddListener(LoadGame);
            _settingsButton.Clicked.AddListener(OpenSettings);
            _mediaButton.Clicked.AddListener(OpenMedia);
            _exitButton.Clicked.AddListener(ExitGame);
        }

        private void StartGame() => 
            SceneLoader.LoadScene(_scene).Forget();

        private void LoadGame()
        {
            //TODO открыть меню с выбором сейва, если есть
        }

        private void OpenSettings() => 
            _settingsCanvas.gameObject.SetActive(true);

        private void OpenMedia()
        {
            //TODO открыть меню с медиа
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
