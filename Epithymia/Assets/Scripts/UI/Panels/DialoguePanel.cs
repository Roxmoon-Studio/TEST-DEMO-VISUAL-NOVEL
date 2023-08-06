using System;
using Core.Dialogues;
using Core.Dialogues.StaticData;
using Cysharp.Threading.Tasks;
using Logger;
using TMPro;
using UI.Buttons;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace UI.Panels
{
    public class DialoguePanel : MonoBehaviour, IDialogueService
    {
        private static readonly int s_show = Animator.StringToHash("Show");
        private static readonly int s_hide = Animator.StringToHash("Hide");
        
        public bool IsFinishedTyping => _typeWriter.IsFinished;

        [Inject] private ILogService _logService;

        [SerializeField] private TextMeshProUGUI _authorTextMesh;
        [SerializeField] private TextMeshProUGUI _dialogueTextMesh;
        [SerializeField] private GameObject _name;
        [SerializeField] private ExtendedButton[] _optionButtons;
        [SerializeField] private int _defaultCharacterPerMinute = 1500;
        [SerializeField] private GameObject _easyFadeEffect;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _durationAnimation;

        private string[] _cachedOptions;
        private Action<int> _cachedOptionCallback;
        private TypeWriter _typeWriter;
        private bool _isShowed;

        private void Awake()
        {
            _typeWriter = new(_dialogueTextMesh, _defaultCharacterPerMinute, _logService);
            _typeWriter.Finished += CreateOptions;
        }

        private void OnDestroy() =>
            _typeWriter.Dispose();

        public void SetOptions(string[] options, Action<int> callback)
        {
            _cachedOptions = options;
            _cachedOptionCallback = callback;
        }

        public void FinishTyping() => 
            _typeWriter.Finish();

        public void SetDialogue(DialogueData data)
        {
            SetAuthor(data.Author);
            _typeWriter.WriteText(data.Text);
            _easyFadeEffect.SetActive(data.IsFaded);
        }

        public void ClearText()
        {
            SetAuthor(string.Empty);
            _easyFadeEffect.SetActive(false);
            _typeWriter.Clear();
        }

        private void SetAuthor(string author)
        {
            _authorTextMesh.text = author.ToUpper();
            _name.SetActive(author != string.Empty);
        }


        private void CreateOptions()
        {
            if (_cachedOptions == null)
                return;

            for (int i = 0; i < _cachedOptions.Length; i++)
            {
                _optionButtons[i].Text = _cachedOptions[i];
                _optionButtons[i].Clicked.AddListener(InvokeOptionSelected(i));
                _optionButtons[i].Clicked.AddListener(Clear);
                _optionButtons[i].IsVisible = true;
            }

            _cachedOptions = null;
        }

        private UnityAction InvokeOptionSelected(int index) => 
            () => _cachedOptionCallback(index);

        private void Clear()
        {
            _cachedOptionCallback = null;
            _cachedOptions = null;
            
            foreach (var optionButton in _optionButtons)
            {
                optionButton.Clicked.RemoveAllListeners();
                optionButton.IsVisible = false;
            }
        }

        public async UniTask ShowAsync()
        {
            if (_isShowed)
                return;
            
            _animator.SetTrigger(s_show);
            _isShowed = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_durationAnimation));
        }

        public async UniTask HideAsync()
        {
            if (!_isShowed)
                return;

            _animator.SetTrigger(s_hide);
            _isShowed = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_durationAnimation));
        }
    }
}
