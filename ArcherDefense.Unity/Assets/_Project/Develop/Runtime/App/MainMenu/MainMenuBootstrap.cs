using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.MainMenu
{
    // Пока переход синхронный, сцена запускает себя сама: когда переход научится ждать её готовности, запускать будет он
    public sealed class MainMenuBootstrap : IStartable
    {
        private readonly PlayerProgressService  _playerProgress;
        private readonly WalletService          _wallet;
        private readonly ConfigsProviderService _configsProviderService;

        public MainMenuBootstrap(PlayerProgressService playerProgress, WalletService wallet, ConfigsProviderService configsProviderService)
        {
            _playerProgress         = playerProgress;
            _wallet                 = wallet;
            _configsProviderService = configsProviderService;
        }

        public void Start()
        {
            Debug.Log($"In main menu. Current levels: \n{BuildLevelListString()}");
            Debug.Log($"Gold: {_wallet.Get(CurrencyId.Gold)}");
        }

        private string BuildLevelListString ()
        {
            string levelList = "";

            LevelListConfig config = _configsProviderService.GetConfig<LevelListConfig>();

            for (int i = 0; i < config.LevelConfigs.Count; i++)
            {
                if (i < _playerProgress.HighestUnlockedLevel)
                {
                    levelList += $"Level {i + 1}: Unlocked\n";
                }
                else
                {
                    levelList += $"Level {i + 1}: Locked\n";
                }
            }

            return levelList;
        }
    }
}
