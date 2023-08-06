using Core.Actions;
using Cysharp.Threading.Tasks;
using UI.Panels;
using VContainer;

namespace UI
{
    [CreateNodeMenu(MENU_NAME + "UI/Show Finish Buttons Panel")]
    public class ShowFinishButtonsPanelActionNode : BaseActionNode
    {
        private class ShowFinishButtonsPanelActionTask : BaseActionTask
        {
            [Inject] private readonly FinishButtonsPanel _finishButtonsPanel;

            protected override UniTask RunInternal()
            {
                _finishButtonsPanel.gameObject.SetActive(true);
                _finishButtonsPanel.Show();
                
                return UniTask.CompletedTask;
            } 
        }

        public override BaseActionTask CreateActionTask() => 
            new ShowFinishButtonsPanelActionTask();
    }
}
