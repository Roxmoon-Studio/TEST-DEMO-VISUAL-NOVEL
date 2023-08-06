using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioService : IAudioService
    {
        private const string MASTER_VOLUME = "Master Volume";
        private const string MUSIC_VOLUME = "Music Volume";
        private const string SOUND_VOLUME = "Sound Volume";

        public Volume MasterVolume { get; }
        public Volume MusicVolume { get; }
        public Volume SoundVolume { get; }

        private readonly AudioSource _soundSource;
        private readonly AudioConfig _config;

        public AudioService(AudioMixer mixer, AudioConfig config)
        {
            MasterVolume = new Volume(mixer, 1f, MASTER_VOLUME);
            MusicVolume = new Volume(mixer, 1f, MUSIC_VOLUME);
            SoundVolume = new Volume(mixer, 1f, SOUND_VOLUME);
            _config = config;

            _soundSource = CreateSoundSource();
        }

        private AudioSource CreateSoundSource()
        {
            var go = new GameObject(_config.AudioSourceName);
            Object.DontDestroyOnLoad(go);
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = _config.SoundMixerGroup;

            return audioSource;
        }
        
        public void PlayClick() => 
            _soundSource.PlayOneShot(_config.ClickSound);
    }
}
