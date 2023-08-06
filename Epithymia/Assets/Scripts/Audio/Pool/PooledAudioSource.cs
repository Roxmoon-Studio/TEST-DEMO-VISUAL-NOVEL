using System;
using UnityEngine;

namespace Audio
{
    public class PooledAudioSource : IDisposable
    {
        private readonly AudioSource _source;
        private readonly SoundPool _pool;

        public PooledAudioSource(AudioSource source, SoundPool pool)
        {
            _source = source;
            _pool = pool;
        }

        public void Play(AudioClip clip, float volume = 1f) => 
            Play(clip, false, volume);

        public void PlayLooping(AudioClip clip, float volume = 1f) => 
            Play(clip, true, volume);

        private void Play(AudioClip clip, bool isLooping, float volume)
        {
            _source.clip = clip;
            _source.volume = volume;
            _source.loop = isLooping;
            _source.Play();
        }

        public void Dispose()
        {
            _source.Stop();
            _pool.Release(this);
        }
    }
}
