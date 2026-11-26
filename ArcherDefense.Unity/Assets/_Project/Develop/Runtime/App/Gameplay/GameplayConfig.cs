using UnityEngine;

namespace _Project.Develop.Runtime
{
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private int _startingGold;

        public int StartingGold => _startingGold;
    }
}
