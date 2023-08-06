using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using XNode;

namespace Core.Actions
{
    public abstract class BaseActionNode : Node
    {
        public const string MENU_NAME = "Actions/";
        
        public abstract class BaseActionTask
        {
            public virtual async UniTask<int> RunAsync()
            {
                await RunInternal();

                return 0;
            }

            protected abstract UniTask RunInternal();
        }

        private class EmptyActionTask : BaseActionTask
        {
            protected override UniTask RunInternal() =>
                UniTask.CompletedTask;
        }

        [Input(ShowBackingValue.Never), ReadOnly]
        public BaseActionNode InputNode;

        [Output(connectionType : ConnectionType.Override, typeConstraint: TypeConstraint.Strict), ReadOnly]
        public BaseActionNode OutputNode;
        
        [Output(typeConstraint: TypeConstraint.Strict), ReadOnly]
        public BaseUtilityNode UtilityOutput;

        public virtual BaseActionTask CreateActionTask() =>
            new EmptyActionTask();

        public virtual BaseActionNode GetOptionAction(int index)
        {
            NodePort outputPort = GetOutputPort(nameof(OutputNode));

            if (outputPort.Connection == null)
                return null;

            return outputPort.Connection.node as BaseActionNode;
        }

        public IEnumerable<BaseUtilityNode> GetUtilityNodes()
        {
            NodePort outputPort = GetOutputPort(nameof(UtilityOutput));
            List<BaseUtilityNode> list = new();
            
            foreach (var connection in outputPort.GetConnections())
                list.Add(connection.node as BaseUtilityNode);
            
            return list;
        }

        public override object GetValue(NodePort port) =>
            null;
    }
}
