using Audio;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using AudioSettings = SaveLoad.Saves.AudioSettings;

namespace UI.Panels
{
    public class SettingsPanel : MonoBehaviour
    {
        [Inject] private IAudioService _audioService;
        [Inject] private ISaveLoadService _saveLoadService;
        
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private void Awake()
        {
            Subscribe();
            InitValuesFromSave();
        }

        private void Subscribe()
        {
            _masterSlider.onValueChanged.AddListener(value => _audioService.MasterVolume.Value = value);
            _musicSlider.onValueChanged.AddListener(value => _audioService.MusicVolume.Value = value);
            _soundSlider.onValueChanged.AddListener(value => _audioService.SoundVolume.Value = value);
        }

        private void InitValuesFromSave()
        {
            var audioSettings = _saveLoadService.Load<AudioSettings>(AudioSettings.DEFAULT_PATH, SaveType.Settings);
            _masterSlider.value = audioSettings.MasterVolume;
            _musicSlider.value = audioSettings.MusicVolume;
            _soundSlider.value = audioSettings.SoundVolume;
        }

        private void OnDestroy()
        {
            var audioSettings = new AudioSettings();
            audioSettings.MasterVolume = _masterSlider.value;
            audioSettings.MusicVolume = _musicSlider.value;
            audioSettings.SoundVolume = _soundSlider.value;
            _saveLoadService.Save(audioSettings, AudioSettings.DEFAULT_PATH, SaveType.Settings);
        }
    }
}
