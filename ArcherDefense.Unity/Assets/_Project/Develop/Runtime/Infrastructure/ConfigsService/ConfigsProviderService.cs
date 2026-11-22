using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService
{
    public sealed class ConfigsProviderService
    {
        private readonly Dictionary<Type, object> _configs = new();
        private readonly IEnumerable<IConfigLoader> _loaders;

        public ConfigsProviderService(IEnumerable<IConfigLoader> loaders)
        {
            _loaders = loaders;
        }

        public void Load()
        {
            _configs.Clear();

            foreach (IConfigLoader loader in _loaders)
                loader.Load(AddConfig);
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.TryGetValue(typeof(T), out object config) == false)
                throw new InvalidOperationException(
                    $"Config of type '{typeof(T).Name}' is not loaded. " +
                    "Ensure Load has completed and one of the loaders provides this config.");

            return (T)config;
        }

        private void AddConfig(Type type, object config)
        {
            if (_configs.TryAdd(type, config) == false)
                throw new InvalidOperationException(
                    $"Duplicate config type '{type.Name}': more than one source provides a config of this type.");
        }
    }
}
