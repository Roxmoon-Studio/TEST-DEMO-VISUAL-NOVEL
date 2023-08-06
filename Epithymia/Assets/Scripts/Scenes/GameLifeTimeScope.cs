using Audio;
using Core.Actions;
using Core.Configs;
using Core.Dialogues;
using Logger;
using Services.SaveLoad;
using UI;
using UI.Characters;
using UI.Panels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scenes
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private BackgroundSwitcher _backgroundSwitcher;
        [SerializeField] private DialoguePanel _dialoguePanel;
        [SerializeField] private CharacterDisplay _characterDisplay;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private InputPanel _inputPanel;
        [SerializeField] private FinishButtonsPanel _finishButtonsPanel;

        protected override void Configure(IContainerBuilder builder)
        {
            //Проверить, что будет, если зарегистрировать инстанс, которому нужна регистрация ниже
            ConfigureInstances(builder);
            ConfigureLogger(builder);
            builder.Register<IActionService, ActionService>(Lifetime.Singleton);
            builder.Register<IConfigService, ConfigService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
        }

        private void ConfigureInstances(IContainerBuilder builder)
        {
            builder.RegisterInstance(_audioPlayer);
            builder.RegisterInstance(_soundPlayer);
            builder.RegisterInstance(_backgroundSwitcher);
            builder.RegisterInstance(_finishButtonsPanel);
            builder.RegisterInstance(_dialoguePanel).AsImplementedInterfaces();
            builder.RegisterInstance(_characterDisplay).AsImplementedInterfaces();
            builder.RegisterInstance(_inputPanel).As<IInput>();
        }

        private static void ConfigureLogger(IContainerBuilder builder)
        {
#if UNITY_EDITOR
            builder.Register<ILogService, DebugLogService>(Lifetime.Singleton);
#else
            builder.Register<ILogService, FileLogService>(Lifetime.Singleton);
#endif
        }
    }
}
