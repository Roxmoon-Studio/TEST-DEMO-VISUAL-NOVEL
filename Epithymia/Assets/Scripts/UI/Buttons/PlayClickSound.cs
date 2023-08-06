using Audio;
using UnityEngine;
using VContainer;

namespace UI.Buttons
{
    public class PlayClickSound : MonoBehaviour
    {
        [Inject]
        private void Inject(IAudioService audioService)
        {
            var button = GetComponent<ExtendedButton>();
            button.Clicked.AddListener(audioService.PlayClick);
        }
    }
}
