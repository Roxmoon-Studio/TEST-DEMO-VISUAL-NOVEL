using Audio;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioConfig _audioConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_audioMixer);
            builder.RegisterInstance(_audioConfig);
            builder.Register<IAudioService, AudioService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
        }
    }
}
