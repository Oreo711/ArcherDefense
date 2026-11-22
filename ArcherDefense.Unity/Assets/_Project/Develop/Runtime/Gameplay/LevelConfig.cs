using UnityEngine;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
	public class LevelConfig : ScriptableObject
	{
		[SerializeField] private int   _whackGoal;
		[SerializeField] private int   _initialLives;
		[SerializeField] private int   _goldPerWhack;
		[SerializeField] private float _minMoleInterval;
		[SerializeField] private float _maxMoleInterval;
		[SerializeField] private float _moleWhackWindow;

		public int   WhackGoal       => _whackGoal;
		public int   InitialLives    => _initialLives;
		public int   GoldPerWhack    => _goldPerWhack;
		public float MinMoleInterval => _minMoleInterval;
		public float MaxMoleInterval => _maxMoleInterval;
		public float MoleWhackWindow => _moleWhackWindow;
	}
}
