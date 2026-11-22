using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuDebugInput : ITickable
    {
        private const int DEBUG_INCOME = 100;
        private const int DEBUG_PRICE = 50;

        private readonly GameNavigation _gameNavigation;
        private readonly WalletService _wallet;

        public MainMenuDebugInput(GameNavigation gameNavigation, WalletService wallet)
        {
            _gameNavigation = gameNavigation;
            _wallet = wallet;
        }

        public void Tick()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (keyboard.spaceKey.wasPressedThisFrame)
                _gameNavigation.OpenGameplay();

            if (keyboard.gKey.wasPressedThisFrame)
            {
                _wallet.Add(CurrencyId.Gold, DEBUG_INCOME);

                Debug.Log($"Золото: {_wallet.Get(CurrencyId.Gold)}");
            }

            if (keyboard.hKey.wasPressedThisFrame)
            {
                bool isSpent = _wallet.TrySpend(CurrencyId.Gold, DEBUG_PRICE);

                Debug.Log(isSpent
                    ? $"Потрачено {DEBUG_PRICE}, осталось {_wallet.Get(CurrencyId.Gold)}"
                    : $"Не хватает золота: {_wallet.Get(CurrencyId.Gold)}");
            }
        }
    }
}
