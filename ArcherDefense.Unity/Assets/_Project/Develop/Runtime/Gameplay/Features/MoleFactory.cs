using UnityEngine;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features
{
	public class MoleFactory
	{
		private readonly Mole _moleComponent;

		public MoleFactory (Mole moleComponent)
		{
			_moleComponent = moleComponent;
		}

		public Mole CreateMole (Vector3 position)
		{
			Mole mole = Object.Instantiate(_moleComponent, position, Quaternion.identity);

			return mole;
		}
	}
}
