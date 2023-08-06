using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Characters/Add Character")]
    public class AddCharacterActionNode : BaseActionNode
    {
        private class AddCharacterActionTask : BaseActionTask
        {
            [Inject] private readonly CharacterDisplay _display;
            private readonly CharacterVisualData _visualData;

            public AddCharacterActionTask(CharacterVisualData visualData)
            {
                _visualData = visualData;
            }

            protected override async UniTask RunInternal() =>
                await _display.AddCharacter(_visualData);
        }

        [SerializeField] private CharacterVisualData _visualData;

        public override BaseActionTask CreateActionTask() =>
            new AddCharacterActionTask(_visualData);
    }
}
