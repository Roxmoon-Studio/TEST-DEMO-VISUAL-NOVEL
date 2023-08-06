using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Core.Actions
{
    public class ActionService : IActionService
    {
        private readonly IObjectResolver _resolver;
        
        private BaseActionNode _currentActionNode;

        public ActionService(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void StartAction(BaseActionNode actionNode)
        {
            _currentActionNode = actionNode;
            
            Run().Forget();
        }

        private async UniTask Run()
        {
            IEnumerable<BaseUtilityNode> utilityNodes = new List<BaseUtilityNode>();
            
            while (_currentActionNode != null)
            {
                var task = _currentActionNode.CreateActionTask();
                _resolver.Inject(task);
                RunUtilityNodes(utilityNodes);

                int option = await task.RunAsync();

                utilityNodes = _currentActionNode.GetUtilityNodes();
                _currentActionNode = _currentActionNode.GetOptionAction(option);
            }
        }

        private void RunUtilityNodes(IEnumerable<BaseUtilityNode> utilityNodes)
        {
            foreach (var node in utilityNodes)
            {
                var utilityTask = node.CreateUtilityTask();
                _resolver.Inject(utilityTask);
                utilityTask.RunAsync().Forget();
            }
        }
    }
}
