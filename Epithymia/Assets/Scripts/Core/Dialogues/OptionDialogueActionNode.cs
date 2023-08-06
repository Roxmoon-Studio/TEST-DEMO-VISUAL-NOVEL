using Core.Actions;
using Core.Dialogues.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;
using VContainer;

namespace Core.Dialogues
{
    [CreateNodeMenu(MENU_NAME + "Text/Option Dialogue")]
    public class OptionDialogueActionNode : BaseActionNode
    {
        private class OptionDialogueActionTask : BaseActionTask
        {
            [Inject] private readonly IDialogueService _dialogueService;
            [Inject] private readonly IInput _input;
            private readonly DialogueData _data;
            private readonly string[] _options;

            private int _result = -1;
            private bool _isCompleted;

            public OptionDialogueActionTask(DialogueData data, string[] options)
            {
                _data = data;
                _options = options;
            }

            public override async UniTask<int> RunAsync()
            {
                _dialogueService.SetDialogue(_data);
                _dialogueService.SetOptions(_options, callback: OptionSelected);

                await RunInternal();

                return _result;
            }

            private void OptionSelected(int optionIndex)
            {
                _result = optionIndex;
                _isCompleted = true;
            }

            protected override async UniTask RunInternal()
            {
                _input.Clicked += OnClicked;

                while (!_isCompleted)
                {
                    if (InputHelper.CheckSkipDialogue()) 
                        OnClicked();
                    
                    await UniTask.Yield();
                }

                _input.Clicked -= OnClicked;
            }

            private void OnClicked()
            {
                if (!_dialogueService.IsFinishedTyping)
                    _dialogueService.FinishTyping();
            }
        }

        [SerializeField] private DialogueData _dialogueData;
        [SerializeField, Output(ShowBackingValue.Always, dynamicPortList: true)] private string[] _options;

        public override BaseActionTask CreateActionTask() =>
            new OptionDialogueActionTask(_dialogueData, _options);

        public override BaseActionNode GetOptionAction(int index) => 
            GetOutputPort(nameof(_options) + " " + index).Connection.node as BaseActionNode;
    }
}
