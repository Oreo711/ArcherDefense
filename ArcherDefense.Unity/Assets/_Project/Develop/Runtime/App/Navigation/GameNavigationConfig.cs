using UnityEngine;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation
{
    [CreateAssetMenu(fileName = "GameNavigationConfig", menuName = "Configs/GameNavigationConfig")]
    public sealed class GameNavigationConfig : ScriptableObject
    {
        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _gameplaySceneName;

        public string MainMenuSceneKey => _mainMenuSceneName;

        public string GameplaySceneKey => _gameplaySceneName;
    }
}
