using Core.Actions;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Audio
{
    [CreateNodeMenu(MENU_NAME + "Audio/Stop Music")]
    public class StopMusicActionNode : BaseActionNode
    {
        private class StopMusicActionTask : BaseActionTask
        {
            [Inject] private AudioPlayer _audioPlayer;
            
            protected override async UniTask RunInternal() => 
                await _audioPlayer.StopMusicAsync();
        }
        
        public override BaseActionTask CreateActionTask() => 
            new StopMusicActionTask();
    }
}
