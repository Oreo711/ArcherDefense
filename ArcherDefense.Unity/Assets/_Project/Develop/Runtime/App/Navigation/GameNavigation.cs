using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Infrastructure.SceneManagement;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation
{
    public sealed class GameNavigation
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ConfigsProviderService _configsProviderService;

        private GameNavigationConfig _config;

        public GameNavigation(ISceneLoader sceneLoader, ConfigsProviderService configsProviderService)
        {
            _sceneLoader = sceneLoader;
            _configsProviderService = configsProviderService;
        }

        private GameNavigationConfig Config
        {
            get
            {
                if (_config == null)
                    _config = _configsProviderService.GetConfig<GameNavigationConfig>();

                return _config;
            }
        }

        public void OpenMainMenu() => _sceneLoader.Load(Config.MainMenuSceneKey);

        public void OpenGameplay() => _sceneLoader.Load(Config.GameplaySceneKey);
    }
}
