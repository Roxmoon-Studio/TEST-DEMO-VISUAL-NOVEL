using Core.Actions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace UI.Panels
{
    [CreateNodeMenu(MENU_NAME + "UI/Panel/Show Dialogue Panel")]
    public class ShowDialoguePanelActionNode : BaseActionNode
    {
        private class ShowDialoguePanelActionTask : BaseActionTask
        {
            [Inject] private DialoguePanel _dialoguePanel;
            
            protected override async UniTask RunInternal() => 
                await _dialoguePanel.ShowAsync();
        }
        
        public override BaseActionTask CreateActionTask() => 
            new ShowDialoguePanelActionTask();
    }
}
