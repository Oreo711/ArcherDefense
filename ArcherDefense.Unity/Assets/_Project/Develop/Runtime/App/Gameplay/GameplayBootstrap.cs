using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Gameplay
{
    // Пока переход синхронный, сцена запускает себя сама: когда переход научится ждать её готовности, запускать будет он
    public sealed class GameplayBootstrap : IStartable
    {
        private readonly GameplayCycle         _gameplayCycle;
        private readonly PlayerProgressService _progressService;

        public GameplayBootstrap (GameplayCycle gameplayCycle, PlayerProgressService progressService)
        {
            _gameplayCycle   = gameplayCycle;
            _progressService = progressService;
        }

        public void Start()
        {
            _gameplayCycle.Setup(_progressService.CurrentLevel);
        }
    }
}
