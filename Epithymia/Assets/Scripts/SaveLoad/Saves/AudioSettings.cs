using System;

namespace SaveLoad.Saves
{
    [Serializable]
    public class AudioSettings
    {
        public static readonly string DEFAULT_PATH = "Audio";
        
        public float MasterVolume { get; set; } = 1f;
        public float MusicVolume { get; set; } = 1f;
        public float SoundVolume { get; set; } = 1f;
    }
}
