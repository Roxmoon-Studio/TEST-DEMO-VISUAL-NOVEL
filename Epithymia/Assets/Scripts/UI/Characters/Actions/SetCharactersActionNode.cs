using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Characters/Set Characters")]
    public class SetCharactersActionNode : BaseActionNode
    {
        private class SetCharactersActionTask : BaseActionTask
        {
            [Inject] private readonly CharacterDisplay _display;
            private readonly CharacterVisualData[] _visualDataArray;

            public SetCharactersActionTask(CharacterVisualData[] visualDataArray)
            {
                _visualDataArray = visualDataArray;
            }

            protected override async UniTask RunInternal() => 
                await _display.SetCharacters(_visualDataArray);
        }
        
        [SerializeField] private CharacterVisualData[] _visualDataArray;

        public override BaseActionTask CreateActionTask() =>
            new SetCharactersActionTask(_visualDataArray);
    }
}
