using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Audio
{
    [CreateNodeMenu(MENU_NAME + "Audio/Play Music")]
    public class PlayMusicActionNode : BaseActionNode
    {
        private class PlayMusicActionTask : BaseActionTask
        {
            [Inject] private readonly AudioPlayer _audioPlayer;

            private readonly AudioClip _audioClip;
            private readonly bool _isLooping;
            private readonly float _volume;

            public PlayMusicActionTask(AudioClip audioClip, bool isLooping, float volume)
            {
                _audioClip = audioClip;
                _isLooping = isLooping;
                _volume = volume;
            }

            protected override UniTask RunInternal()
            {
                _audioPlayer.SetMusic(_audioClip, _isLooping, _volume);

                return UniTask.CompletedTask;
            }
        }

        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _isLooping = true;
        [SerializeField] private float _volume = 1f;

        public override BaseActionTask CreateActionTask() =>
            new PlayMusicActionTask(_audioClip, _isLooping, _volume);
    }
}
