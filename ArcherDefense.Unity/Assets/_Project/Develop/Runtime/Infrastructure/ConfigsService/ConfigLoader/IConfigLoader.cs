using System;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader
{
    public interface IConfigLoader
    {
        void Load(Action<Type, object> addConfig);
    }
}
