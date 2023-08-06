using System;
using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Audio
{
    [CreateNodeMenu(MENU_NAME + "Audio/Play Sound")]
    public class PlaySoundUtilityNode : BaseUtilityNode
    {
        private class PlaySoundUtilityTask : BaseUtilityTask
        {
            [Inject] private readonly SoundPlayer _soundPlayer;

            private readonly AudioClip _audioClip;
            private readonly float _delay;
            private readonly bool _isLooping;
            private readonly float _volume;

            public PlaySoundUtilityTask(AudioClip clip, float delay, bool isLooping, float volume)
            {
                _audioClip = clip;
                _delay = delay;
                _isLooping = isLooping;
                _volume = volume;
            }

            protected override async UniTask RunInternal()
            {
                if (_delay > 0f) 
                    await UniTask.Delay(TimeSpan.FromSeconds(_delay));

                if (_isLooping) 
                    _soundPlayer.PlayLooping(_audioClip, _volume);
                else
                    _soundPlayer.PlayOneShot(_audioClip, _volume);
            }
        }

        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _delay;
        [SerializeField] private bool _isLooping;
        [SerializeField] private float _volume = 1f;

        public override BaseUtilityTask CreateUtilityTask() => 
            new PlaySoundUtilityTask(_audioClip, _delay, _isLooping, _volume);
    }
}
