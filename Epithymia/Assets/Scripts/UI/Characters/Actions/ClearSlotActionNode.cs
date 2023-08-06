using Core.Actions;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Clear Slot")]
    public class ClearSlotActionNode : BaseActionNode
    {
        private class ClearSlotActionTask : BaseActionTask
        {
            [Inject] private readonly CharacterDisplay _display;
            private readonly SlotPlace _slotPlace;

            public ClearSlotActionTask(SlotPlace slotPlace)
            {
                _slotPlace = slotPlace;
            }

            protected override async UniTask RunInternal() => 
                await _display.ClearSlot(_slotPlace);
        }
        
        [SerializeField] private SlotPlace _slotPlace;
        
        public override BaseActionTask CreateActionTask() => 
            new ClearSlotActionTask(_slotPlace);
    }
}
