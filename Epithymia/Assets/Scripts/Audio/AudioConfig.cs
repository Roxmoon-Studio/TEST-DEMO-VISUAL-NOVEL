using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(menuName = nameof(AudioConfig), fileName = nameof(AudioConfig))]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField] public string AudioSourceName { get; private set; }
        [field: SerializeField] public AudioClip ClickSound { get; private set; }
        [field: SerializeField] public AudioMixerGroup SoundMixerGroup { get; private set; }
    }
}
