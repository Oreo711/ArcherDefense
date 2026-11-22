using System;
using System.Collections.Generic;
using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay
{
	public sealed class GameplayCycle : MonoBehaviour, ITickable, IDisposable
	{
		[SerializeField] private List<Mole> _moles;

		private ConfigsProviderService _configsProviderService;
		private PlayerProgressService  _progressService;
		private GameNavigation         _gameNavigation;
		private WhackInputService      _inputService;
		private WalletService          _walletService;

		private LevelListConfig _levelListConfig;
		private LevelConfig     _levelConfig;

		private bool  _isSetup;
		private int   _currentLevel;
		private float _nextInterval;
		private int   _lives;
		private int   _whacks;
		private int   _earnedGold;

		[Inject]
		public void Construct (
			ConfigsProviderService configsProviderService,
			PlayerProgressService progressService,
			GameNavigation gameNavigation,
			WhackInputService inputService,
			WalletService walletService
		)
		{
			_configsProviderService = configsProviderService;
			_progressService        = progressService;
			_gameNavigation         = gameNavigation;
			_inputService           = inputService;
			_walletService          = walletService;
			_levelListConfig        = _configsProviderService.GetConfig<LevelListConfig>();
		}

		public void Setup (int level)
		{
			_currentLevel = level;
			_levelConfig  = _levelListConfig.LevelConfigs[level - 1];
			_lives        = _levelConfig.InitialLives;
			_nextInterval = Random.Range(_levelConfig.MinMoleInterval, _levelConfig.MaxMoleInterval);

			_inputService.Whacked += OnWhacked;

			foreach (Mole mole in _moles)
			{
				mole.Entered += OnMoleEnteredManually;
			}

			_isSetup = true;
		}

		private void OnWhacked (int number)
		{
			Mole mole = _moles[number - 1];

			if (mole.IsOut)
			{
				mole.EnterHole();
			}

			_whacks++;
			_earnedGold += _levelConfig.GoldPerWhack;

			if (_whacks > _levelConfig.WhackGoal)
			{
				Win();
			}
		}

		public void Tick ()
		{
			if (!_isSetup)
			{
				return;
			}

			foreach (Mole mole in _moles)
			{
				mole.Tick();
			}

			_nextInterval -= Time.deltaTime;

			if (_nextInterval <= 0)
			{
				PickMole();
				_nextInterval = Random.Range(_levelConfig.MinMoleInterval, _levelConfig.MaxMoleInterval);
			}
		}

		private void OnMoleEnteredManually ()
		{
			_lives--;
			Debug.Log($"Lost a life! {_lives} remain.");

			if (_lives <= 0)
			{
				Lose();
			}
		}

		private void Win ()
		{
			Debug.Log("You Win!");
			_walletService.Add(CurrencyId.Gold, _earnedGold);
			_progressService.CompleteLevel(_currentLevel);
			_gameNavigation.OpenMainMenu();
		}

		private void Lose ()
		{
			Debug.Log("You Lose!");
			_earnedGold = 0;
			_gameNavigation.OpenGameplay();
		}

		private void PickMole ()
		{
			Mole mole = _moles[Random.Range(0, _moles.Count)];
			mole.LeaveHole(_levelConfig.MoleWhackWindow);
		}

		public void Dispose ()
		{
			foreach (Mole mole in _moles)
			{
				mole.Entered -= OnMoleEnteredManually;
			}
		}
	}
}
