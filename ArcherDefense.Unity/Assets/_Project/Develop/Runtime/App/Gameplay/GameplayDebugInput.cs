using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Gameplay
{
    public sealed class GameplayDebugInput : ITickable
    {
        private readonly GameNavigation _gameNavigation;
        private readonly PlayerProgressService _playerProgress;

        public GameplayDebugInput(GameNavigation gameNavigation, PlayerProgressService playerProgress)
        {
            _gameNavigation = gameNavigation;
            _playerProgress = playerProgress;
        }

        public void Tick()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (keyboard.spaceKey.wasPressedThisFrame)
                _gameNavigation.OpenMainMenu();

            if (keyboard.vKey.wasPressedThisFrame)
                CompleteLevel();
        }

        private void CompleteLevel()
        {
            _playerProgress.CompleteLevel(_playerProgress.CurrentLevel);

            Debug.Log($"Победа: текущий уровень {_playerProgress.CurrentLevel}");

            _gameNavigation.OpenMainMenu();
        }
    }
}
