using System.Collections.Generic;
using UnityEngine;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay
{
	[CreateAssetMenu(fileName = "LevelListConfig", menuName = "Configs/LevelListConfig")]
	public class LevelListConfig : ScriptableObject
	{
		[SerializeField] private List<LevelConfig> _levelConfigs;

		public IReadOnlyList<LevelConfig> LevelConfigs => _levelConfigs;
	}
}
