using Core.Actions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Audio
{
    [CreateNodeMenu(MENU_NAME + "Audio/Stop Sound")]
    public class StopSoundUtilityNode  : BaseUtilityNode
    {
        private class StopSoundUtilityTask : BaseUtilityTask
        {
            [Inject] private readonly SoundPlayer _soundPlayer;

            private readonly AudioClip _audioClip;

            public StopSoundUtilityTask(AudioClip clip)
            {
                _audioClip = clip;
            }

            protected override UniTask RunInternal()
            {
                _soundPlayer.StopSound(_audioClip);
                
                return UniTask.CompletedTask;
            }
        }

        [SerializeField] private AudioClip _audioClip;

        public override BaseUtilityTask CreateUtilityTask() => 
            new StopSoundUtilityTask(_audioClip);

    }
}
