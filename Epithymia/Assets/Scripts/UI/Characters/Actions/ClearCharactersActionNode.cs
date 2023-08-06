using Core.Actions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace UI.Characters.Actions
{
    [CreateNodeMenu(MENU_NAME + "UI/Characters/Clear Characters")]
    public class ClearCharactersActionNode : BaseActionNode
    {
        private class ClearCharactersActionTask : BaseActionTask
        {
            [Inject] private readonly CharacterDisplay _display;

            protected override async UniTask RunInternal() =>
                await _display.Clear();
        }
        
        public override BaseActionTask CreateActionTask() =>
            new ClearCharactersActionTask();
    }
}
