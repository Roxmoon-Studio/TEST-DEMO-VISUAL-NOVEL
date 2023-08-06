using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Characters/Change Character")]
    public class ChangeCharacterActionNode : BaseUtilityNode
    {
        private class ChangeCharacterActionTask : BaseUtilityTask
        {
            [Inject] private readonly CharacterDisplay _display;
            private readonly CharacterVisualData _visualData;

            public ChangeCharacterActionTask(CharacterVisualData visualData)
            {
                _visualData = visualData;
            }

            protected override async UniTask RunInternal() =>
                await _display.AddCharacter(_visualData);
        }

        [SerializeField] private CharacterVisualData _visualData;

        public override BaseUtilityTask CreateUtilityTask() =>
            new ChangeCharacterActionTask(_visualData);
    }
}
