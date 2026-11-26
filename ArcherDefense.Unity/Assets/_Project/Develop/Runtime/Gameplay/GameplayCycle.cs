using System;
using System.Collections.Generic;
using System.Linq;
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
		[SerializeField] private List<Transform> _holes;

		private readonly List<Mole> _moles = new();

		private ConfigsProviderService _configsProviderService;
		private PlayerProgressService  _progressService;
		private MoleFactory            _moleFactory;
		private GameNavigation         _gameNavigation;
		private WhackInputService      _inputService;
		private WalletService          _walletService;

		private LevelListConfig _levelListConfig;
		private LevelConfig     _levelConfig;

		private bool  _isSetup;
		private bool  _isOver;
		private bool  _hasWon;
		private int   _currentLevel;
		private float _nextInterval;
		private int   _lives;
		private int   _whacks;
		private int   _earnedGold;

		[Inject]
		public void Construct (
			ConfigsProviderService configsProviderService,
			PlayerProgressService progressService,
			MoleFactory moleFactory,
			GameNavigation gameNavigation,
			WhackInputService inputService,
			WalletService walletService
		)
		{
			_configsProviderService = configsProviderService;
			_progressService        = progressService;
			_moleFactory            = moleFactory;
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

			foreach (Transform hole in _holes)
			{
				Mole mole = _moleFactory.CreateMole(hole.position);

				_moles.Add(mole);
			}

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
				_whacks++;
				_earnedGold += _levelConfig.GoldPerWhack;
			}

			if (_whacks >= _levelConfig.WhackGoal)
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

			if (_isOver)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if (_hasWon)
					{
						_gameNavigation.OpenMainMenu();
					}
					else
					{
						_gameNavigation.OpenGameplay();
					}
				}

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
			_isOver = true;
			_hasWon = true;
			Debug.Log("You Win!");
			_walletService.Add(CurrencyId.Gold, _earnedGold);
			_progressService.CompleteLevel(_currentLevel);
		}

		private void Lose ()
		{
			_isOver = true;
			Debug.Log("You Lose!");
			_earnedGold = 0;
		}

		private void PickMole ()
		{
			List<Mole> hiddenMoles = new List<Mole>(_moles.Where(mole => !mole.IsOut));
			Mole       mole        = hiddenMoles[Random.Range(0, _moles.Count)];
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
