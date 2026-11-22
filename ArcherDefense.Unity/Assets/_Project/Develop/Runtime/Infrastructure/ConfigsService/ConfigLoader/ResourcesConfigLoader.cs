using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.ConfigsService.ConfigLoader
{
    public sealed class ResourcesConfigLoader : IConfigLoader
    {
        private readonly IReadOnlyDictionary<Type, string> _configPaths;

        public ResourcesConfigLoader(IReadOnlyDictionary<Type, string> configPaths)
        {
            _configPaths = configPaths;
        }

        public void Load(Action<Type, object> addConfig)
        {
            foreach (KeyValuePair<Type, string> configPath in _configPaths)
            {
                ScriptableObject config = Resources.Load<ScriptableObject>(configPath.Value);

                if (config == null)
                    throw new InvalidOperationException(
                        $"Config of type '{configPath.Key.Name}' is not found in Resources at path '{configPath.Value}'.");

                addConfig(configPath.Key, config);
            }
        }
    }
}
