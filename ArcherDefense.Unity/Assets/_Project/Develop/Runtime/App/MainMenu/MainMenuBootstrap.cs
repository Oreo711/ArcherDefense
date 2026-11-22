using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.MainMenu
{
    // Пока переход синхронный, сцена запускает себя сама: когда переход научится ждать её готовности, запускать будет он
    public sealed class MainMenuBootstrap : IStartable
    {
        private readonly PlayerProgressService _playerProgress;
        private readonly WalletService         _wallet;

        public MainMenuBootstrap(PlayerProgressService playerProgress, WalletService wallet)
        {
            _playerProgress = playerProgress;
            _wallet         = wallet;
        }

        public void Start()
        {
            Debug.Log($"In main menu. Current level: {_playerProgress.CurrentLevel}");
            Debug.Log($"Gold: {_wallet.Get(CurrencyId.Gold)}");
        }
    }
}
