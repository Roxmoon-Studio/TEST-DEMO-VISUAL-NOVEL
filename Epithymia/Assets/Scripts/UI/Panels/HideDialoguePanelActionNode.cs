using Core.Actions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace UI.Panels
{
    [CreateNodeMenu(MENU_NAME + "UI/Panel/Hide Dialogue Panel")]
    public class HideDialoguePanelActionNode : BaseActionNode
    {
        private class HideDialoguePanelActionTask : BaseActionTask
        {
            [Inject] private DialoguePanel _dialoguePanel;
            
            protected override async UniTask RunInternal() => 
                await _dialoguePanel.HideAsync();
        }
        
        public override BaseActionTask CreateActionTask() => 
            new HideDialoguePanelActionTask();
    }
}
