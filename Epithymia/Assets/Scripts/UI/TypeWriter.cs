using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logger;
using TMPro;

namespace UI
{
    public class TypeWriter : IDisposable
    {
        private const float SECONDS_IN_MINUTE = 60f;

        public event Action Finished;
        public bool IsFinished => _cts == null;

        private readonly TextMeshProUGUI _textMeshPro;
        private readonly TimeSpan _characterTypingDelay;
        private readonly ILogService _logService;

        private CancellationTokenSource _cts;

        public TypeWriter(TextMeshProUGUI textMeshPro, float characterPerMinute, ILogService logService)
        {
            _characterTypingDelay = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / characterPerMinute);
            _textMeshPro = textMeshPro;
            _logService = logService;
        }

        public async void WriteText(string text)
        {
            Finish();
            _cts = new CancellationTokenSource();
            try
            {
                await PrintTextCoroutine(text, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                _logService.Log("Skipped text writing");
            }
        }

        public void Finish()
        {
            if (IsFinished)
                return;
            
            Dispose();

            _textMeshPro.maxVisibleCharacters = int.MaxValue;

            Finished?.Invoke();
        }

        public void Clear() =>
            _textMeshPro.text = string.Empty;

        private async UniTask PrintTextCoroutine(string text, CancellationToken token)
        {
            _textMeshPro.text = text;

            for (int i = 0; i <= text.Length; i++)
            {
                _textMeshPro.maxVisibleCharacters = i;

                await UniTask.Delay(_characterTypingDelay, cancellationToken: token);

                if (token.IsCancellationRequested)
                    return;
            }

            Finish();
        }

        public void Dispose()
        {
            if (_cts == null)
                return;
            
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}
