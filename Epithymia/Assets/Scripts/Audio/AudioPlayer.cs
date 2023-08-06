using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public event Action<AudioClip> MusicChanged;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _smoothStartDuration = 1f;
        [SerializeField] private float _smoothStopDuration = 1f;

        public void SetMusic(AudioClip audioClip, bool isLooping, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.loop = isLooping;
            _audioSource.Play();
            SmoothStartAsync(volume).Forget();
            MusicChanged?.Invoke(audioClip);
        }

        public async UniTask StopMusicAsync()
        {
            await SmoothStopAsync();
            _audioSource.Stop();
        }

        public AudioClip GetCurrentMusic() =>
            _audioSource.clip;

        private async UniTask SmoothStartAsync(float volume)
        {
            float step = volume / _smoothStartDuration;
            _audioSource.volume = 0f;

            while (!MathUtility.Approximately(volume, _audioSource.volume))
            {
                _audioSource.volume += step * Time.deltaTime;

                if (_audioSource.volume > volume)
                    _audioSource.volume = volume;

                await UniTask.Yield();
            }
        }

        private async UniTask SmoothStopAsync()
        {
            float step = _audioSource.volume / _smoothStopDuration;

            while (!MathUtility.Approximately(0f, _audioSource.volume))
            {
                _audioSource.volume -= step * Time.deltaTime;

                if (_audioSource.volume < 0f)
                    _audioSource.volume = 0f;

                await UniTask.Yield();
            }
        }
    }
}
