using System;
using System.Collections.Generic;
using Attributes;
using Logger;
using UnityEngine;
using VContainer;

namespace Audio
{
    [RequireComponent(typeof(SoundPool))]
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [Inject] private ILogService _logService;
        
        [SerializeField, AutoInit] private AudioSource _audioSource;
        [SerializeField, AutoInit] private SoundPool _soundPool;

        private readonly Dictionary<AudioClip, IDisposable> _soundMap = new();

        public void PlayOneShot(AudioClip audioClip, float volume = 1f) => 
            _audioSource.PlayOneShot(audioClip, volume);

        public void PlayLooping(AudioClip audioClip, float volume = 1f)
        {
            if (_soundMap.ContainsKey(audioClip))
            {
                _logService.LogWarning("AudioClip is already playing.");
                return;
            }

            var pooledAudioSource = _soundPool.Get();
            pooledAudioSource.PlayLooping(audioClip, volume);
            _soundMap[audioClip] = pooledAudioSource;
        }
        
        public void StopSound(AudioClip clip)
        {
            if (!_soundMap.ContainsKey(clip))
            {
                _logService.LogWarning("AudioClip is not playing.");
                return;
            }
            
            _soundMap[clip].Dispose();
            _soundMap.Remove(clip);
        }
    }
}
