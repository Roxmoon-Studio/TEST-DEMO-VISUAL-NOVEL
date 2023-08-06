using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Buttons.Animations
{
    [Serializable]
    public class ZoomAnimation : ButtonAnimation
    {
        [SerializeField] private float _value = 1.5f;
        [SerializeField] private float _time = 0.3f;
        
        public override async UniTask AnimateAsync(ExtendedButton button)
        {
            if (button == null)
                return;
            
            RecreateToken();
            float time = 0f;
            Vector3 scaleVector = Vector3.one * _value;
            Vector3 currentScale = button.transform.localScale;

            while (time < _time)
            {
                await UniTask.Yield();
                
                if (_cts.Token.IsCancellationRequested || button == null)
                    return;
                
                
                time += Time.deltaTime;
                button.transform.localScale = Vector3.Lerp(currentScale, scaleVector, time / _time);
            }
        }
    }
}
