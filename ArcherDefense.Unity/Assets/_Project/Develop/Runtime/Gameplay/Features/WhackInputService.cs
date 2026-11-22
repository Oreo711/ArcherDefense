using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features
{
	public sealed class WhackInputService : ITickable
	{
		public event Action<int> Whacked;

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

		public void Tick ()
		{
			foreach (KeyCode keyCode in _keyCodes.Keys)
			{
				if (Input.GetKeyDown(keyCode))
				{
					Whacked?.Invoke(_keyCodes[keyCode]);
				}
			}
		}
	}
}
