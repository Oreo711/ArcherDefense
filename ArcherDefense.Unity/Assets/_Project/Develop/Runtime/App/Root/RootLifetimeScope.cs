using System;
using System.Collections.Generic;
using _Project.Develop.Runtime._Project.Develop.Runtime.App.Navigation;
using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress;
using _Project.Develop.Runtime.Infrastructure.ApplicationStatus;
using _Project.Develop.Runtime.Infrastructure.ConfigsService;
using _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader;
using _Project.Develop.Runtime.Infrastructure.SceneManagement;
using VContainer;
using VContainer.Unity;


namespace _Project.Develop.Runtime.App.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            InstallSceneLoading(builder);
            InstallNavigation(builder);
            InstallConfigs(builder);
            InstallMeta(builder);
            InstallApplicationStatus(builder);
        }

        private void InstallSceneLoading(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, BuiltInSceneLoader>(Lifetime.Singleton);
        }

        private void InstallNavigation(IContainerBuilder builder)
        {
            builder.Register<GameNavigation>(Lifetime.Singleton);
        }

        private void InstallApplicationStatus(IContainerBuilder builder)
        {
            builder.RegisterComponentOnNewGameObject<ApplicationLifecycleNotifier>(Lifetime.Singleton)
                .DontDestroyOnLoad()
                .As<IApplicationLifecycleNotifier>();

            builder.RegisterEntryPoint<ApplicationLifecycleLog>();
        }

        private void InstallMeta(IContainerBuilder builder)
        {
            builder.Register<WalletService>(Lifetime.Singleton);
            builder.Register<PlayerProgressService>(Lifetime.Singleton);
        }

        private void InstallConfigs(IContainerBuilder builder)
        {
            builder.Register<ConfigsProviderService>(Lifetime.Singleton);

            builder.Register<IConfigLoader, ResourcesConfigLoader>(Lifetime.Singleton)
                .WithParameter<IReadOnlyDictionary<Type, string>>(new Dictionary<Type, string>
                {
                    [typeof(GameNavigationConfig)] = "Configs/GameNavigationConfig",
                    [typeof(LevelListConfig)] = "Configs/LevelListConfig"
                });
        }
    }
}
