using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SoundPool : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixerGroup;
        
        private readonly Queue<PooledAudioSource> _unusedPool = new();
        
        public PooledAudioSource Get()
        {
            PooledAudioSource result;

            if (_unusedPool.Count > 0)
                result = _unusedPool.Dequeue();
            else
                result = Create();

            return result;
        }

        private PooledAudioSource Create()
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = _mixerGroup;
            var pooled = new PooledAudioSource(audioSource, this);
            _unusedPool.Enqueue(pooled);

            return pooled;
        }

        public void Release(PooledAudioSource pooled) => 
            _unusedPool.Enqueue(pooled);
    }
}
