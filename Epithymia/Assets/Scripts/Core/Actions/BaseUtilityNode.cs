using Cysharp.Threading.Tasks;
using XNode;

namespace Core.Actions
{
    public abstract class BaseUtilityNode : Node
    {
        public const string MENU_NAME = "Utilities/";
        
        public abstract class BaseUtilityTask
        {
            public virtual async UniTask RunAsync()
            {
                await RunInternal();
            }

            protected abstract UniTask RunInternal();
        }

        private class EmptyUtilityTask : BaseUtilityTask
        {
            protected override UniTask RunInternal() =>
                UniTask.CompletedTask;
        }
        
        [Input(typeConstraint: TypeConstraint.Strict), ReadOnly]
        public BaseUtilityNode Input;

        public virtual BaseUtilityTask CreateUtilityTask() =>
            new EmptyUtilityTask();
    }
}
