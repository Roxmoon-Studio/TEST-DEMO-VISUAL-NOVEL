namespace Audio
{
    public interface IAudioService
    {
        Volume MasterVolume { get; }
        Volume MusicVolume { get; }
        Volume SoundVolume { get; }

        void PlayClick();
    }
}
