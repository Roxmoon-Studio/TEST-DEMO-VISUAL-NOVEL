using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace UI.Buttons.Animations
{
    [Serializable]
    public abstract class ButtonAnimation : IDisposable
    {
        [field: SerializeField] public bool IsAwaiting { get; private set; }

        protected CancellationTokenSource _cts;

        public abstract UniTask AnimateAsync(ExtendedButton button);

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        protected void RecreateToken() => 
            CancellationTokenSourceUtility.Recreate(ref _cts);
    }
}
