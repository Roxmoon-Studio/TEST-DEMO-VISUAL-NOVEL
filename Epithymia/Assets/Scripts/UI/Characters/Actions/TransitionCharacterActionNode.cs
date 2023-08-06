using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Characters/Transition Character")]
    public class TransitionCharacterActionNode : BaseActionNode
    {
        private class TransitionCharacterActionTask : BaseActionTask
        {
            [Inject] private CharacterDisplay _display;

            private readonly CharacterTransitionData _data;

            public TransitionCharacterActionTask(CharacterTransitionData data)
            {
                _data = data;
            }

            protected override async UniTask RunInternal() => 
                await _display.MoveCharacter(_data);
        }
        
        [SerializeField] private CharacterTransitionData _characterTransitionData;
        
        public override BaseActionTask CreateActionTask()
        {
            return new TransitionCharacterActionTask(_characterTransitionData);
        }
    }
}
