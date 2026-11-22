using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features;
using VContainer;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayBootstrap>();
            builder.RegisterEntryPoint<GameplayDebugInput>();
            builder.RegisterComponentInHierarchy<GameplayCycle>().AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<WhackInputService>(Lifetime.Scoped).AsSelf();
        }
    }
}
