using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay;
using _Project.Develop.Runtime._Project.Develop.Runtime.Gameplay.Features;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.App.Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private Mole _mole;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayBootstrap>();
            builder.RegisterComponentInHierarchy<GameplayCycle>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponent(_mole).AsImplementedInterfaces().AsSelf();
            builder.Register<MoleFactory>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<WhackInputService>(Lifetime.Scoped).AsSelf();
        }
    }
}
