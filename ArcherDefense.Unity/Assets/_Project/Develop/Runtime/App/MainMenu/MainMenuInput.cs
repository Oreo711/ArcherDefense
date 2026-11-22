using System.Collections.Generic;
using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuInput : ITickable
    {
        private readonly GameNavigation _gameNavigation;
        private readonly PlayerProgressService _progressService;

        private readonly Dictionary<KeyCode, int> _keyCodes = new()
        {
            { KeyCode.Keypad1, 1 },
            { KeyCode.Keypad2, 2 },
            { KeyCode.Keypad3, 3 },
            { KeyCode.Keypad4, 4 },
            { KeyCode.Keypad5, 5 },
            { KeyCode.Keypad6, 6 },
            { KeyCode.Keypad7, 7 },
            { KeyCode.Keypad8, 8 },
            { KeyCode.Keypad9, 9 }
        };

        public MainMenuInput(GameNavigation gameNavigation, WalletService wallet, PlayerProgressService progressService)
        {
            _gameNavigation  = gameNavigation;
            _progressService = progressService;
        }

        public void Tick()
        {
            foreach (KeyCode keycode in _keyCodes.Keys)
            {
                if (Input.GetKeyDown(keycode))
                {
                    if (_keyCodes[keycode] > _progressService.HighestUnlockedLevel)
                    {
                        Debug.Log("Level Locked");
                        return;
                    }
                    
                    _progressService.SelectLevel(_keyCodes[keycode]);
                    _gameNavigation.OpenGameplay();
                }
            }
        }
    }
}
