using Cysharp.Threading.Tasks;
using UI.Buttons;
using UI.Windows;
using UnityEngine;
using Utilities;
using VContainer;

namespace UI.Panels
{
    public class OptionsPanel : MonoBehaviour
    {
        [Inject] private IObjectResolver _resolver;
        
        [SerializeField] private ExtendedButton _toggleButton;
        [SerializeField] private ExtendedButton _settingsButton;
        [SerializeField] private ExtendedButton _menuButton;
        [SerializeField] private ExtendedButton _saveButton;
        [SerializeField] private GameObject _togglePanel;
        [SerializeField, Scene] private string _mainMenuScene;
        [SerializeField] private GameObject _settingsCanvas;
        [SerializeField] private Transform _canvas;

        private void Awake()
        {
            _toggleButton.Clicked.AddListener(OnToggleButtonClicked);
            _settingsButton.Clicked.AddListener(OnSettingsButtonClicked);
            _menuButton.Clicked.AddListener(OnMenuButtonClicked);
            _saveButton.Clicked.AddListener(OnSaveButtonClicked);
        }

        private void OnToggleButtonClicked() => 
            _togglePanel.SetActive(!_togglePanel.activeSelf);

        private void OnSettingsButtonClicked() => 
            _settingsCanvas.SetActive(!_settingsCanvas.activeSelf);

        private void OnMenuButtonClicked()
        {
            PopupWindow.OpenAsync(
                resolver: _resolver,
                parent: _canvas,
                text: "Вы действительно хотите выйти в главное меню? (Данные игры не сохранятся.)",
                okCallback: () => SceneLoader.LoadScene(_mainMenuScene).Forget()
                ).Forget();
        }

        private void OnSaveButtonClicked()
        {
            
        }
    }
}
