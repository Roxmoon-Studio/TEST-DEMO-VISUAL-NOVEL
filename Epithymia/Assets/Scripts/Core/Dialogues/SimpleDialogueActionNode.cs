using Core.Actions;
using Core.Dialogues.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;
using VContainer;

namespace Core.Dialogues
{
    [CreateNodeMenu(MENU_NAME + "Text/Simple Dialogue")]
    public class SimpleDialogueActionNode : BaseActionNode
    {
        private class SimpleDialogueActionTask : BaseActionTask
        {
            [Inject] private readonly IDialogueService _dialogueService;
            [Inject] private readonly IInput _input;
            
            private readonly DialogueData _data;

            private bool _isClicked;
            private bool _isCompleted;

            public SimpleDialogueActionTask(DialogueData data)
            {
                _data = data;
            }

            protected override async UniTask RunInternal()
            {
                _dialogueService.SetDialogue(_data);

                _isCompleted = false;
                _input.Clicked += OnClicked;

                while (!_isCompleted)
                {
                    if (InputHelper.CheckSkipDialogue()) 
                        OnClicked();

                    await UniTask.Yield();
                }

                _input.Clicked -= OnClicked;

                _dialogueService.ClearText();
            }

            private void OnClicked()
            {
                if (_dialogueService.IsFinishedTyping)
                    _isCompleted = true;

                _dialogueService.FinishTyping();
            }
        }

        [SerializeField] private DialogueData _dialogueData;

        public override BaseActionTask CreateActionTask() =>
            new SimpleDialogueActionTask(_dialogueData);
    }
}
