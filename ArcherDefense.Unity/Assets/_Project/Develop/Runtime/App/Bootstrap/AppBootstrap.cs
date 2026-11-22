using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Bootstrap
{
    public sealed class AppBootstrap : IStartable
    {
        private const int TARGET_FRAME_RATE = 60;

        private readonly ConfigsProviderService _configsProviderService;
        private readonly GameNavigation _gameNavigation;

        public AppBootstrap(ConfigsProviderService configsProviderService, GameNavigation gameNavigation)
        {
            _configsProviderService = configsProviderService;
            _gameNavigation = gameNavigation;
        }

        public void Start()
        {
            SetupApp();

            _configsProviderService.Load();

            _gameNavigation.OpenMainMenu();
        }

        private void SetupApp()
        {
            Application.targetFrameRate = TARGET_FRAME_RATE;
            QualitySettings.vSyncCount = 0;
        }
    }
}
